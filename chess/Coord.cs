using System;

namespace chess
{
    class Coord : IEquatable<Coord>
    {
        public int hor, ver, direction;

        public Coord(int hor, int ver, int direction)
        {
            this.hor = hor;
            this.ver = ver;
            this.direction = direction;
        }

        public bool Equals(Coord coord)
        {
            if (this.hor == coord.hor && this.ver == coord.ver)
                return true;
            else
                return false;
        }
    }    
}
