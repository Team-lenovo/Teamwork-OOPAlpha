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
using AcademyInvaders.View;
using AcademyInvaders.Core.Factories;

namespace AcademyInvaders.Core
{
    public class Engine : IEngine
    {
        private static readonly IEngine instance = new Engine();
        private int gameSpeed;
        private List<IPrintable> gameObjects = new List<IPrintable>();

        public static IEngine Instance
        {
            get
            {
                return instance;
            }
        }

        public List<IPrintable> GameObjects
        {
            get
            {
                return this.gameObjects;
            }
            set
            {
                this.gameObjects = value;
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
                    IPlayer offlinePlayer = InvadersFactory.Instance.CreatePlayer();
                    IEnemy enemy = InvadersFactory.Instance.CreateEnemy();

                    Instance.PlayOffline(offlinePlayer);
                    break;
                case 2:
                    IClient client = InvadersFactory.Instance.CreateInvadersClient();
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

        public void PlayOffline(IPlayer offlinePlayer)
        {
            List<IEnemy> enemies = new List<IEnemy>();
            Random rnd = new Random();
            int counter = 0;

            while (true)
            {
                int randX = rnd.Next(0, Console.WindowWidth);
                Console.Clear();
                Console.CursorVisible = false;

                //if (offlinePlayer.ShootedBullets.Count != 0)
                //{
                //    for (int i = 0; i < offlinePlayer.ShootedBullets.Count; i++)
                //    {
                //        if (offlinePlayer.ShootedBullets[i].ObjectPosition.Y == 0)
                //        {
                //            offlinePlayer.ShootedBullets.RemoveAt(i);
                //        }
                //        else
                //        {
                //            Screen.PrintObject(offlinePlayer.ShootedBullets[i]);
                //            offlinePlayer.ShootedBullets[i].Move();
                //        }
                //    }
                //}

                offlinePlayer.ShootedBullets.Remove(offlinePlayer.ShootedBullets.ToList().Find(i => i.ObjectPosition.Y == 0));
                offlinePlayer.ShootedBullets.ToList().ForEach(Screen.PrintObject);
                offlinePlayer.ShootedBullets.ToList().ForEach(b => b.Move());

                if (counter % 20 == 0)
                {
                    enemies.Add(InvadersFactory.Instance.CreateEnemy(null, 2, null, ConsoleColor.Cyan, randX));
                }

                enemies.Remove(enemies.Find(i => i.ObjectPosition.Y == Console.WindowHeight));
                
                Screen.PrintObject(offlinePlayer);
                Screen.PrintStats(offlinePlayer);
                enemies.ForEach(Screen.PrintObject);

                enemies.ForEach(p => p.Move());
                offlinePlayer.Move();

                //offlinePlayer.Score++; // Test ---------------
                counter++;

                Thread.Sleep((int)(100));
            }
        }

        public void PlayOnline(IClient client)
        {
            IPlayer currPlayer;
            IPlayer opponent;
            while (true)
            {
                Console.Clear();
                Console.CursorVisible = false;

                // Read GameObjects from server and print
                ReceiveSerializedList(client);
                currPlayer = (IPlayer)gameObjects[0];
                opponent = (IPlayer)gameObjects[1];
                
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

                currPlayer.ShootedBullets.ToList().ForEach(Screen.PrintObject);
                // Mirror opponent
                opponent.Skin = "|<V>|";
                opponent.ObjectPosition.Y = 1;
                opponent.ShootedBullets.ToList().ForEach(b =>
                {
                    b.ObjectPosition.Y = Console.WindowHeight - b.ObjectPosition.Y - 1;
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

        public void RemoveObject(IPrintable gameObject, List<IPrintable> enemies)
        {
            if (gameObject != null && (gameObject.ObjectPosition.Y == 1 || gameObject.ObjectPosition.Y == Console.WindowHeight))
            {
                enemies.Remove(gameObject);
            }
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
                    Instance.PlayOffline(InvadersFactory.Instance.CreatePlayer());
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
