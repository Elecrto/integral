using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integral
{
    internal class Decarts
    {
		private double x, y;
		public double X {get { return x; }set { x = value; }}
        public double Y { get { return y; } set { y = value; } }

        public Decarts(double a = 0, double b =0) {
            this.x = a;
            this.y = b;
        }

        public Decarts(ScreenPoints pointS, Size panel_size)
        {
            double minY = -10;
            double maxY = 10;
            double indent = panel_size.Height / (maxY - minY);
            this.Y = maxY - pointS.Y * indent;
            double minX, maxX;
            maxX = panel_size.Width / (indent * 2);
            minX = -maxX;
            this.X = maxX - pointS.X * indent;
        }
    }
}
