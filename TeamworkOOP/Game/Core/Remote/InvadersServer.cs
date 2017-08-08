using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

using AcademyInvaders.Core.Contracts;
using AcademyInvaders.Core.Providers;
using AcademyInvaders.Models;
using AcademyInvaders.Models.Contracts;
using AcademyInvaders.Remote;
using AcademyInvaders.Utils;
using System.Linq;

namespace AcademyInvaders.Core.Remote
{
    public class InvadersServer : IServer
    {
        private static readonly InvadersServer instance = new InvadersServer();

        private readonly TcpListener server;
        private readonly IPAddress ip;
        private readonly int port;
        private bool isRunning;

        // Game logic ================================================
        private readonly Dictionary<string, TcpClient> clients;
        private readonly Dictionary<Tuple<string, string>, Player> users; //Load game

        // Auto name opponent =============
        private Dictionary<string, Player> players = new Dictionary<string, Player>();
        // ==========================================================


        private InvadersServer()
        {
            this.ip = IPAddress.Parse(InvadersIO.ReadSettings("ip"));
            this.port = int.Parse(InvadersIO.ReadSettings("port"));
            this.server = new TcpListener(this.ip, port);
            this.clients = new Dictionary<string, TcpClient>();
            this.users = GameDB.Instance.Users;
        }

        public static IServer Instance
        {
            get
            {
                return instance;
            }
        }

        public void Start()
        {
            this.server.Start();
            this.isRunning = true;
            Console.WriteLine("Invaders initiated!");

            this.LoopClients();
        }

        public void LoopClients()
        {
            while (isRunning) // TODO: Add server close method
            {
                Console.WriteLine("Waiting for connection...");

                // wait for client connection
                InvadersClient newPClient = new InvadersClient();
                newPClient.Client = server.AcceptTcpClient();
                newPClient.PlayerName = newPClient.GetHashCode().ToString();
                newPClient.ClientPlayer = new Player();
                this.players.Add(newPClient.PlayerName, newPClient.ClientPlayer);

                // client found & create a thread to handle communication
                Thread t = new Thread(new ParameterizedThreadStart(HandleClient));
                t.Start(newPClient);
            }
        }

        public void HandleClient(object obj)
        {
            // retrieve client from parameter passed to thread
            InvadersClient client = (InvadersClient)obj;
            StreamWriter sWriter = new StreamWriter(client.Client.GetStream());
            StreamReader sReader = new StreamReader(client.Client.GetStream());
            bool clientConnected = true;
            string data = "";

            // Client status read
            data = sReader.ReadLine();
            Console.WriteLine(data);

            // Connection confirmation
            sWriter.WriteLine("Your client is connected to InvadersServer!");
            sWriter.Flush();
            Console.Clear();

            // Player authentication
            //string playerName = PlayerAuthentication(client);

            string opponentName = this.ChooseOpponent(client);

            Player onlinePlayer = client.ClientPlayer;
            Player opponent = players[opponentName];

            NetworkStream ns = client.Client.GetStream();
            List<IPrintable> pl = new List<IPrintable>();
            pl.Add(onlinePlayer);
            pl.Add(opponent);
            while (clientConnected)
            {
                try
                {
                    // Online game start
                    onlinePlayer.Score++; // Test

                    SendSerializedObject(client.Client, pl);

                    if (onlinePlayer.ShootedBullets.Count != 0)
                    {
                        for (int i = 0; i < onlinePlayer.ShootedBullets.Count; i++)
                        {
                            if (onlinePlayer.ShootedBullets[i].ObjectPosition.Y == 0)
                            {
                                onlinePlayer.ShootedBullets.RemoveAt(i);
                            }
                            else
                            {
                                onlinePlayer.ShootedBullets[i].Move();
                            }
                        }
                    }

                    data = sReader.ReadLine();
                    onlinePlayer.MoveOnLine(int.Parse(data));

                    //Console.WriteLine(client.PlayerName + ": " + data);

                    if (data == "terminate")
                    {
                        Console.WriteLine("User terminated connection!");

                        sWriter.Close();
                        sReader.Close();
                        client.Client.Close();
                        clientConnected = false;
                    }

                    // to write something back
                    //sWriter.WriteLine("server test data: " + ((i++) % 10));
                    //sWriter.Flush();
                    Thread.Sleep(100); // TODO: Match game speed ========
                }
                catch (IOException)
                {
                    Console.WriteLine("Connection with player lost!");
                    sWriter.Close();
                    sReader.Close();
                    client.Client.Close();
                    clientConnected = false;
                }
            }
        }

        public string PlayerAuthentication(TcpClient client)
        {
            StreamReader sReader = new StreamReader(client.GetStream());
            StreamWriter sWriter = new StreamWriter(client.GetStream());

            sWriter.WriteLine("Enter user name: ");
            sWriter.Flush();
            string playerName = sReader.ReadLine();
            sWriter.WriteLine("Enter password: ");
            sWriter.Flush();
            string password = sReader.ReadLine();

            this.clients.Add(playerName, client); // Exchange user data & choose player 2 =====

            return playerName;
        }

        public string ChooseOpponent(InvadersClient client)
        {
            string opponentName = "";
            string currentPlayer = client.PlayerName;

            while (opponentName == "")
            {
                if (this.players.Count < 2)
                {
                    Thread.Sleep(1000);
                    Console.Clear();
                    Console.WriteLine("Waiting for second player...");
                    continue;
                }
                opponentName = this.players.Where(p => p.Key != currentPlayer).First().Key;
            }

            return opponentName.ToUpper();
        }

        public void SendSerializedObject(TcpClient client, object obj)
        {
            NetworkStream ns = client.GetStream();
            byte[] serializedObject;
            var formatter = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                formatter.Serialize(ms, obj);
                serializedObject = ms.ToArray();
            }
            ns.Write(serializedObject, 0, serializedObject.Length);
            ns.Flush();
        }
    }
}
