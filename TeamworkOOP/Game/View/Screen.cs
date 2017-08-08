using System;

using AcademyInvaders.Models;
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

        public static void PrintStats(Player player1, Player player2 = null)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"Score: {player1.Score} {player1.ObjectPosition.ToString()}");

            if (player2 != null)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(30, 0);
                Console.WriteLine($"Score: {player2.Score} {player2.ObjectPosition.ToString()}");
            }
        }
    }
}
