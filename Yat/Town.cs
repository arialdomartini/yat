using System;

namespace Yat
{
    public class Town
    {
        public int x {
            get;
            private set;
        }

        public int y {
            get;
            private set;
        }

        public Town(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}

