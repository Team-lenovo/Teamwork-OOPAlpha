using AcademyInvaders.Models.Abstractions;
using AcademyInvaders.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyInvaders.Models
{
    public class Bullet : GameObject, IMoveable, IRemoveable, IPrintable
    {
        private Position currPlayerPosition;
        private string playerName;
        private const int BulletHealth = 1;
        private Size BulletSize = new Size(1, 1);
        private Position objectPosition;



        public Bullet(string playerName, Position currPlayerPosition, Size BulletSize) : base(currPlayerPosition, BulletHealth, BulletSize)
        {
            this.PlayerName = playerName;
            this.ObjectPosition = currPlayerPosition;
            this.Move();
            
        }

        

        public string PlayerName
        {
            get { return this.playerName; }
            set { this.playerName = value; }
        }

        public ConsoleColor Color
        {
            get
            {
                return ConsoleColor.Red;
            }
        }

        public override int Health { get; protected set; }


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

        public Position CurrPlayerPosition
        {
            get
            {
                return this.currPlayerPosition;

            }
            set
            {
                this.currPlayerPosition = value;
            }

        }

        public override void Destroy()
        {
            throw new NotImplementedException();
        }




        public override void Move()
        {
            while (this.ObjectPosition.Y>0)
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
