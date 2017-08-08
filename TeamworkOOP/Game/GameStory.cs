﻿using System;
using System.Collections.Generic;
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
            string title = @"    
 
 █████╗  ██████╗ █████╗ ██████╗ ███████╗███╗   ███╗██╗   ██╗    
██╔══██╗██╔════╝██╔══██╗██╔══██╗██╔════╝████╗ ████║╚██╗ ██╔╝    
███████║██║     ███████║██║  ██║█████╗  ██╔████╔██║ ╚████╔╝     
██╔══██║██║     ██╔══██║██║  ██║██╔══╝  ██║╚██╔╝██║  ╚██╔╝      
██║  ██║╚██████╗██║  ██║██████╔╝███████╗██║ ╚═╝ ██║   ██║       
╚═╝  ╚═╝ ╚═════╝╚═╝  ╚═╝╚═════╝ ╚══════╝╚═╝     ╚═╝   ╚═╝    
   
██╗███╗   ██╗██╗   ██╗ █████╗ ██████╗ ███████╗██████╗ ███████╗
██║████╗  ██║██║   ██║██╔══██╗██╔══██╗██╔════╝██╔══██╗██╔════╝
██║██╔██╗ ██║██║   ██║███████║██║  ██║█████╗  ██████╔╝███████╗
██║██║╚██╗██║╚██╗ ██╔╝██╔══██║██║  ██║██╔══╝  ██╔══██╗╚════██║
██║██║ ╚████║ ╚████╔╝ ██║  ██║██████╔╝███████╗██║  ██║███████║
╚═╝╚═╝  ╚═══╝  ╚═══╝  ╚═╝  ╚═╝╚═════╝ ╚══════╝╚═╝  ╚═╝╚══════╝
";
             
            Console.WriteLine(title);
        }

        public static void printTitle2()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            string title = @"    
 
 █████╗  ██████╗ █████╗ ██████╗ ███████╗███╗   ███╗██╗   ██╗    
██╔══██╗██╔════╝██╔══██╗██╔══██╗██╔════╝████╗ ████║╚██╗ ██╔╝    
███████║██║     ███████║██║  ██║█████╗  ██╔████╔██║ ╚████╔╝     
██╔══██║██║     ██╔══██║██║  ██║██╔══╝  ██║╚██╔╝██║  ╚██╔╝      
██║  ██║╚██████╗██║  ██║██████╔╝███████╗██║ ╚═╝ ██║   ██║       
╚═╝  ╚═╝ ╚═════╝╚═╝  ╚═╝╚═════╝ ╚══════╝╚═╝     ╚═╝   ╚═╝    
   
██╗███╗   ██╗██╗   ██╗ █████╗ ██████╗ ███████╗██████╗ ███████╗
██║████╗  ██║██║   ██║██╔══██╗██╔══██╗██╔════╝██╔══██╗██╔════╝
██║██╔██╗ ██║██║   ██║███████║██║  ██║█████╗  ██████╔╝███████╗
██║██║╚██╗██║╚██╗ ██╔╝██╔══██║██║  ██║██╔══╝  ██╔══██╗╚════██║
██║██║ ╚████║ ╚████╔╝ ██║  ██║██████╔╝███████╗██║  ██║███████║
╚═╝╚═╝  ╚═══╝  ╚═══╝  ╚═╝  ╚═╝╚═════╝ ╚══════╝╚═╝  ╚═╝╚══════╝
";
            string pressKey = @"
 __   __   ___  __   __                                  ___           
|__) |__) |__  /__` /__`        /\  |\ | \ /       |__/ |__  \ /       
|    |  \ |___ .__/ .__/       /~~\ | \|  |        |  \ |___  |        
                                                                       
___  __         __   __       ___              ___                     
 |  /  \       /  ` /  \ |\ |  |  | |\ | |  | |__                      
 |  \__/       \__, \__/ | \|  |  | | \| \__/ |___                     
                                                                                                                                                                                                                                                
";
            Console.WriteLine(title + "\n\n\n\n");

            Console.WriteLine(pressKey);
        }

        public static void printTitleAlternative()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;

            string title = @"
 █████╗ ██╗     ██████╗ ██╗  ██╗ █████╗                       
██╔══██╗██║     ██╔══██╗██║  ██║██╔══██╗                      
███████║██║     ██████╔╝███████║███████║                      
██╔══██║██║     ██╔═══╝ ██╔══██║██╔══██║                      
██║  ██║███████╗██║     ██║  ██║██║  ██║                      
╚═╝  ╚═╝╚══════╝╚═╝     ╚═╝  ╚═╝╚═╝  ╚═╝                      
                                                              
██╗███╗   ██╗██╗   ██╗ █████╗ ██████╗ ███████╗██████╗ ███████╗
██║████╗  ██║██║   ██║██╔══██╗██╔══██╗██╔════╝██╔══██╗██╔════╝
██║██╔██╗ ██║██║   ██║███████║██║  ██║█████╗  ██████╔╝███████╗
██║██║╚██╗██║╚██╗ ██╔╝██╔══██║██║  ██║██╔══╝  ██╔══██╗╚════██║
██║██║ ╚████║ ╚████╔╝ ██║  ██║██████╔╝███████╗██║  ██║███████║
╚═╝╚═╝  ╚═══╝  ╚═══╝  ╚═╝  ╚═╝╚═════╝ ╚══════╝╚═╝  ╚═╝╚══════╝
";
            Console.WriteLine(title);
        }
        
        public static void printGameOver()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            string gameOver = @"
  ▄████  ▄▄▄       ███▄ ▄███▓▓█████     ▒█████   ██▒   █▓▓█████  ██▀███  
 ██▒ ▀█▒▒████▄    ▓██▒▀█▀ ██▒▓█   ▀    ▒██▒  ██▒▓██░   █▒▓█   ▀ ▓██ ▒ ██▒
▒██░▄▄▄░▒██  ▀█▄  ▓██    ▓██░▒███      ▒██░  ██▒ ▓██  █▒░▒███   ▓██ ░▄█ ▒
░▓█  ██▓░██▄▄▄▄██ ▒██    ▒██ ▒▓█  ▄    ▒██   ██░  ▒██ █░░▒▓█  ▄ ▒██▀▀█▄  
░▒▓███▀▒ ▓█   ▓██▒▒██▒   ░██▒░▒████▒   ░ ████▓▒░   ▒▀█░  ░▒████▒░██▓ ▒██▒
 ░▒   ▒  ▒▒   ▓▒█░░ ▒░   ░  ░░░ ▒░ ░   ░ ▒░▒░▒░    ░ ▐░  ░░ ▒░ ░░ ▒▓ ░▒▓░
  ░   ░   ▒   ▒▒ ░░  ░      ░ ░ ░  ░     ░ ▒ ▒░    ░ ░░   ░ ░  ░  ░▒ ░ ▒░
░ ░   ░   ░   ▒   ░      ░      ░      ░ ░ ░ ▒       ░░     ░     ░░   ░ 
      ░       ░  ░       ░      ░  ░       ░ ░        ░     ░  ░   ░     
";
            Console.WriteLine(gameOver);
        }

        public static void printBigVikWinsAgain()
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            string bigVik = @"
 ▄▄▄▄    ██▓  ▄████        ██▒   █▓ ██▓ ██ ▄█▀                                       
▓█████▄ ▓██▒ ██▒ ▀█▒      ▓██░   █▒▓██▒ ██▄█▒                                        
▒██▒ ▄██▒██▒▒██░▄▄▄░       ▓██  █▒░▒██▒▓███▄░                                        
▒██░█▀  ░██░░▓█  ██▓        ▒██ █░░░██░▓██ █▄                                        
░▓█  ▀█▓░██░░▒▓███▀▒         ▒▀█░  ░██░▒██▒ █▄                                       
░▒▓███▀▒░▓   ░▒   ▒          ░ ▐░  ░▓  ▒ ▒▒ ▓▒                                       
▒░▒   ░  ▒ ░  ░   ░          ░ ░░   ▒ ░░ ░▒ ▒░                                       
 ░    ░  ▒ ░░ ░   ░            ░░   ▒ ░░ ░░ ░                                        
 ░       ░        ░             ░   ░  ░  ░                                          
      ░                        ░                                                     
 █     █░ ██▓ ███▄    █   ██████     ▄▄▄        ▄████  ▄▄▄       ██▓ ███▄    █  ▐██▌ 
▓█░ █ ░█░▓██▒ ██ ▀█   █ ▒██    ▒    ▒████▄     ██▒ ▀█▒▒████▄    ▓██▒ ██ ▀█   █  ▐██▌ 
▒█░ █ ░█ ▒██▒▓██  ▀█ ██▒░ ▓██▄      ▒██  ▀█▄  ▒██░▄▄▄░▒██  ▀█▄  ▒██▒▓██  ▀█ ██▒ ▐██▌ 
░█░ █ ░█ ░██░▓██▒  ▐▌██▒  ▒   ██▒   ░██▄▄▄▄██ ░▓█  ██▓░██▄▄▄▄██ ░██░▓██▒  ▐▌██▒ ▓██▒ 
░░██▒██▓ ░██░▒██░   ▓██░▒██████▒▒    ▓█   ▓██▒░▒▓███▀▒ ▓█   ▓██▒░██░▒██░   ▓██░ ▒▄▄  
░ ▓░▒ ▒  ░▓  ░ ▒░   ▒ ▒ ▒ ▒▓▒ ▒ ░    ▒▒   ▓▒█░ ░▒   ▒  ▒▒   ▓▒█░░▓  ░ ▒░   ▒ ▒  ░▀▀▒ 
  ▒ ░ ░   ▒ ░░ ░░   ░ ▒░░ ░▒  ░ ░     ▒   ▒▒ ░  ░   ░   ▒   ▒▒ ░ ▒ ░░ ░░   ░ ▒░ ░  ░ 
  ░   ░   ▒ ░   ░   ░ ░ ░  ░  ░       ░   ▒   ░ ░   ░   ░   ▒    ▒ ░   ░   ░ ░     ░ 
    ░     ░           ░       ░           ░  ░      ░       ░  ░ ░           ░  ░    
                                                                                     
";
            Console.WriteLine(bigVik);
        }

        public static void printGameComplete()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            string gameComplete = @"
 __    __     _                               _        
/ / /\ \ \___| | ___ ___  _ __ ___   ___     | |_ ___  
\ \/  \/ / _ \ |/ __/ _ \| '_ ` _ \ / _ \    | __/ _ \ 
 \  /\  /  __/ | (_| (_) | | | | | |  __/    | || (_) |
  \/  \/ \___|_|\___\___/|_| |_| |_|\___|     \__\___/ 
                                                       
                  _       _             ____           
  /\/\   ___   __| |_   _| | ___       |___ \          
 /    \ / _ \ / _` | | | | |/ _ \        __) |         
/ /\/\ \ (_) | (_| | |_| | |  __/       / __/          
\/    \/\___/ \__,_|\__,_|_|\___|      |_____|                                                       
";
            Console.WriteLine(gameComplete);
        }

        public static void printLevelStory(int level)
        {
            string introLevel = @"
Dark times have fallen upon the Telerik Alpha Academy...

Evil alien invaders have attacked the academy and taken hold of it! They
have also taken hostage the trainers as well as the students of the academy.

Unfortunately, the invaders have programmed their warships to instantly attack
anybody who tries to enter the academy grounds.

Using your awesome programming skills, you have managed to breach their
system's security and steal one of their flagships. Now, the only thing you
have to do is navigate the ship and destroy the capital ship of the aliens
in order to free the academy of this vicious enemy.

Good luck!
";
            string firstLevelCompleted = @"
Good job!

You have defeated the first line of defense of the evil aliens. Unfortunately,
They have taken Victor captive and done some alien magic on him, making him hostile to you.

Defend yourself!!


";
            string secondLevelCompleted = @"
Congratulations, you have saved the Academy! 

You sure have proved to be awesome in programming as well as commanding evil alien ships.
Stay awesome and keep up the good work. 

Oh... and one more thing
";
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
