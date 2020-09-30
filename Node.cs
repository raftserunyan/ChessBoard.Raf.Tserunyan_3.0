using System;
using System.Collections.Generic;

namespace ChessBoard.Raf.Tserunyan_3._0
{
    public class Node
    {
        public static Board board;

        public int I { get; set; }
        public int J { get; set; }
        public bool IsVisited { get; set; } = false;
        public int Distance { get; set; }
        public Node Parent { get; set; }
        public byte StepNumber;
        public string Name = " ";
        public bool IsPartOfTheWay = false;

        /// <summary>
        /// A list of cells which can the knight step on.
        /// </summary>
        public List<object> AvailableCells = new List<object>();

        /// <summary>
        /// Initializing 'available' cells for the node.
        /// </summary>
        public void SetAvailableCells()
        {
            AvailableCells.Clear();

            for (int i = I - 2; i <= I + 2; i++)
            {
                if (i < 0)
                    continue;
                else if (i > 7)
                    break;

                for (int j = J - 2; j <= J + 2; j++)
                {
                    if (j < 0)
                        continue;
                    else if (j > 7)
                        break;

                    if (Math.Abs(I - i) + Math.Abs(J - j) == 3)
                    {
                        AvailableCells.Add(board[i, j]);
                    }
                }
            }
        }

        /// <summary>
        /// Creates a new instance of node.
        /// </summary>
        /// <param name="_i">Node's row number</param>
        /// <param name="_j">Node's column number</param>
        public Node(int _i, int _j)
        {
            I = _i;
            J = _j;
            SetAvailableCells();
        }

        /// <summary>
        /// Creates a new instance of node.
        /// </summary>
        /// <param name="coord">Coordinates for the node.</param>
        public Node(string coord)
        {
            Name = "Knight";
            try
            {
                I = 8 - byte.Parse(coord.Substring(0, 1));
                J = Convert.ToByte(Convert.ToChar(coord.Substring(2, 1).ToUpper())) - 65;
            }
            catch (Exception)
            {
                throw new Exception("Your input was not in correct format, try again...");
            }
            SetAvailableCells();

            board[I, J] = this;
        }

        /// <summary>
        /// Returns the first symbol of the cell's name.
        /// </summary>
        /// <returns>First symbol of the cell's name</returns>
        public override string ToString()
        {
            return Name.Substring(0,1);
        }
    }
}
