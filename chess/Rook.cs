using System;
using System.Collections.Generic;

namespace chess
{
    class Rook : Figure
    {
        public Rook(int hor, int ver, String[] chessBoard)
        {
            this.hor = hor; this.ver = ver;
            this.possibleMoves = StepRook(chessBoard);
        }

        public List<Coord> StepRook(String[] chessBoard)
        {
            List<Coord> possibleMoves = new List<Coord>();
            Dictionary<int, bool> isBusy = new Dictionary<int, bool>();
            Coord[] stepCoord = { new Coord(-1, 0, 1), 
                                  new Coord(0, -1, 3), 
                                  new Coord(1, 0, 5),
                                  new Coord(0, 1, 7) };

            for (int i = 1; i <= 7; i += 2)
                isBusy.Add(i, false);

            for (int i = 1; i < chessBoard.Length; ++i)
            {
                foreach (Coord crd in stepCoord)
                {
                    int hor = this.hor + crd.hor * i;
                    int ver = this.ver + crd.ver * i;
                    int direct = crd.direction;

                    if (hor < 0 || hor > 7 || ver < 0 || ver > 7 || isBusy[direct])
                        continue;

                    if (!chessBoard[ver][hor].Equals('.'))
                        isBusy[direct] = true;

                    possibleMoves.Add(new Coord(hor, ver, direct));
                }
            }

            return possibleMoves;
        }
    }
}
