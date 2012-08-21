using System;
using System.Collections.Generic;

namespace chess
{
    interface IBishop
    {
        List<Coord> StepBishop(String[] chessBoard);
    }
}
