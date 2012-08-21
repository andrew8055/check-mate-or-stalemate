using System;
using System.Collections.Generic;

namespace chess
{
    class Figure
    {
        public int hor, ver;
        public List<Coord> possibleMoves;

        public Figure()
        {
            this.hor = 0;
            this.ver = 0;
            this.possibleMoves = new List<Coord>();
        }

        public String GetName { get { return GetType().Name; } }

        public bool IsMovePossible(int hor, int ver, int direction)
        {
            return this.possibleMoves.Contains(new Coord(hor, ver, direction));
        }
    }
}
