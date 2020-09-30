using System;
using System.Collections;

namespace ChessBoard.Raf.Tserunyan_3._0
{
    public class Board
    {
        public Node knight;
        public object[,] Matrix;
        public byte AmountOfStepsToTheTarget;

        public object this[int i, int j]
        {
            get { return Matrix[i, j]; }
            set { Matrix[i, j] = value; }
        }

        /// <summary>
        /// Initializes the chess board.
        /// </summary>
        public Board()
        {
            Node.board = this;
            Matrix = new object[8, 8];

            Create();
        }

        /// <summary>
        /// Initializes board's cells.
        /// </summary>
        private void Create()
        {
            for (byte i = 0; i < 8; i++)
            {
                for (byte j = 0; j < 8; j++)
                {
                    Matrix[i, j] = new Node(i, j);
                }
            }
        }

        /// <summary>
        /// Prints the chess board.
        /// </summary>
        public void Show()
        {
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("-----------------------------------------");
            Console.WriteLine("|   | A | B | C | D | E | F | G | H |   |");
            Console.WriteLine("-----------------------------------------");
            Console.ResetColor();

            for (byte i = 0; i < 8; i++)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"| {8 - i} |");
                Console.ResetColor();

                //Some loop for choosing colors
                for (byte j = 0; j < 8; j++)
                {
                    //Coloring cells(black or white)
                    switch ((i + j) % 2)
                    {
                        case 0:
                            {
                                Console.BackgroundColor = ConsoleColor.Gray;
                                break;
                            }
                        case 1:
                            {
                                Console.BackgroundColor = ConsoleColor.Black;
                                break;
                            }
                    }

                    Node currentNode = Matrix[i, j] as Node;

                    //Picking dark green color for knight's cell
                    if (currentNode == knight)
                        Console.BackgroundColor = ConsoleColor.DarkGreen;

                    //If knight has to step into current cell on the way of his target cell,
                    //paint that cell in magenta or red(in case if this cell is the final one)
                    if (currentNode.IsPartOfTheWay)
                    {
                        if (currentNode.StepNumber == AmountOfStepsToTheTarget)
                            Console.BackgroundColor = ConsoleColor.Red;
                        else
                            Console.BackgroundColor = ConsoleColor.Magenta;
                    }


                    //Picking the right fore color to make the text on the cell visible
                    switch (Console.BackgroundColor)
                    {
                        case ConsoleColor.Green:
                            {
                                Console.ForegroundColor = ConsoleColor.Black;
                                break;
                            }
                        case ConsoleColor.DarkGreen:
                            {
                                Console.ForegroundColor = ConsoleColor.Black;
                                break;
                            }
                        default:
                            {
                                Console.ForegroundColor = ConsoleColor.White;
                                break;
                            }
                    }

                    Console.Write($" {currentNode} ");
                    Console.ResetColor();

                    if (j < 7)
                        Console.Write("|");
                }

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"| {8 - i} |");
                Console.ResetColor();
                Console.WriteLine();

                if (i == 7)
                    Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("-----------------------------------------");
                Console.ResetColor();
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("|   | A | B | C | D | E | F | G | H |   |");
            Console.WriteLine("-----------------------------------------");
            Console.ResetColor();

            Console.WriteLine();
        }

        /// <summary>
        /// Check's user's input on correction and if everithing's all right, finds the shortest path to target cell.
        /// </summary>
        /// <param name="newCoord">Target cell's coordinates</param>
        /// <param name="firstCoord">Knight's coordinates</param>
        public void FindShortestWay(string newCoord, string firstCoord)
        {
            int I, J;

            if (newCoord == firstCoord)
                throw new Exception("You'v entered knight's current coordinates, try another coordinates...");

            try
            {
                I = 8 - byte.Parse(newCoord.Substring(0, 1));
                J = Convert.ToByte(Convert.ToChar(newCoord.Substring(2, 1).ToUpper())) - 65;
            }
            catch (Exception)
            {
                throw new Exception("Your input was not in correct format, try again...");
            }

            GetTheShortestPath(I, J);
        }

        /// <summary>
        /// Finds the shortest path to the target cell.
        /// </summary>
        /// <param name="i">Target cell's row number</param>
        /// <param name="j">Target cell's column number</param>
        private void GetTheShortestPath(int i, int j)
        {
            Queue DiscoveredCells = new Queue();

            DiscoveredCells.Enqueue(knight);

            while (DiscoveredCells.Count > 0)
            {
                Node current = (Node)DiscoveredCells.Dequeue();

                current.SetAvailableCells();

                if (current == Matrix[i, j])
                {
                    AmountOfStepsToTheTarget = (byte)current.Distance;

                    current.StepNumber = AmountOfStepsToTheTarget;
                    do
                    {
                        current.Name = current.StepNumber.ToString();
                        current.IsPartOfTheWay = true;

                        Node parentNode = current.Parent as Node;
                        parentNode.StepNumber = (byte)(current.StepNumber - 1);
                        current = parentNode;
                    }
                    while (current != knight);

                    return;
                }

                current.IsVisited = true;

                foreach (Node child in current.AvailableCells)
                {
                    if (!child.IsVisited)
                    {
                        child.Parent = current;
                        child.Distance = current.Distance + 1;
                        DiscoveredCells.Enqueue(child);
                    }
                }
            }
        }
    }
}