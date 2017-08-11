using AcademyInvaders.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyInvaders.Models
{
    public class Boss : Enemy, IBoss
    {
        private IList<IBullet> shootedBullets;

        public Boss(Position position, int health, Size? size, ConsoleColor color, int randomX) : base(position, health, size, color, randomX)
        {
            this.Health = 10;
            this.ShootedBullets = new List<IBullet>();
        }

        public IList<IBullet> ShootedBullets
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
        }

        public void Shoot()
        {
            Position startBulletPosition = new Position() { X = this.ObjectPosition.X, Y = this.ObjectPosition.Y };

            this.ShootedBullets.Add(new Bullet("Big Boss Bullet", this.Position, new Size(1, 1), ConsoleColor.DarkBlue));
        }

        public override string ToString()
        {
            return string.Format("\\-BigBoss-/");
        }
    }
}
