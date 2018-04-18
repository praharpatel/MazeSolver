using System;
using System.Drawing;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlueBeamMaze
{
    public class Node
    {
        private Color color;
        private Node ancestor;
        private int Xcord;
        private int Ycord;
        private bool visited;

        public Node(Color color, int x, int y)
        {
            ancestor = null;
            this.color = color;
            Xcord = x;
            Ycord = y;
            visited = false;
        }

        public Color getColor()
        {
            return color;
        }

        public Node getancestor()
        {
            return ancestor;
        }

        public int getXcord()
        {
            return Xcord;
        }

        public int getYcord()
        {
            return Ycord;
        }

        public void setancestor(Node node)
        {
            ancestor = node;
        }

        public void setColor(Color color)
        {
            this.color = color;

        }

        public void setChecked(bool check)
        {
            visited = check;
        }

        public bool hasvisited()
        {
            return visited;
        }


        
     
        public static bool isBlack(Color color)
        {
            int redDifference = 0 - color.R;
            int greenDifference = 0 - color.G;
            int blueDifference = 0 - color.B;

            if (Math.Abs(redDifference) + Math.Abs(greenDifference) + Math.Abs(blueDifference) <= 150)
                return true;
            else
                return false;
        }

        public static bool isBlue(Color color)
        {
            int redDifference = 0 - color.R;
            int greenDifference = 0 - color.G;
            int blueDifference = 255 - color.B;

            if (Math.Abs(redDifference) + Math.Abs(greenDifference) + Math.Abs(blueDifference) <= 180)
                return true;
            else
                return false;
        }

        public static bool isRed(Color color)
        {
            int redDifference = 255 - color.R;
            int greenDifference = 0 - color.G;
            int blueDifference = 0 - color.B;

            if (Math.Abs(redDifference) + Math.Abs(greenDifference) + Math.Abs(blueDifference) <= 180)
                return true;
            else
                return false;
        }
    }
}
