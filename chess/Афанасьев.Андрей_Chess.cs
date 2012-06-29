using System;
using System.Collections.Generic;
using System.IO;

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

    class Rook : Figure
    {
        public Rook(int hor, int ver, String[] chessBoard)
        {
            this.hor = hor; this.ver = ver;
            this.possibleMoves = StepRook(chessBoard);            
        }

        public List<Coord> StepRook(String[] chessBoard)
        {
            bool[] isBusy = { false, false, false, false };
            List<Coord> possibleMoves = new List<Coord>();

            for (int i = 1; i < chessBoard.Length; ++i)
            {
                if (this.ver + i <= 7 && isBusy[0] == false)
                {
                    if (!chessBoard[this.ver + i][this.hor].Equals('.')) 
                        isBusy[0] = true;

                    possibleMoves.Add(new Coord(this.hor, this.ver + i, 7));
                }
                if (this.ver - i >= 0 && isBusy[1] == false)
                {
                    if (!chessBoard[this.ver - i][this.hor].Equals('.')) 
                        isBusy[1] = true;

                    possibleMoves.Add(new Coord(this.hor, this.ver - i, 3));
                }
                if (this.hor + i <= 7 && isBusy[2] == false)
                {
                    if (!chessBoard[this.ver][this.hor + i].Equals('.')) 
                        isBusy[2] = true;

                    possibleMoves.Add(new Coord(this.hor + i, this.ver, 5));
                }
                if (this.hor - i >= 0 && isBusy[3] == false)
                {
                    if (!chessBoard[this.ver][this.hor - i].Equals('.')) 
                        isBusy[3] = true;

                    possibleMoves.Add(new Coord(this.hor - i, this.ver, 1));
                }
            }

            return possibleMoves;
        }
    }       

    class Bishop : Figure
    {
        public Bishop(int hor, int ver, String[] chessBoard)
        {
            this.hor = hor; this.ver = ver;
            this.possibleMoves = StepBishop(chessBoard);
        }

        public List<Coord> StepBishop(String[] chessBoard)
        {
            bool[] isBusy = { false, false, false, false };
            List<Coord> possibleMoves = new List<Coord>();

            for (int i = 1; i < chessBoard.Length; ++i)
            {
                if (this.hor + i <= 7 && this.ver + i <= 7 && isBusy[0] == false)
                {
                    if (!chessBoard[this.ver + i][this.hor + i].Equals('.')) 
                        isBusy[0] = true;

                    possibleMoves.Add(new Coord(this.hor + i, this.ver + i, 6));
                }
                if (this.hor - i >= 0 && this.ver - i >= 0 && isBusy[1] == false)
                {
                    if (!chessBoard[this.ver - i][this.hor - i].Equals('.')) 
                        isBusy[1] = true;

                    possibleMoves.Add(new Coord(this.hor - i, this.ver - i, 2));
                }
                if (this.hor - i >= 0 && this.ver + i <= 7 && isBusy[2] == false)
                {
                    if (!chessBoard[this.ver + i][this.hor - i].Equals('.')) 
                        isBusy[2] = true;

                    possibleMoves.Add(new Coord(this.hor - i, this.ver + i, 8));
                }
                if (this.hor + i <= 7 && this.ver - i >= 0 && isBusy[3] == false)
                {
                    if (!chessBoard[this.ver - i][this.hor + i].Equals('.')) 
                        isBusy[3] = true;

                    possibleMoves.Add(new Coord(this.hor + i, this.ver - i, 4));
                }
            }

            return possibleMoves;
        }
    }

    interface IRook
    {
        List<Coord> StepRook(String[] chessBoard);
    }

    interface IBishop
    {
        List<Coord> StepBishop(String[] chessBoard);
    }

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

    class Chess
    {
        private String[] chessBoard;
        private List<Figure> blackFigures;
        private List<Figure> whiteFigures;

        public Chess()
        {
            this.chessBoard = new String[8];
            this.blackFigures = new List<Figure>();
            this.whiteFigures = new List<Figure>();
        }

        public void LoadFromFile(String fileName)
        {
            StreamReader reader = new StreamReader(fileName);

            for (int i = 0; i < 8; ++i)
            {
                chessBoard[i] = reader.ReadLine();
                chessBoard[i] = chessBoard[i].Replace(" ", "");

                if (chessBoard[i].Length != 8)
                {
                    Console.WriteLine("Доска размера 8*8! Вы ввели больше/меньше чем необходимо значений");
                    --i;
                    continue;
                }
            }

            for (int i = 0; i < 8; ++i)
            {
                for (int j = 0; j < 8; ++j)
                {
                    switch (chessBoard[i][j].ToString())
                    {
                        case "K":
                            whiteFigures.Add(new King(j, i));
                            break;
                        case "Q":
                            whiteFigures.Add(new Queen(j, i, chessBoard));
                            break;
                        case "B":
                            whiteFigures.Add(new Bishop(j, i, chessBoard));
                            break;
                        case "N":
                            whiteFigures.Add(new Knight(j, i, chessBoard));
                            break;
                        case "R":
                            whiteFigures.Add(new Rook(j, i, chessBoard));
                            break;
                        case "k":
                            blackFigures.Add(new King(j, i));
                            break;
                        case "q":
                            blackFigures.Add(new Queen(j, i, chessBoard));
                            break;
                        case "b":
                            blackFigures.Add(new Bishop(j, i, chessBoard));
                            break;
                        case "n":
                            blackFigures.Add(new Knight(j, i, chessBoard));
                            break;
                        case "r":
                            blackFigures.Add(new Rook(j, i, chessBoard));
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public void Load()
        {
            Console.WriteLine("Set the chessboard: ");

            for (int i = 0; i < 8; ++i)
            {
                chessBoard[i] = Console.ReadLine();
                chessBoard[i] = chessBoard[i].Replace(" ", "");

                if (chessBoard[i].Length != 8)
                {
                    Console.WriteLine("Доска размера 8*8! Вы ввели больше/меньше чем необходимо значений");
                    --i;
                    continue;
                }
            }

            for (int i = 0; i < 8; ++i)
            {
                for (int j = 0; j < 8; ++j)
                {
                    switch (chessBoard[i][j].ToString())
                    {
                        case "K":
                            whiteFigures.Add(new King(j, i));
                            break;
                        case "Q":
                            whiteFigures.Add(new Queen(j, i, chessBoard));
                            break;
                        case "B":
                            whiteFigures.Add(new Bishop(j, i, chessBoard));
                            break;
                        case "N":
                            whiteFigures.Add(new Knight(j, i, chessBoard));
                            break;
                        case "R":
                            whiteFigures.Add(new Rook(j, i, chessBoard));
                            break;
                        case "k":
                            blackFigures.Add(new King(j, i));
                            break;
                        case "q":
                            blackFigures.Add(new Queen(j, i, chessBoard));
                            break;
                        case "b":
                            blackFigures.Add(new Bishop(j, i, chessBoard));
                            break;
                        case "n":
                            blackFigures.Add(new Knight(j, i, chessBoard));
                            break;
                        case "r":
                            blackFigures.Add(new Rook(j, i, chessBoard));
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        public void GameResult()
        {
            King whiteKing = King.ConvertFigureToKing
                             (
                                 whiteFigures.Find
                                 (
                                     delegate(Figure figure)
                                     {
                                         return figure.GetName.Equals("King");
                                     }
                                 )
                             );
                
            
            List<Figure> blackAttackFigures = blackFigures.FindAll
            (
                delegate(Figure figure) 
                { 
                    return figure.IsMovePossible(whiteKing.hor, whiteKing.ver, 0); 
                }
            );

            if (blackAttackFigures.Count == 0)
            {
                if (whiteFigures.Count > 1)
                {
                    Console.WriteLine("ok");
                    return;
                }

                whiteKing.StepKing(blackFigures, whiteFigures);

                if (whiteKing.possibleMoves.Count > 0)
                {
                    Console.WriteLine("ok");
                    return;
                }
                else
                {
                    Console.WriteLine("stalemate");
                    return;
                }
            }            
            else
            {
                whiteKing.StepKing(blackFigures, whiteFigures);

                if (whiteKing.possibleMoves.Count > 0)
                {
                    Console.WriteLine("check");
                    return;
                }
                if (blackAttackFigures.Count > 1)
                {
                    Console.WriteLine("mate");
                    return;
                }
                else
                {
                    Coord direction = blackAttackFigures[0].possibleMoves.Find
                                      (
                                          delegate(Coord crd) 
                                          {
                                              return crd.hor == whiteKing.hor 
                                                     && crd.ver == whiteKing.ver; 
                                          }
                                      );

                    foreach (Coord coord in blackAttackFigures[0].possibleMoves)
                    {
                        if (direction.direction == coord.direction 
                            && (direction.hor != coord.hor || direction.ver != coord.ver) )
                        {
                            foreach (Figure whiteFigure in whiteFigures)
                            {
                                if (whiteFigure.hor == whiteKing.hor 
                                    && whiteFigure.ver == whiteKing.ver)
                                    continue;

                                Coord coordIntercept = null;

                                coordIntercept = whiteFigure.possibleMoves.Find
                                       (
                                           delegate(Coord crd) 
                                           { 
                                               return (crd.hor == coord.hor 
                                                       && crd.ver == coord.ver) 
                                                      || (crd.hor == blackAttackFigures[0].hor 
                                                          && crd.ver == blackAttackFigures[0].ver);
                                           }
                                       );

                                if (coordIntercept != null)
                                {
                                    Console.WriteLine("check");
                                    return;
                                }
                            }
                        }
                    }
                    Console.WriteLine("mate");
                }
            }
        }
    }    

    class Program
    {
        static void Main(string[] args)
        {
            Chess party = new Chess();

            party.Load();            
            party.GameResult();

            Console.ReadLine();
        }
    }
}
