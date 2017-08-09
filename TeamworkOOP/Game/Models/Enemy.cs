using AcademyInvaders.Models.Abstractions;
using AcademyInvaders.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyInvaders.Models
{
    public class Enemy :GameObject, IMoveable, IPrintable, IRemoveable, IShootable
    {
        private ConsoleColor color;
        private Position objectPosition;

        public Enemy(Position position, int health, Size size) : base(position, health, size)
        {
            
        }

        public ConsoleColor Color
        {
            get
            {
                return ConsoleColor.Cyan;
            }
            
        }

        public override int Health
        {
            get
            {
                return 2;
            }

            protected set
            {
                this.Health = value;
            }
        }

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

        

        public void Destroy()
        {
            throw new NotImplementedException();
        }

        public override void Move()
        {
            throw new NotImplementedException();
        }

        

        public string  Print()
        {
            return "@@@@@";
        }

        public void Shoot()
        {
            throw new NotImplementedException();
        }

        //public IEnumerable<Enemy> CreateObjects()
        //{
        //    return new List<GameObject>();
        //}
    }
}
