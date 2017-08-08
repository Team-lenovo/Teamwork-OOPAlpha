using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Linq;

using AcademyInvaders.Core.Contracts;
using AcademyInvaders.Models;
using AcademyInvaders.Models.Contracts;
using AcademyInvaders.Remote;
using AcademyInvaders.View;
using static AcademyInvaders.Utils.ConsoleWindowSet;

namespace AcademyInvaders.Core
{
    public class Engine : IEngine
    {
        private static readonly IEngine instance = new Engine();
        private int gameSpeed;
        private List<IPrintable> gameObjects;

        public static IEngine Instance
        {
            get
            {
                return instance;
            }
        }

        public int GameSpeed
        {
            get
            {
                return this.gameSpeed;
            }
            set
            {
                this.gameSpeed = value;
            }
        }

        public void Run()
        {
            Screen.SetScreenSize(41, 120);

            //IntPtr hConsole = DllImports.GetStdHandle(-11);   // get console handle
            //DllImports.COORD xy = new DllImports.COORD(100, 100);
            //DllImports.SetConsoleDisplayMode(hConsole, 1, out xy); // set the console to fullscreen
            Instance.GameSpeed = 200;

            while (true)
            {

                GameStory.printTitle();
                if (Console.KeyAvailable)
                {
                    break;
                }
                Thread.Sleep(1000);
                Console.Clear();
                GameStory.printTitle2();
                if (Console.KeyAvailable)
                {
                    break;
                }
                Thread.Sleep(1000);
                Console.Clear();
            }
            Console.Clear();

            GameStory.printMenu();

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Player offlinePlayer = new Player();
                    Instance.PlayOffline(offlinePlayer);
                    break;
                case 2:
                    IClient client = new InvadersClient();
                    client.ConnectClient();
                    Instance.PlayOnline(client);
                    break;
                case 3:
                    Environment.Exit(0);
                    return;
                default:
                    break;
            }
        }

        public void PlayOffline(Player offlinePlayer)
        {
            while (true)
            {
                Console.Clear();
                Console.CursorVisible = false;

                if (offlinePlayer.ShootedBullets.Count != 0)
                {
                    for (int i = 0; i < offlinePlayer.ShootedBullets.Count; i++)
                    {
                        if (offlinePlayer.ShootedBullets[i].ObjectPosition.Y == 0)
                        {
                            offlinePlayer.ShootedBullets.RemoveAt(i);
                        }
                        else
                        {
                            Screen.PrintObject(offlinePlayer.ShootedBullets[i]);
                            offlinePlayer.ShootedBullets[i].Move();
                        }
                    }
                }
                Screen.PrintObject(offlinePlayer);
                Screen.PrintStats(offlinePlayer);


                offlinePlayer.Move();
                //offlinePlayer.Score++; // Test ---------------

                Thread.Sleep((int)(100));
            }
        }

        public void PlayOnline(IClient client)
        {
            Player currPlayer;
            Player opponent;
            while (true)
            {
                Console.Clear();
                Console.CursorVisible = false;

                // Read GameObjects from server and print
                ReceiveSerializedList(client);
                currPlayer = (Player)gameObjects[0];
                opponent = (Player)gameObjects[1];

                // Game end -----------
                if (opponent.Health == 0)
                {
                    GameStory.printGameComplete();
                    break;
                }
                else if (currPlayer.Health == 0)
                {
                    GameStory.printGameOver();
                    break;
                }


                Screen.PrintStats(currPlayer, (Player)gameObjects[1]);

                currPlayer.ShootedBullets.ForEach(Screen.PrintObject);
                // Mirror opponent
                opponent.Skin = "|<V>|";
                opponent.playerPosition.Y = 1;
                opponent.ShootedBullets.ForEach(b =>
                {
                    b.objectPosition.Y = Console.WindowHeight - b.objectPosition.Y - 1;
                    Screen.PrintObject(b);
                });


                this.gameObjects.ForEach(Screen.PrintObject);

                int pressedKey = ReadPressedKey();
                client.SendData(pressedKey.ToString());

                Thread.Sleep((int)(300 - Instance.GameSpeed));
            }
        }

        public int ReadPressedKey()
        {
            int result = 0;
            while (Console.KeyAvailable)
            {
                ConsoleKeyInfo pressedKey = Console.ReadKey(true);
                result = (int)pressedKey.Key;
            }

            return result;
        }

        public void ReceiveSerializedList(IClient client)
        {
            try
            {
                NetworkStream ns = client.Client.GetStream();
                byte[] incommingBytes = new byte[12000];
                ns.Read(incommingBytes, 0, 12000);
                using (var ms = new MemoryStream(incommingBytes))
                {
                    var formatter = new BinaryFormatter();
                    this.gameObjects = (List<IPrintable>)formatter.Deserialize(ms);
                }

            }
            catch (IOException)
            {
                Console.WriteLine("Connection with server lost!");
                Console.WriteLine("Continue offline? (y/n)");
                string choice = Console.ReadLine().ToLower();
                if (choice == "y")
                {
                    Instance.PlayOffline(new Player());
                }
                else
                {
                    Environment.Exit(0);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Exception");
            }
        }
    }
}
