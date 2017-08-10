using AcademyInvaders.Models.Abstractions;
using AcademyInvaders.Models.Contracts;
using System;

namespace AcademyInvaders.Models
{
    [Serializable]
    public class Enemy : GameObject, IMoveable, IPrintable, IShootable, ISizeable, IEnemy
    {
        private ConsoleColor color;
        private Position objectPosition;
        private int health;

        public Enemy(Position position, int health, Size? size, ConsoleColor color, int randomX) : base(position, health, size)
        {
            this.ObjectPosition = position;
            this.Size = size;
            this.Color = color;
            this.ObjectPosition.X = randomX;
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

        public override int Health
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

            this.ObjectPosition.Y++;
        }

        public void Shoot()
        {
            throw new NotImplementedException();
        }

        void ISizeable.Size()
        {
            throw new NotImplementedException();
        }
        
        public override string ToString()
        {
            return "\\>V</";
        }
    }
}