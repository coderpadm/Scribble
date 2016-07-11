using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scrib
{
    public partial class Form1 : Form
    {
        Point mouse;

        List<Point> points = new List<Point>();
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            //For single point
            if(mouse!=null)
            {
                e.Graphics.DrawRectangle(Pens.Green, 10, 20, 100, 50);
            }

            //For multiple points - to illustrate perfomance issues
            foreach (Point point in points)
            {
                e.Graphics.DrawRectangle(Pens.Green, point.X, point.Y, 100, 50);
            }

        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            //For one location
            mouse = e.Location;

            //For multiple points
            points.Add(e.Location);

            //Please call paint as soon as possible
            Invalidate();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
