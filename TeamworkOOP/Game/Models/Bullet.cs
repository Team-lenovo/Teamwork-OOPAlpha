using AcademyInvaders.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyInvaders.Models
{
    public class Bullet : IMoveable,IRemoveable, IPrintable
    {
        private  Position position;
        
        public Bullet(Position position)
        {
            this.Position = PlayerPosition;
        }

        public ConsoleColor Color
        {
            get
            {
                return ConsoleColor.Red;
            }
        }

        public Position PlayerPosition
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Position Position
        {
            get
            {
                return this.position;
                
            }
            set
            {
                this.position = value;
            }
           
        }
        
        public void Destroy()
        {
            throw new NotImplementedException();
        }

        public  void Move()
        {
            while (Console.KeyAvailable)
            {
                ConsoleKeyInfo pressedKey = Console.ReadKey(true);

                if (pressedKey.Key == ConsoleKey.Spacebar)
                {


                    this.Position=PlayerPosition;
                    
                    
                }
                  
            }
        }



        
    }
}
