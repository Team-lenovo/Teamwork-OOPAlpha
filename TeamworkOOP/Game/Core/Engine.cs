using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

using AcademyInvaders.Core.Contracts;
using AcademyInvaders.Core.Factories;
using AcademyInvaders.Models.Contracts;
using AcademyInvaders.View;

namespace AcademyInvaders.Core
{
    public class Engine : IEngine
    {
        private static readonly IEngine instance = new Engine();
        private List<object> gameObjects = new List<object>();
        private int gameSpeed;


        public static IEngine Instance
        {
            get
            {
                return instance;
            }
        }

        public List<object> GameObjects
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
                if (value == 100)
                {
                    value = 99;
                }
                else
                {
                    this.gameSpeed = value;
                }
            }
        }

        public void Run()
        {
            Screen.SetScreenSize(41, 120);

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
            int randX = 0;
            int counter = 0;

            while (true)
            {
                Console.Clear();
                Console.CursorVisible = false;

                offlinePlayer.ShootedBullets.RemoveAll(b => b.ObjectPosition.Y == 1);
                offlinePlayer.ShootedBullets.ForEach(Screen.PrintObject);
                offlinePlayer.ShootedBullets.ForEach(b => b.Move());

                if (counter % 10 == 0)
                {
                    randX = rnd.Next(0, Console.WindowWidth - 2);
                    enemies.Add(InvadersFactory.Instance.CreateEnemy(null, 1, null, ConsoleColor.Green, randX));
                    Instance.GameSpeed++;
                }
                else if (counter > 1000)
                {

                }

                enemies.Remove(enemies.Find(i => i.ObjectPosition.Y == Console.WindowHeight));

                Screen.PrintObject(offlinePlayer);
                Screen.PrintStats(offlinePlayer);
                enemies.ForEach(Screen.PrintObject);
                enemies.ForEach(p => p.Move());
                offlinePlayer.Move();

                Instance.HitCheck(offlinePlayer, enemies);

                // Game end -----------
                if (offlinePlayer.Health == 0)
                {
                    Console.Clear();
                    GameStory.printGameOver();
                    break;
                }

                counter++;
                Thread.Sleep(100 - Instance.GameSpeed);
            }
        }

        public void PlayOnline(IClient client)
        {
            IPlayer currentPlayer;
            IPlayer opponent;
            List<IEnemy> enemies;

            while (true)
            {
                Console.Clear();
                Console.CursorVisible = false;

                // Read GameObjects from server and print
                ReceiveSerializedList(client);
                currentPlayer = (IPlayer)gameObjects[0];
                opponent = (IPlayer)gameObjects[1];
                enemies = (List<IEnemy>)gameObjects[2];

                // Game end -----------
                if (opponent.Health == 0)
                {
                    GameStory.printGameComplete();
                    break;
                }
                else if (currentPlayer.Health == 0)
                {
                    GameStory.printGameOver();
                    break;
                }

                Instance.MirrorOpponent(opponent);
                Screen.PrintStats(currentPlayer, opponent);
                currentPlayer.ShootedBullets.ForEach(Screen.PrintObject);
                Screen.PrintObject(currentPlayer);
                Screen.PrintObject(opponent);
                enemies.ForEach(Screen.PrintObject);

                int pressedKey = ReadPressedKey();
                client.SendData(pressedKey.ToString());

                Thread.Sleep(100);
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
                    this.gameObjects = (List<object>)formatter.Deserialize(ms);
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

        public void MirrorOpponent(IPlayer opponent)
        {
            opponent.Skin = "|<V>|";
            opponent.ObjectPosition.Y = 1;
            opponent.ShootedBullets.ForEach(b =>
            {
                b.ObjectPosition.Y = Console.WindowHeight - b.ObjectPosition.Y - 1;
                Screen.PrintObject(b);
            });
        }

        public void HitCheck(IPlayer player, List<IEnemy> enemies, IPlayer opponent = null)
        {
            if (enemies != null)
            {
                enemies.ForEach(enemy =>
                {
                    if (player.ShootedBullets.Any(bullet =>
                        bullet.ObjectPosition.X >= enemy.ObjectPosition.X &&
                        bullet.ObjectPosition.X <= enemy.ObjectPosition.X + enemy.ToString().Length - 1 &&
                        enemy.ObjectPosition.Y == bullet.ObjectPosition.Y))
                    {
                        player.Score++;
                        enemy.Health--;
                    }
                    else if (((enemy.ObjectPosition.X >= player.ObjectPosition.X &&
                              enemy.ObjectPosition.X <= player.ObjectPosition.X + player.ToString().Length - 1) ||
                              (enemy.ObjectPosition.X + enemy.ToString().Length - 1 <= player.ObjectPosition.X + player.ToString().Length - 1 &&
                              enemy.ObjectPosition.X + enemy.ToString().Length - 1 >= player.ObjectPosition.X)) &&
                             enemy.ObjectPosition.Y == player.ObjectPosition.Y)
                    {
                        player.Health--;
                    }
                });
                enemies.RemoveAll(e => e.Health == 0);
            }

            if (opponent != null)
            {
                if (player.ShootedBullets.Any(b =>
                    b.ObjectPosition.X >= opponent.ObjectPosition.X &&
                    b.ObjectPosition.X <= opponent.ObjectPosition.X + opponent.ToString().Length - 1 &&
                    opponent.ObjectPosition.Y == Console.WindowHeight - b.ObjectPosition.Y))
                {
                    player.Score++;
                    opponent.Health--;
                }
            }
        }
    }
}
