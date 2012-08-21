using System.Collections.Generic;

namespace chess
{
    class King : Figure
    {
        public King(int hor, int ver) { this.hor = hor; this.ver = ver; }

        public King(int hor, int ver, List<Coord> possibleMoves)
        {
            this.hor = hor; this.ver = ver;

            this.possibleMoves.InsertRange(0, possibleMoves);
        }
        
        public void StepKing(List<Figure> blackFigures, List<Figure> whiteFigures)
        {
            Coord[] stepCoord = { new Coord(-1, 0, 1), 
                                  new Coord(-1, -1, 2), 
                                  new Coord(0, -1, 3), 
                                  new Coord(1, -1, 4), 
                                  new Coord(1, 0, 5), 
                                  new Coord(1, 1, 6), 
                                  new Coord(0, 1, 7), 
                                  new Coord(-1, 1, 8) };

            bool isPossibleMove = true;

            foreach (Coord coord in stepCoord)
            {
                int hor = this.hor + coord.hor;
                int ver = this.ver + coord.ver;
                int direction = coord.direction;

                if (hor < 0 || hor > 7 || ver < 0 || ver > 7)
                    continue;

                foreach (Figure blackFigure in blackFigures)
                {
                    if (blackFigure.IsMovePossible(hor, ver, direction))
                    {
                        isPossibleMove = false;
                        break;
                    }
                }
                foreach (Figure whiteFigure in whiteFigures)
                {
                    if (whiteFigure.hor == hor && whiteFigure.ver == ver 
                        && whiteFigure.GetName != "King")
                    {
                        isPossibleMove = false;
                        break;
                    }
                }
                if (isPossibleMove == true)
                    possibleMoves.Add(new Coord(hor, ver, direction));
                isPossibleMove = true;
            }
        }

        public static King ConvertFigureToKing(Figure figure)
        {
            return new King(figure.hor, figure.ver, figure.possibleMoves);
        }
    }   
}
