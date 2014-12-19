using System;

namespace Yat
{
    public class Town
    {
        public int X {
            get;
            private set;
        }

        public int Y {
            get;
            private set;
        }

        public Town(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}

