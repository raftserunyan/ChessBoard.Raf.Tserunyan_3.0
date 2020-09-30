using System;

namespace ChessBoard.Raf.Tserunyan_3._0
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board();
            board.Show();

            bool inputSuccessfull = false;
            string coord1 = "";
            string coord2 = "";

            while (!inputSuccessfull)
            {
                try
                {
                    Console.Write("Where do we put the knight? (For example: 4 f): ");
                    coord1 = Console.ReadLine();
                    board.knight = new Node(coord1);

                    inputSuccessfull = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(e.Message);
                    Console.ResetColor();
                    Console.WriteLine();
                }
            }

            board.knight.Distance = 0;
            board.knight.Parent = board.knight;

            board.Show();

            inputSuccessfull = false;
            while (!inputSuccessfull)
            {
                try
                {
                    Console.Write("Which one is our target cell? (For example: 8 f): ");
                    coord2 = Console.ReadLine();
                    board.FindShortestWay(coord2, coord1);

                    inputSuccessfull = true;
                }
                catch (Exception e)
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(e.Message);
                    Console.ResetColor();
                    Console.WriteLine();
                }
            }

            board.Show();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"You can get to cell {coord2} in just {board.AmountOfStepsToTheTarget} step(s).");
            Console.ResetColor();

            Console.ReadKey();
        }        
    }
}
