using System;
using System.Collections.Generic;

namespace chess
{
    class Queen : Figure, IRook, IBishop
    {
        private Rook rook;
        private Bishop bishop;

        public Queen(int hor, int ver, String[] chessBoard)
        {
            this.hor = hor; this.ver = ver;
            rook = new Rook(hor, ver, chessBoard);
            bishop = new Bishop(hor, ver, chessBoard);

            this.possibleMoves.InsertRange(0, StepRook(chessBoard));
            this.possibleMoves.InsertRange(0, StepBishop(chessBoard));
        }

        public List<Coord> StepRook(String[] chessBoard)
        {
            return this.rook.StepRook(chessBoard);
        }

        public List<Coord> StepBishop(String[] chessBoard)
        {
            return this.bishop.StepBishop(chessBoard);
        }
    }
}
