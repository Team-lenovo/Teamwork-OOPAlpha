using AcademyInvaders.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyInvaders.Models
{
    public class Enemy : IMoveable, IPrintable, IRemoveable, IShootable
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

        public string  Print()
        {
            return "****";
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
