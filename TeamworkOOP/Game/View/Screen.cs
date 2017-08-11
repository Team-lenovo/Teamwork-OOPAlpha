using System;

using AcademyInvaders.Models.Contracts;

namespace AcademyInvaders.View
{
    public static class Screen
    {
        public static readonly int screenHight;
        public static readonly int screenWidth;

        public static void SetScreenSize(int hight, int width)
        {
            Console.BufferHeight = Console.WindowHeight = hight;
            Console.BufferWidth = Console.WindowWidth = width;
        }

        public static void PrintObject(IPrintable obj)
        {
            Console.ForegroundColor = obj.Color;
            Console.SetCursorPosition(obj.ObjectPosition.X, obj.ObjectPosition.Y);
            Console.Write(obj);
        }

        public static void PrintStats(IPlayer player1, IPlayer player2 = null, IBoss boss = null)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"$:{player1.Score} H:{new string('+', player1.Health)}");

            if (player2 != null)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(Console.WindowWidth / 2 + 20, 0);
                Console.WriteLine($"$:{player2.Score} H:{new string('+', player2.Health)}");
            }

            if (boss != null)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(Console.WindowWidth / 2 + 20, 0);
                Console.WriteLine($"BOSS H:{new string('+', boss.Health)}");
            }
        }
    }
}
