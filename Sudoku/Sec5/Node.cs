using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Sec5
{
    class Node
    {
        public int[,] State { get; set; }
        public int Depth { get; set; }
        public int PathCost { get; set; }
        public Node Parent { get; set; }

        // root constructor
        public Node(int[,] level)
        {
            this.State = level.Clone() as int[,];
            this.Depth = 0;
            this.PathCost = 0;
            this.Parent = null;
        }
        // child constructor
        public Node(Node current)
        {
            this.State = current.State.Clone() as int[,];
            this.Depth = current.Depth + 1;
            this.PathCost = current.PathCost + 1;
            this.Parent = current;
        }

        // returns all possible childerns of current node with some conditions 
        public List<Node> AddChildren()
        {
            List<Node> Childrens = new List<Node>();

            for (byte row = 0; row < 9; row++)
            {
                for (byte col = 0; col < 9; col++)
                {   
                    // if this box is not empty
                    if (this.State[row, col] ==  0)
                    {
                        for (byte label = 1; label < 10; label++)
                        {
                            // Check that this value has not already be used on this row &
                            // Check that this value has not already be used on this column &
                            // Check that this value has not already be used on this 3x3 square
                            if (!(findInRow(row, label)) &
                                !(findInCol(col, label)) &
                                !(findInSquare(row, col, label)))
                            {
                                Childrens.Insert(0, new Node(this));
                                Childrens[0].State[row, col] = label;

                            }

                        }
                        return Childrens;
                    }
                }
            }
            return Childrens;
        }


        Boolean findInRow(int r, byte label)
        {
            for (byte c = 0; c < 9; c++)
            {
                if (this.State[r, c] == label)
                {
                    return true;
                }
            }
            return false;
        }
        Boolean findInCol(int c, byte label)
        {
            for (byte r = 0; r < 9; r++)
            {
                if (this.State[r, c] == label)
                {
                    return true;
                }
            }
            return false;
        }
        Boolean findInSquare(byte r, byte c, byte label)
        {
            byte delta_i = 0, delta_j = 0;

            if (r < 3)
            {
                delta_i = 0; // 0 1 2
            }
            else if (r < 6)
            {
                delta_i = 1; // 3 4 5
            }
            else if (r < 9)
            {
                delta_i = 2; // 6 7 8
            }
            //~~~~~~~~~~~~~~~
            if (c < 3)
            {
                delta_j = 0; // 0 1 2
            }
            else if (c < 6)
            {
                delta_j = 1; // 3 4 5
            }
            else if (c < 9)
            {
                delta_j = 2; // 6 7 8
            }

            for (byte i = 0; i < 3; i++)
            {
                for (byte j = 0; j < 3; j++)
                {
                    if (this.State[(3 * delta_i) + i, (3 * delta_j) + j] == label)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

    }
}
