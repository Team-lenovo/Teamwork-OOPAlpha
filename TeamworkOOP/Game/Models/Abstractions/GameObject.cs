using AcademyInvaders.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcademyInvaders.Models;

namespace AcademyInvaders.Models.Abstractions
{
    public abstract class GameObject : IRemoveable, IMoveable, IShootable
    {
        private Position position;
        private int health;             
        private Size size;

        public GameObject(Position position, int health, Size size)
        {
            this.Position = position;
            this.Health = health;
            this.Size = size;
        }

        public Position Position
        {
            get { return this.position; }
            set { this.position = value; }
        }
        
        public Size Size
        {
            get { return this.size; }
            set { this.size = value; }
        }

        public bool IsDestroyed { get; protected set; }
        public abstract int Health { get; protected set; }

        public abstract void Destroy();

        public abstract void Move();

        public abstract void Shoot();
        
        
    }
}
