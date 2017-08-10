using System;

using AcademyInvaders.Models.Contracts;
using System.Collections.Generic;

namespace AcademyInvaders.Models
{
    public enum PlayerState
    {
        Default,
        Shield,
        Speed,
        Dead
    }

    public enum WeaponChoice
    {
        Single,
        Double,
        Bombs,
        Torpedo,
        Spread
    }

    [Serializable]
    public class Player : IPlayer, IPrintable
    {
        private List<IBullet> shootedBullets;
        private string skin;             //Inherit from GameObject
        private PlayerState state;
        private WeaponChoice weapon;
        private ConsoleColor color;
        public Position playerPosition; //Inherit from GameObject

        private int lives;               //Inherit from GameObject
        private int health;              //Inherit from GameObject
        private int score;

        //private bool shielded; //Collision with states

        private string[] stateSkins = {
            "<^>",    //default <^> shoots ^
            "(<^>)",  //shielded (<^>)
            "/^\\",   //speed mode /^\ 
            ">x<"     //dead >x<
        };

        private string[] weaponSkins =
        {
            "\'",//extra single weapons '<^>' shoots '
            "\"",//extra double weapons "<^>" shoots "
            "*",  //extra bombs *<^>* shoots * x X
            "!",  //extra torpedoes !<^>! shoots !
            "|" //spread weapons |<^>| shoots \ /
        };

        //public Player() : this(PlayerState.Default, WeaponChoice.Torpedo, ConsoleColor.Cyan, new Position((Console.WindowWidth - 4) / 2, Console.WindowHeight - 1), 3, 30, 0)
        //{
        //}

        public Player(PlayerState state, WeaponChoice weapon, ConsoleColor color, Position playerPosition, int lives, int health, int score)
        {

            this.State = state;
            this.Weapon = weapon;
            this.Color = color;
            this.ObjectPosition = playerPosition;
            this.ShootedBullets = new List<IBullet>();
            this.Lives = lives;
            this.Health = health;
            this.Score = score;

        }

        public string Skin          //Inherit from GameObject
        {
            get
            {
                return this.skin;
            }
            set //Set by State, Wepons and Shielded
            {
                this.skin = value;
            }
        }

        public PlayerState State
        {
            get
            {
                return this.state;
            }
            set
            {
                this.Skin = stateSkins[(int)value];
                this.state = value;
            }
        }

        public WeaponChoice Weapon
        {
            get
            {
                return this.weapon;
            }
            set
            {
                this.Skin = $"{weaponSkins[(int)value]}{this.skin}{weaponSkins[(int)value]}";
                this.weapon = value;
            }
        }

        public ConsoleColor Color
        {
            get
            {
                return this.color;
            }
            set
            {
                this.color = value;
            }
        }

        public Position ObjectPosition //Inherit from GameObject
        {
            get
            {
                return this.playerPosition;
            }
            set
            {
                this.playerPosition = value;
            }
        }

        public int Lives              //Inherit from GameObject
        {
            get
            {
                return this.lives;
            }
            set
            {
                this.lives = value;
            }
        }

        public int Health
        {
            get
            {
                return this.health;
            }
            set
            {
                this.health = value;
            }
        }

        public int Score
        {
            get
            {
                return this.score;
            }
            set
            {
                this.score = value;
            }
        }

        public List<IBullet> ShootedBullets
        {
            get
            {
                return this.shootedBullets;
            }
            set
            {
                this.shootedBullets = value;
            }
        }


        public void Move()
        {
            while (Console.KeyAvailable)
            {
                ConsoleKeyInfo pressedKey = Console.ReadKey(true);

                if (pressedKey.Key == ConsoleKey.LeftArrow)
                {
                    if (this.ObjectPosition.X - 1 >= 1)
                    {
                        this.playerPosition.X -= 1; //TODO: Fix property access
                    }
                }
                else if (pressedKey.Key == ConsoleKey.RightArrow)
                {
                    if (this.ObjectPosition.X + 1 < Console.WindowWidth - this.ToString().Length)
                    {
                        this.playerPosition.X += 1;
                    }
                }
                else if (pressedKey.Key == ConsoleKey.Spacebar)
                {
                    Position startBulletPosition = new Position() { X = this.ObjectPosition.X, Y = this.ObjectPosition.Y };
                    IBullet newBullet = new Bullet(this.GetHashCode().ToString(), startBulletPosition, new Size(1, 1));
                    //Here must be created by factory
                    ShootedBullets.Add(newBullet);
                }
            }
        }

        public void MoveOnLine(int pressedKey)
        {
            if ((ConsoleKey)pressedKey == ConsoleKey.LeftArrow)
            {
                if (this.ObjectPosition.X - 1 >= 1)
                {
                    this.playerPosition.X -= 1;
                }
            }
            else if ((ConsoleKey)pressedKey == ConsoleKey.RightArrow)
            {
                if (this.ObjectPosition.X + 1 < Console.WindowWidth - this.ToString().Length)
                {
                    this.playerPosition.X += 1;
                }
            }
            else if ((ConsoleKey)pressedKey == ConsoleKey.Spacebar)
            {
                Position startBulletPosition = new Position() { X = this.ObjectPosition.X, Y = this.ObjectPosition.Y }; 
                IBullet newBullet = new Bullet(this.GetHashCode().ToString(), startBulletPosition, new Size(1, 1));
                //Here must be created by factory
                ShootedBullets.Add(newBullet);
            }
        }

        public override string ToString()
        {
            return this.Skin;
        }
    }
}
