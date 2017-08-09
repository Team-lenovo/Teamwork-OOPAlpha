using AcademyInvaders.Models.Abstractions;
using AcademyInvaders.Models.Contracts;
using AcademyInvaders.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyInvaders.Models
{
    [Serializable]
    public class Bullet : GameObject, IMoveable, IPrintable, IBullet
    {
        //private Position currPlayerPosition;
        private string playerName;
        private const int BulletHealth = 1;
        private Size BulletSize = new Size(1, 1);
        public Position objectPosition;

        public Bullet(string playerName, Position currPlayerPosition, Size BulletSize, ConsoleColor color = ConsoleColor.Red) : base(currPlayerPosition, BulletHealth, BulletSize)
        {
            this.PlayerName = playerName;
            this.ObjectPosition = new Position(currPlayerPosition.X + 2, currPlayerPosition.Y - 1);
            this.Color = color;
        }

        public string PlayerName
        {
            get { return this.playerName; }
            set { this.playerName = value; }
        }

        public ConsoleColor Color { get; set; }

        public override int Health { get; set; }


        public Position ObjectPosition
        {
            get
            {
                return this.objectPosition;
            }
            set
            {
                this.objectPosition = value;
            }
        }

        public override void Move()
        {
            if (objectPosition.Y != 0)
            {
                this.objectPosition.Y--;
            }
        }

        public override string ToString()
        {
            return "*";
        }
    }
}
