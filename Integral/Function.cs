using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integral
{
    internal class Function
    {
        //private int a, b;
        //public int A { get { return a; } set { a = value; } }
        //public int B { get { return b; } set { b = value; } }
        //Decarts p;
        public double minY, maxY, minX, maxX;

     

        public Function(double minY = -100, double maxY = 100, double minX = -100, double maxX = 100)
        {
            this.minY = minY;
            this.maxY = maxY;
            this.minX = minX;
            this.maxX = maxX;
        }

        public double func(double x)
        {
            return x * x;
        }
        public Decarts point(double x)
        {
            return new Decarts(x, func(x));
        }

    }
}
