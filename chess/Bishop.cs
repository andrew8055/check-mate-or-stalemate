using System;
using System.Collections.Generic;

namespace chess
{
    class Bishop : Figure
    {
        public Bishop(int hor, int ver, String[] chessBoard)
        {
            this.hor = hor; this.ver = ver;
            this.possibleMoves = StepBishop(chessBoard);
        }

        public List<Coord> StepBishop(String[] chessBoard)
        {
            List<Coord> possibleMoves = new List<Coord>();
            Dictionary<int, bool> isBusy = new Dictionary<int, bool>();
            Coord[] stepCoord = { new Coord(1, 1, 6), 
                                  new Coord(-1, -1, 2), 
                                  new Coord(-1, 1, 8),
                                  new Coord(1, -1, 4) };

            for (int i = 2; i <= 8; i += 2)
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

            //bool[] isBusy = { false, false, false, false };
            //List<Coord> possibleMoves = new List<Coord>();

            //for (int i = 1; i < chessBoard.Length; ++i)
            //{
            //    if (this.hor + i <= 7 && this.ver + i <= 7 && isBusy[0] == false)
            //    {
            //        if (!chessBoard[this.ver + i][this.hor + i].Equals('.'))
            //            isBusy[0] = true;

            //        possibleMoves.Add(new Coord(this.hor + i, this.ver + i, 6));
            //    }
            //    if (this.hor - i >= 0 && this.ver - i >= 0 && isBusy[1] == false)
            //    {
            //        if (!chessBoard[this.ver - i][this.hor - i].Equals('.'))
            //            isBusy[1] = true;

            //        possibleMoves.Add(new Coord(this.hor - i, this.ver - i, 2));
            //    }
            //    if (this.hor - i >= 0 && this.ver + i <= 7 && isBusy[2] == false)
            //    {
            //        if (!chessBoard[this.ver + i][this.hor - i].Equals('.'))
            //            isBusy[2] = true;

            //        possibleMoves.Add(new Coord(this.hor - i, this.ver + i, 8));
            //    }
            //    if (this.hor + i <= 7 && this.ver - i >= 0 && isBusy[3] == false)
            //    {
            //        if (!chessBoard[this.ver - i][this.hor + i].Equals('.'))
            //            isBusy[3] = true;

            //        possibleMoves.Add(new Coord(this.hor + i, this.ver - i, 4));
            //    }
            //}

            //return possibleMoves;
        }
    }
}
