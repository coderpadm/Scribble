using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Scrib
{
    public partial class Form1 : Form
    {
        //  Point mouse; - for single point

        List<Stroke> strokes = new List<Stroke>();
        Stack<Stroke> redo = new Stack<Stroke>();
        Style currentStyle = Style.Rectangles;
        Color currentColor = Color.Bisque;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            /*
            //For single point
            if(mouse!=null)
            {
                e.Graphics.DrawRectangle(Pens.Green, 10, 20, 100, 50);
            }
            */

            //For multiple points - to illustrate perfomance issues

            foreach (var currentStyle in Enum.GetValues(typeof(Style)))
            {
                Iterate(e, (Style)currentStyle);
            }

        }

        private void Iterate(PaintEventArgs e, Style currentStyle)
        {
            foreach (Stroke stroke in strokes)
            {
                stroke.style = currentStyle;
                if (stroke.style == Style.Rectangles)
                {
                    PaintRectangles(stroke, e.Graphics);

                }
                else if (stroke.style == Style.Circles)
                {

                    PaintCircles(stroke, e.Graphics);
                }
                else
                {
                    Debug.WriteLine(stroke.style);
                    PaintRandom(stroke, e.Graphics);

                }


            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            /*
            //For one location
            mouse = e.Location;
            */
            //Atleast one mousebutton is used
            if (e.Button != MouseButtons.None && strokes.Count > 0)
            {
                //For multiple points
                strokes[strokes.Count - 1].points.Add(e.Location);
                redo.Clear();
                //Please call paint as soon as possible
                Invalidate();
            }
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            strokes.Add(new Stroke());
            Form1_MouseMove(sender, e);
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            //For ctrl+z - undo
            if (e.Control && e.KeyCode == Keys.Z)
            {
                Undo();
            }
            //For ctrl+y -Redo
            else if (e.Control && e.KeyCode == Keys.Y)
            {
                Redo();
            }
            else if (e.KeyCode == Keys.C)
            {
                currentStyle = Style.Cursive;
            }

        }

        private void PaintRectangles(Stroke stroke, Graphics g)
        {
            foreach (Point point in stroke.points)
            {

                currentColor = Color.Orange;
                Pen pen = new Pen(currentColor, 2);

                g.DrawRectangle(pen, point.X, point.Y, 10, 10);

            }
        }

        private static Color SetCurrColor(Point point)
        {
            return Color.FromArgb(Math.Abs(point.X % 255), Math.Abs(point.Y % 255), 128);
        }

        private void PaintCircles(Stroke stroke, Graphics g)
        {

            foreach (Point point in stroke.points)
            {
                Color currentColor = SetCurrColor(point);
                Pen pen = new Pen(currentColor, 2);

                g.DrawEllipse(pen, new Rectangle(0, 0, point.X, point.X));
            }

        }
        private void PaintRandom(Stroke stroke, Graphics g)
        {
            Color currentColor = Color.DarkBlue;
            Pen pen = new Pen(currentColor, 2);
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.DashDotDot;


            int count = stroke.points.Count;



            Point p = new Point(0, 0);
            for (int i = 0; i < count; i++)
            {


                currentColor = SetCurrColor(stroke.points[i]);
                g.DrawLine(pen, p, stroke.points[i]);
            }


        }


        private void Redo()
        {
            if (redo.Count > 0)
            {
                strokes.Add(redo.Pop());
                Invalidate();
            }
        }

        private void Undo()
        {
            if (strokes.Count > 0)
            {
                redo.Push(strokes[strokes.Count - 1]);
                strokes.RemoveAt(strokes.Count - 1);
                Invalidate();
            }

        }
    }
}
