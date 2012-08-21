using System;
using System.Collections.Generic;

namespace chess
{
    interface IRook
    {
        List<Coord> StepRook(String[] chessBoard);
    }
}
