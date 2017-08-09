using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyInvaders
{
    public static class GameStory
    {
        public static void printTitle()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.CursorVisible = false;
            string title = File.ReadAllText(@"..\..\Core\Resources\title.txt");

            Console.WriteLine(title);
        }

        public static void printTitle2()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.CursorVisible = false;
            string title2 = File.ReadAllText(@"..\..\Core\Resources\title2.txt");

            Console.WriteLine(title2);
        }


        public static void printMenu()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            string menu = File.ReadAllText(@"..\..\Core\Resources\menu.txt");
            Console.WriteLine(menu);
        }
        
        public static void printGameOver()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            string gameOver = File.ReadAllText(@"..\..\Core\Resources\gameOver.txt");
            Console.WriteLine(gameOver);
        }

        public static void printBigVikWinsAgain()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            string bigVik = File.ReadAllText(@"..\..\Core\Resources\bigVikWinsAgain.txt");
            Console.WriteLine(bigVik);
        }

        public static void printGameComplete()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            string gameComplete = File.ReadAllText(@"..\..\Core\Resources\gameComplete.txt");
            Console.WriteLine(gameComplete);
        }

        public static void printLevelStory(int level)
        {
            string introLevel = (@"..\..\Core\Resources\levels\00.introLevel.txt");
            string firstLevelCompleted = (@"..\..\Core\Resources\levels\01.firstLevelCompleted.txt");
            string secondLevelCompleted = (@"..\..\Core\Resources\levels\02.secondLevelCompleted.txt");
            switch (level)
            {
                case 0:
                    Console.WriteLine(introLevel);

                    break;
                case 1:
                    Console.WriteLine(firstLevelCompleted);
                    break;
                case 2:
                    Console.WriteLine(secondLevelCompleted);
                    printGameComplete();
                    break;
                default:
                    break;


            };
        }
    }

}
