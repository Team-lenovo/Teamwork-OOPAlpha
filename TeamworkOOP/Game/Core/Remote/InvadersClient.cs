using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

using AcademyInvaders.Core.Contracts;
using AcademyInvaders.Core.Remote;
using AcademyInvaders.Models;
using AcademyInvaders.Utils;

namespace AcademyInvaders.Remote
{
    public class InvadersClient : IClient
    {
        private TcpClient client;
        private IPAddress ip;
        private int port;
        private StreamReader sReader;
        private StreamWriter sWriter;

        private Player clientPlayer;
        private string playerName;

        public InvadersClient()
        {
            this.ip = IPAddress.Parse(InvadersIO.ReadSettings("ip"));
            this.port = int.Parse(InvadersIO.ReadSettings("port"));
            this.client = new TcpClient();
        }

        public TcpClient Client
        {
            get
            {
                return this.client;
            }
            set
            {
                this.client = value;
            }
        }

        public Player ClientPlayer
        {
            get
            {
                return this.clientPlayer;
            }
            set
            {
                this.clientPlayer = value;
            }
        }

        public string PlayerName
        {
            get
            {
                return this.playerName;
            }
            set
            {
                this.playerName = value;
            }
        }

        public void ConnectClient()
        {
            while (!client.Connected)
            {
                try
                {
                    Console.WriteLine("Attempting connection...");
                    client.Connect(this.ip, this.port);
                    this.sReader = new StreamReader(client.GetStream());
                    this.sWriter = new StreamWriter(client.GetStream());

                    this.sWriter.WriteLine("New player connected! " + DateTime.Now);
                    this.sWriter.Flush();
                    // Connection confirmation from server
                    Console.WriteLine(this.sReader.ReadLine());
                }
                catch (Exception)
                {
                    Console.WriteLine("Connection Error!");

                    Console.WriteLine("Starting new Invaders server...");
                    Thread serverThread = new Thread(InvadersServer.Instance.Start);
                    serverThread.Start();
                }
            }
        }

        public void SendData(string output)
        {
            try
            {
                this.sWriter.WriteLine(output);
                this.sWriter.Flush();
            }
            catch (SocketException)
            {
                Console.WriteLine("Error writing object!");
            }
        }

        public string ReadServerData()
        {
            string serverData = "";
            try
            {
                serverData = this.sReader.ReadLine();
            }
            catch (IOException)
            {
                Console.WriteLine("Error reading object!");
            }

            return serverData;
        }

        public void Chat()
        {
            while (client.Connected)
            {
                Console.Write("Client: ");
                string sData = Console.ReadLine();

                sWriter.WriteLine(sData);
                sWriter.Flush();

                String sDataIncomming = sReader.ReadLine();
                Console.WriteLine(sDataIncomming);
            }
        }

        public void CloseClient()
        {
            sWriter.Close();
            sReader.Close();
            client.Close();
        }
    }
}