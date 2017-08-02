using Game.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Models
{
   public class Enemy : IMoveable, IPrintable, IRemoveable, IShootable
    {




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
    }
}
