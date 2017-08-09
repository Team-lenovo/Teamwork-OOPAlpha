using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

using AcademyInvaders.Core.Contracts;
using AcademyInvaders.Models;
using AcademyInvaders.Models.Contracts;
using AcademyInvaders.Remote;
using AcademyInvaders.Utils;
using System.Linq;
using AcademyInvaders.Core.Factories;

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

        // Auto name opponent =============
        private Dictionary<string, IPlayer> players = new Dictionary<string, IPlayer>();
        // ==========================================================


        private InvadersServer()
        {
            this.ip = IPAddress.Parse(InvadersIO.ReadSettings("ip"));
            this.port = int.Parse(InvadersIO.ReadSettings("port"));
            this.server = new TcpListener(this.ip, port);
            this.clients = new Dictionary<string, TcpClient>();
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
                IClient newPClient = InvadersFactory.Instance.CreateInvadersClient();
                newPClient.Client = server.AcceptTcpClient();
                newPClient.PlayerName = newPClient.GetHashCode().ToString();
                newPClient.ClientPlayer = InvadersFactory.Instance.CreatePlayer();
                this.players.Add(newPClient.PlayerName, newPClient.ClientPlayer);

                // client found & create a thread to handle communication
                Thread t = new Thread(new ParameterizedThreadStart(HandleClient));
                t.Start(newPClient);
            }
        }

        public void HandleClient(object obj)
        {
            // retrieve client from parameter passed to thread
            IClient client = (IClient)obj;
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

            IPlayer onlinePlayer = client.ClientPlayer;
            IPlayer opponent = players[opponentName];

            NetworkStream ns = client.Client.GetStream();
            List<IPrintable> pl = new List<IPrintable>();
            pl.Add(onlinePlayer);
            pl.Add(opponent);
            while (clientConnected)
            {
                try
                {
                    // Online game start
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

                    // Hit check ======================
                    if (onlinePlayer.ShootedBullets.Any(b =>
                    b.ObjectPosition.X >= opponent.ObjectPosition.X &&
                    b.ObjectPosition.X <= opponent.ObjectPosition.X + opponent.ToString().Length - 1 &&
                    opponent.ObjectPosition.Y == Console.WindowHeight - b.ObjectPosition.Y))
                    {
                        onlinePlayer.Score++;
                        opponent.Health--;
                    }

                    data = sReader.ReadLine();
                    onlinePlayer.MoveOnLine(int.Parse(data));


                    if (data == "terminate")
                    {
                        Console.WriteLine("User terminated connection!");

                        sWriter.Close();
                        sReader.Close();
                        client.Client.Close();
                        clientConnected = false;
                    }

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

        public string ChooseOpponent(IClient client)
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
