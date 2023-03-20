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
            Function f = new Function(-2, 2);

            Pen pen = new Pen(Color.Black);



            bg.Graphics.DrawLine(pen, containerSize.Width / 2, 0, containerSize.Width / 2, containerSize.Height);
            bg.Graphics.DrawLine(pen, 0, containerSize.Height / 2, containerSize.Width, containerSize.Height / 2);

            

            int step = 10;

            for (int i = 0; i <= containerSize.Width - step; i+= step)
            {

                bg.Graphics.DrawLine(pen, i, find_y(i), i + step, find_y(i + step));
            }
            //та самая последняя линия - не забыть!

            bg.Render(mainGraphics);
        }

        private int find_y(int i)
        {
            Function f = new Function();

            ScreenPoints xS = new ScreenPoints(i);
            Decarts xD = new Decarts(xS, ContainerSize);
            double y = f.func(xD.X);
            Decarts yD = new Decarts(0, y);
            ScreenPoints yS = new ScreenPoints(yD, ContainerSize);
            return yS.Y;
        }
    }
}
