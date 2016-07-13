using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scrib
{

    public enum Style
    {
        Dots,
        Cursive,
        Rectangles,
        Circles,
        Fan

    }
    
    public class Stroke
    {
        public List<Point> points=new List<Point>();
        
        public Color color=Color.Red;
        public int size;
        public Style style;
        public int width;
        
    }
}
