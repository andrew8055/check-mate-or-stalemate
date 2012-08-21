using System;
using System.IO;

namespace chess
{
    class Program
    {
        static void Main(string[] args)
        {
            Chess party = new Chess();
            string fileName;

            if (args.Length == 1)
                fileName = args[0];
            else
                fileName = "chessboard.txt";

            try
            {
                party.LoadFromFile(fileName);
                party.GameResult();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadLine();
        }
    }
}
