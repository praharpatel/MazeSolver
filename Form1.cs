using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BlueBeamMaze
{

    public partial class Form1 : Form
    {
        //****************************GLOBALS
         bool isSolved = false;
         Bitmap bp;
         string mazeResult;
         Color green = Color.DarkGreen;
         Queue<Node> qObj = new Queue<Node>();
         int count = 0;
         int width;
         int height;
         Node[,] nodes;
        //****************************
        public Form1()
        {
            InitializeComponent();
            label2.Text= "Enter file args and click execute to start...";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            progressBar1.Value = 0;

            string[] args = textBox1.Text.Split(' ');
            init(args);
            
            progressBar1.Value = 100;
            

        }

        public void init(string[] arg)
        {
            label2.Text = "Solving is currently in Progress...";
            label2.Refresh();
            try
            {
                
                progressBar1.Increment(20);

                bp = new Bitmap(arg[0]);
                mazeResult = arg[1];

                width = bp.Width;
                height = bp.Height;
                nodes = new Node[width, height]; // every pixel in image

                
                progressBar1.Increment(20);

                findEntrance();

                
                progressBar1.Increment(20);

                findCorrectPath();

                
                progressBar1.Increment(20);

                foundPath();
                label2.Text = "Maze Solved Succesfully, Please check the .exe folder for solved maze";

            }
            catch (ArgumentException e)
            {
                Console.WriteLine("File error, file not loaded correctly. Try again");
            }

            Console.ReadLine();
        }

        public  void findEntrance()
        {
            int initX = -1;
            int initY = 0;


            //This loop goes through and initiliazes each node with a pixel and finds the entrance to the maze

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {

                    nodes[x, y] = new Node(bp.GetPixel(x, y), x, y);

                    //Node.isRed is a static method that simply checks for color of the node
                    if (Node.isRed(bp.GetPixel(x, y)))
                    {
                        if (initX == -1)
                        {
                            initX = x;
                            initY = y;
                        }
                    }

                }
            }



            nodes[initX, initY].setChecked(true);
            qObj.Enqueue(nodes[initX, initY]);
        }

        public  void findCorrectPath()
        {
            while (qObj.Count > 0)
            {
                Node currPixel = qObj.Dequeue();
                count++;

                //Solution to maze found
                if (Node.isBlue(currPixel.getColor()))
                {
                    isSolved = true;


                    while (currPixel.getancestor() != null)
                    {

                        bp.SetPixel(currPixel.getXcord(), currPixel.getYcord(), green); // set original to green


                        if (!Node.isBlack(bp.GetPixel(currPixel.getXcord() + 1, currPixel.getYcord()))) // expaned green line 
                        {
                            bp.SetPixel(currPixel.getXcord() + 1, currPixel.getYcord(), green);
                            if (!Node.isBlack(bp.GetPixel(currPixel.getXcord() + 2, currPixel.getYcord())))
                            {
                                bp.SetPixel(currPixel.getXcord() + 2, currPixel.getYcord(), green);
                            }
                        }

                        if (!Node.isBlack(bp.GetPixel(currPixel.getXcord(), currPixel.getYcord() + 1)))
                        {
                            bp.SetPixel(currPixel.getXcord(), currPixel.getYcord() + 1, green);
                            if (!Node.isBlack(bp.GetPixel(currPixel.getXcord(), currPixel.getYcord() + 2)))
                            {
                                bp.SetPixel(currPixel.getXcord(), currPixel.getYcord() + 2, green);
                            }
                        }

                        if (!Node.isBlack(bp.GetPixel(currPixel.getXcord() - 1, currPixel.getYcord())))
                        {
                            bp.SetPixel(currPixel.getXcord() - 1, currPixel.getYcord(), green);
                            if (!Node.isBlack(bp.GetPixel(currPixel.getXcord() - 2, currPixel.getYcord())))
                            {
                                bp.SetPixel(currPixel.getXcord() - 2, currPixel.getYcord(), green);
                            }
                        }

                        if (!Node.isBlack(bp.GetPixel(currPixel.getXcord(), currPixel.getYcord() - 1)))
                        {
                            bp.SetPixel(currPixel.getXcord(), currPixel.getYcord() - 1, green);
                            if (!Node.isBlack(bp.GetPixel(currPixel.getXcord(), currPixel.getYcord() - 2)))
                            {
                                bp.SetPixel(currPixel.getXcord(), currPixel.getYcord() - 2, green);
                            }
                        }


                        currPixel = currPixel.getancestor();
                    }
                    qObj.Clear();
                    break;

                }

                if (!qObj.Contains(nodes[currPixel.getXcord() + 1, currPixel.getYcord()])) // check immediate nodes on all four sides
                {
                    if (!nodes[currPixel.getXcord() + 1, currPixel.getYcord()].hasvisited())
                    {
                        nodes[currPixel.getXcord() + 1, currPixel.getYcord()].setChecked(true);
                        if (!Node.isBlack(nodes[currPixel.getXcord() + 1, currPixel.getYcord()].getColor()))
                        {
                            //The "Parent Node" is set in order to trace the path back from the solution
                            nodes[currPixel.getXcord() + 1, currPixel.getYcord()].setancestor(currPixel);
                            qObj.Enqueue(nodes[currPixel.getXcord() + 1, currPixel.getYcord()]);
                        }
                    }
                }

                if (!qObj.Contains(nodes[currPixel.getXcord(), currPixel.getYcord() + 1]))
                {
                    if (!nodes[currPixel.getXcord(), currPixel.getYcord() + 1].hasvisited())
                    {
                        nodes[currPixel.getXcord(), currPixel.getYcord() + 1].setChecked(true);
                        if (!Node.isBlack(nodes[currPixel.getXcord(), currPixel.getYcord() + 1].getColor()))
                        {

                            nodes[currPixel.getXcord(), currPixel.getYcord() + 1].setancestor(currPixel);
                            qObj.Enqueue(nodes[currPixel.getXcord(), currPixel.getYcord() + 1]);
                        }
                    }
                }

                if (!qObj.Contains(nodes[currPixel.getXcord() - 1, currPixel.getYcord()]))
                {
                    if (!nodes[currPixel.getXcord() - 1, currPixel.getYcord()].hasvisited())
                    {
                        nodes[currPixel.getXcord() - 1, currPixel.getYcord()].setChecked(true);
                        if (!Node.isBlack(nodes[currPixel.getXcord() - 1, currPixel.getYcord()].getColor()))
                        {
                            nodes[currPixel.getXcord() - 1, currPixel.getYcord()].setancestor(currPixel);
                            qObj.Enqueue(nodes[currPixel.getXcord() - 1, currPixel.getYcord()]);
                        }
                    }
                }

                if (!qObj.Contains(nodes[currPixel.getXcord(), currPixel.getYcord() - 1]))
                {
                    if (!nodes[currPixel.getXcord(), currPixel.getYcord() - 1].hasvisited())
                    {
                        nodes[currPixel.getXcord(), currPixel.getYcord() - 1].setChecked(true);
                        if (!Node.isBlack(nodes[currPixel.getXcord(), currPixel.getYcord() - 1].getColor()))
                        {
                            nodes[currPixel.getXcord(), currPixel.getYcord() - 1].setancestor(currPixel);
                            qObj.Enqueue(nodes[currPixel.getXcord(), currPixel.getYcord() - 1]);
                        }
                    }
                }

            }
        }

        public  void foundPath()
        {
            if (isSolved)
            {
                bp.Save(mazeResult);

            }
            else
            {
                System.Environment.Exit(1);

            }
        }
    }
}
