using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcademyInvaders.Models.Abstractions;

namespace AcademyInvaders.Models
{
    public class SinglePlayerMode : GameObject
    {
        public SinglePlayerMode(Position position, int health, Size size) 
        : base(position, health, size)
        {
        }

        public override int Health
        {
            get
            {
                throw new NotImplementedException();
            }

            protected set
            {
                throw new NotImplementedException();
            }
        }

        public override void Move()
        {
            throw new NotImplementedException();
        }

       
    }
}
