using AcademyInvaders.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyInvaders.Models
{
    public class Bullet : IMoveable, IPrintable, IRemoveable
    {
        public Position PlayerPosition => throw new NotImplementedException();

        public ConsoleColor Color => throw new NotImplementedException();

        public void Destroy()
        {
            throw new NotImplementedException();
        }

        public void Move()
        {
            throw new NotImplementedException();
        }

        public string Print()
        {
            return "*";
        }
    }
}
