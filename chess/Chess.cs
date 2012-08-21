using System;
using System.Collections.Generic;
using System.IO;

namespace chess
{
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
                            && (direction.hor != coord.hor || direction.ver != coord.ver))
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
}
