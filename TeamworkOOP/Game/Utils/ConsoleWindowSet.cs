using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace AcademyInvaders.Utils
{
    // https://gist.github.com/lsauer/3741940
    public class ConsoleWindowSet
    {
        
        /// <summary>
        /// Contains native methods imported as unmanaged code.
        /// </summary>
        internal static class DllImports
        {
            [StructLayout(LayoutKind.Sequential)]
            public struct COORD
            {

                public short X;
                public short Y;
                public COORD(short x, short y)
                {
                    this.X = x;
                    this.Y = y;
                }

            }
            [DllImport("kernel32.dll")]
            public static extern IntPtr GetStdHandle(int handle);
            [DllImport("kernel32.dll", SetLastError = true)]
            public static extern bool SetConsoleDisplayMode(
                IntPtr ConsoleOutput
                , uint Flags
                , out COORD NewScreenBufferDimensions
                );
        }
    }
}
