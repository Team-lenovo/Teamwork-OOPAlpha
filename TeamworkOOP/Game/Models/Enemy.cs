using AcademyInvaders.Models.Abstractions;
using AcademyInvaders.Models.Contracts;
using System;

namespace AcademyInvaders.Models
{
    public class Enemy : GameObject, IMoveable, IPrintable, IRemoveable, IShootable, ISizeable, IEnemy
    {
        private ConsoleColor color;
        private Position objectPosition;
        private int health;

        public Enemy(Position position, int health, Size size, ConsoleColor color) : base(position, health, size)
        {
            this.Color = color;
        }

        public ConsoleColor Color
        {
            get
            {
                return ConsoleColor.Cyan;
            }
            set
            {
                this.color = value;
            }
        }

        public override int Health
        {
            get
            {
                return 2;
            }

            set
            {
                this.health = value;
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



        public string Print()
        {
            return "@@@@@";
        }

        public void Shoot()
        {
            throw new NotImplementedException();
        }

        void ISizeable.Size()
        {
            throw new NotImplementedException();
        }

        //public IEnumerable<Enemy> CreateObjects()
        //{
        //    return new List<GameObject>();
        //}
    }
}