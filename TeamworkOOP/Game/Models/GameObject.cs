using Game.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game
{
    public abstract class GameObject : IRemoveable, IMoveable
    {
        private Position position;
        private int health;
        private int lives;
        private string skin;

        public GameObject(Position position, int health, int lives, string skin)
        {
            this.Position = position;
            this.Health = health;
            this.Lives = lives;
            this.Skin = skin;
        }

        public Position Position
        {
            get { return this.position; }
            set { this.position = value; }
        }

        public int Health
        {
            get { return this.health; }
            set { this.health = value; }
        }

        public int Lives
        {
            get { return this.lives; }
            set { this.lives = value; }
        }

        public string Skin
        {
            get { return this.skin; }
            set { this.skin = value; }
        }

        public bool IsDestroyed { get; protected set; }


        public void Destroy()
        {
            this.health--;
            if (this.health == 0)
            {
                this.IsDestroyed = true;
            }
        }

        public void Move()
        {

        }

        public IEnumerable<GameObject> CreateObjects()
        {
            return new List<GameObject>();
        }
    }
}
