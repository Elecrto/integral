using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integral
{
    internal class Painter
    {
        private object locker = new();
        private Size containerSize;
        private Thread t;
        private Graphics mainGraphics;
        private BufferedGraphics bg;

        Pen pen = new Pen(Color.Black);

        public Graphics MainGraphics
        {
            get => mainGraphics;
            set
            {
                lock (locker)
                {
                    mainGraphics = value;
                    ContainerSize = mainGraphics.VisibleClipBounds.Size.ToSize();
                    bg = BufferedGraphicsManager.Current.Allocate(
                        mainGraphics, new Rectangle(new Point(0, 0), ContainerSize)
                    );
                }
            }
        }

        public Size ContainerSize
        {
            get => containerSize;
            set
            {
                containerSize = value;
            }
        }

        public Painter(Graphics mainGraphics)
        {
            this.MainGraphics = mainGraphics;
        }

        public void paint() {

            bg.Graphics.Clear(Color.White);

            //ScreenPoints eps = new ScreenPoints(2);
            int eps = 2;
            //Function f = new Function(-2, 2);

            //Pen pen = new Pen(Color.Black);





            Function f = new Function(-10, 10, -10, 10);
            showAxes(f);

            int step = 10;

            for (int i = 0; i <= containerSize.Width - step; i+= step)
            {

                bg.Graphics.DrawLine(pen, i, find_y(f,i), i + step, find_y(f,i + step));
            }
            //та самая последняя линия - не забыть!

            bg.Graphics.DrawLine(pen, containerSize.Width - step, find_y(f, containerSize.Width - step),
                containerSize.Width, find_y(f, containerSize.Width));
            bg.Render();
        }

        private int find_y(Function f, int i)
        {
            
            ScreenPoints xS = new ScreenPoints(i);
            Decarts xD = new Decarts(xS, ContainerSize, f);
            double y = f.func(xD.X);
            Decarts yD = new Decarts(0, y);
            ScreenPoints yS = new ScreenPoints(yD, ContainerSize, f);
            return yS.Y;
        }

        private void showAxes(Function f)
        {
            bg.Graphics.DrawLine(pen,  - Convert.ToInt32(containerSize.Width * f.minX / (f.maxX-f.minX)), 0,
                - Convert.ToInt32(containerSize.Width * f.minX / (f.maxX - f.minX)), containerSize.Height);
            bg.Graphics.DrawLine(pen, 0, Convert.ToInt32(containerSize.Height * f.maxY / (f.maxY - f.minY)), 
                containerSize.Width, Convert.ToInt32(containerSize.Height * f.maxY / (f.maxY - f.minY)));
        }
    }
}
