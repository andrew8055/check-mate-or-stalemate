using System;
using System.Collections.Generic;

namespace chess
{
    class Knight : Figure
    {
        public Knight(int hor, int ver, String[] chessBoard)
        {
            this.hor = hor; this.ver = ver;

            Coord[] stepCoord = { new Coord(-2, 1, 1), 
                                  new Coord(-2, -1, 2), 
                                  new Coord(-1, -2, 3),
                                  new Coord(1, -2, 4),
                                  new Coord(2, -1, 5),
                                  new Coord(2, 1, 6),
                                  new Coord(1, 2, 7),
                                  new Coord(-1, 2, 8) };

            foreach (Coord coord in stepCoord)
            {
                if (this.hor + coord.hor > 7 || this.hor + coord.hor < 0)
                    continue;
                if (this.ver + coord.ver > 7 || this.ver + coord.ver < 0)
                    continue;
                this.possibleMoves.Add(new Coord(this.hor + coord.hor,
                                                 this.ver + coord.ver, coord.direction));
            }
        }
    }
}
