using Game.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Game
{
    public static class GameStart
    {
        public static void Print(Player player)
        {
            Console.CursorVisible = false;

            Console.ForegroundColor = player.Color;
            Console.SetCursorPosition(player.PlayerPosition.X, player.PlayerPosition.Y);
            Console.WriteLine(player);

            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"Score: {player.Score} Position: X:{player.PlayerPosition.X} Y:{player.PlayerPosition.Y}");
        }
        public static void  Print(Enemy enemy)
        {
            Console.CursorVisible = false;
                        
            Console.SetCursorPosition(25, 25);
            Console.WriteLine(enemy.Print());

            Console.ForegroundColor = ConsoleColor.White;
           
         
        }


        public static void Main()
        {
            //Console size
            Console.BufferHeight = Console.WindowHeight = 40;
            Console.BufferWidth = Console.WindowWidth = 100;
            double speed = 200.0;

            Player player = new Player();
            Enemy enemy = new Enemy();

            Console.SetCursorPosition((Console.WindowWidth - player.Skin.Length) / 2, Console.WindowHeight - 1);
            Console.SetCursorPosition((Console.WindowWidth - 5) , Console.WindowHeight - 10);

            int i = 0;

            while (true) //Animation
            {
                Console.Clear();

                Print(player);
                Print(enemy);
                player.Move();
                player.Score++;



                Thread.Sleep((int)(100)); //Refresh rate
            }
        }
    }
}
