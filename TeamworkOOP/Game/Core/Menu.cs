using System;

using AcademyInvaders.Utils;

namespace AcademyInvaders
{
    public static class Menu
    {
        public static int Start()
        {
            string path = "../../Core/Resources/Start.txt";
            InvadersIO.PrintMenu(path);

            int choice = int.Parse(Console.ReadLine());
            Console.Clear();

            return choice;
        }
    }
}
