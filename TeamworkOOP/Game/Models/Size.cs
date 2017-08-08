﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademyInvaders.Models
{
    [Serializable]
    public struct Size
    {
        public Size(int width, int height)
        {
            this.Width = width;
            this.Height = height;
        }

        public int Width { get; set; }
        public int Height { get; set; }
    }
}
