using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sec5
{
    public partial class Sudoku : Form
    {
        public Sudoku()
        {
            InitializeComponent();
            init();


        }

        int[,] level = Levels.easy;
        TextBox[,] textBoxes = new TextBox[9, 9];
        string[] labels = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", };
        List<int[,]> Solution = new List<int[,]>();
        Node CurrentNode;

        void init()
        {
            var allBoxes = Controls.OfType<TextBox>();
            var boxes = allBoxes.ToArray();
            //TextBox[,] textBoxes = new TextBox[9, 9]; 

            for (byte i = 0; i < 9; i++)
            {
                for (byte j = 0; j < 9; j++)
                {
                    textBoxes[i, j] = boxes[((9 * i) + j)];
                    textBoxes[i, j].TextChanged += new System.EventHandler(this.TextChange);
                }
            }
            nodeToState(level);
            richTextBox2.Text = "";
            richTextBox1.Text = "";
            CurrentNode = null;
            List<int[,]> Solution = new List<int[,]>();
        }

        private void reset(object sender, EventArgs e)
        {
            init();
        }

        private void TextChange(object sender, EventArgs e)
        {
            // our labels or empty
            string res = Array.Find(labels, s => s.Equals((sender as TextBox).Text));
            if (res == null)
                (sender as TextBox).Text = "";

        }
  

        private void Solveit(object sender, EventArgs e)
        {
            Boolean solved = SolveUsing_DFS();
            if (solved)
            {
                richTextBox1.Text =
                    "Depth:  " + Solution.Count() + " (Steps)" +"\n" +
                    "Cost:    " + Solution.Count() * 1;
                
                foreach(var step in Solution)
                {
                    for(byte row = 0; row < 9; row++)
                    {
                        for (byte col = 0; col < 9; col++)
                        {
                            richTextBox2.Text += step[row,col] + "  ";
                        }
                        richTextBox2.Text += "\n";
                    }
                    richTextBox2.Text += "\n\n";
                }

                //MessageBox.Show("solved");
            }
            else
            {
                MessageBox.Show("can't");
            }
        }
       
        //========================== depth first algorithm ============================
        private Boolean SolveUsing_DFS()
        {
            Stack<Node> DFS_Stack = new Stack<Node>();
            Node InitialState = new Node(level);
            DFS_Stack.Push(InitialState);

            while (DFS_Stack.Count != 0)
            {
                //remove
                CurrentNode = DFS_Stack.Pop();
                
                //check goal
                if ( isComplete(CurrentNode.State))
                {
                    //write it to screen
                    nodeToState(CurrentNode.State);
                    // return solution
                    while (CurrentNode != null)
                    {
                        Solution.Insert(0, CurrentNode.State);
                        CurrentNode = CurrentNode.Parent;
                    }
                    
                    return true;
                }
                //add children
                if (CurrentNode.Depth == 100)
                {
                    continue;
                }
                foreach (var child in CurrentNode.AddChildren())//<5,1>
                {
                    DFS_Stack.Push(child);
                }

            }

            return false;
        }
        //======================================================


        void nodeToState(int[,] state)
        {
            for (byte row = 0; row < 9; row++)
            {
                for (byte col = 0; col < 9; col++)
                {
                    textBoxes[8-row , 8-col].Text = state[row, col].ToString(); // state[row, col].ToString();
                }
            } 
        }
        Boolean isComplete(int[,] state)
        {
            for (byte row = 0; row < 9; row++)
            {
                for (byte col = 0; col < 9; col++)
                {
                    if (state[row, col] == 0)
                        return false;
                }
            }
            // if it is complete
            return true;
        }
    }
}
