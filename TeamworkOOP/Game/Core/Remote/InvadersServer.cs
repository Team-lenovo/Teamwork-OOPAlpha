using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

using AcademyInvaders.Core.Contracts;
using AcademyInvaders.Core.Factories;
using AcademyInvaders.Models.Contracts;
using AcademyInvaders.Utils;

namespace AcademyInvaders.Core.Remote
{
    public class InvadersServer : IServer
    {
        private static readonly InvadersServer instance = new InvadersServer();

        private readonly TcpListener server;
        private readonly IPAddress ip;
        private readonly int port;
        private bool isRunning;

        private readonly Dictionary<string, TcpClient> clients;
        private readonly Dictionary<string, IPlayer> players;


        private InvadersServer()
        {
            this.ip = IPAddress.Parse(InvadersIO.ReadSettings("ip"));
            this.port = int.Parse(InvadersIO.ReadSettings("port"));
            this.server = new TcpListener(this.ip, port);
            this.clients = new Dictionary<string, TcpClient>();
            this.players = new Dictionary<string, IPlayer>();
        }

        public static IServer Instance
        {
            get
            {
                return instance;
            }
        }

        public Dictionary<string, TcpClient> Clients
        {
            get
            {
                return this.clients;
            }
        }

        public Dictionary<string, IPlayer> Players
        {
            get
            {
                return this.players;
            }
        }


        public void Start()
        {
            this.server.Start();
            this.isRunning = true;
            Console.WriteLine("Invaders initiated!");

            Instance.LoopClients();
        }

        public void LoopClients()
        {
            while (isRunning)
            {
                Console.WriteLine("Waiting for connection...");

                IClient newPClient = InvadersFactory.Instance.CreateInvadersClient();
                newPClient.Client = server.AcceptTcpClient();
                newPClient.PlayerName = newPClient.GetHashCode().ToString();
                newPClient.ClientPlayer = InvadersFactory.Instance.CreatePlayer();
                Instance.Players.Add(newPClient.PlayerName, newPClient.ClientPlayer);

                Thread t = new Thread(new ParameterizedThreadStart(HandleClient));
                t.Start(newPClient);
            }
        }

        public void HandleClient(object obj)
        {
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

            IPlayer onlinePlayer = client.ClientPlayer;
            string opponentName = Instance.ChooseOpponent(client);
            IPlayer opponent = Instance.Players[opponentName];
            List<IEnemy> enemies = new List<IEnemy>();

            NetworkStream ns = client.Client.GetStream();
            List<object> objectsList = new List<object>() { onlinePlayer, opponent, enemies };

            Random rnd = new Random();
            int randX = 0;
            int counter = 0;

            while (clientConnected)
            {
                try
                {
                    // Online game start
                    SendSerializedObject(client.Client, objectsList);

                    onlinePlayer.ShootedBullets.RemoveAll(b => b.ObjectPosition.Y == 1);
                    onlinePlayer.ShootedBullets.ForEach(b => b.Move());

                    if (counter % 3 == 0)
                    {
                        randX = rnd.Next(0, Console.WindowWidth - 2);
                        enemies.Add(InvadersFactory.Instance.CreateEnemy(null, 1, null, ConsoleColor.Green, randX));
                    }
                    enemies.ForEach(p => p.Move());
                    Engine.Instance.HitCheck(onlinePlayer, enemies, opponent);
                    enemies.Remove(enemies.Find(i => i.ObjectPosition.Y == Console.WindowHeight));

                    // Read passed key
                    data = sReader.ReadLine();
                    onlinePlayer.MoveOnLine(int.Parse(data));

                    counter++;
                    Thread.Sleep(100);
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

        public string ChooseOpponent(IClient client)
        {
            string opponentName = "";
            string currentPlayer = client.PlayerName;

            while (opponentName == "")
            {
                if (Instance.Players.Count < 2)
                {
                    Thread.Sleep(1000);
                    Console.Clear();
                    Console.WriteLine("Waiting for second player...");
                    continue;
                }
                opponentName = Instance.Players.Where(p => p.Key != currentPlayer).First().Key;
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
