using AcademyInvaders.Models.Contracts;
using System;
using System.Collections.Generic;

namespace AcademyInvaders.Models
{
    public class Boss : Enemy, IBoss
    {
        private List<IBullet> shootedBullets;

        public Boss(Position position, int health, Size? size, ConsoleColor color, int randomX) : base(position, health, size, color, randomX)
        {
            this.Health = 10;
            this.ShootedBullets = new List<IBullet>();
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

        public override void Move()
        {

            Random rnd = new Random();
            int randX = 0;
            randX = rnd.Next(0, Console.WindowWidth - 4);

            this.ObjectPosition.X = randX;

            this.Shoot();
        }

        public void Shoot()
        {
            Position startBulletPosition = new Position() { X = this.ObjectPosition.X, Y = this.ObjectPosition.Y };

            this.ShootedBullets.Add(new Bullet("Big Boss Bullet", this.Position, new Size(1, 1), ConsoleColor.Yellow));
        }

        public override string ToString()
        {
            return string.Format("\\-BigBoss-/");
        }
    }
}
