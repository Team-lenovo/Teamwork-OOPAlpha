using System;

namespace AcademyInvaders.Models
{
    [Serializable]
    public class Position
    {
        public Position() : this(0, 0)
        {
        }
        public Position(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public override string ToString()
        {
            return string.Format($"[X:{this.X} Y:{this.Y}]");
        }
    }
}
