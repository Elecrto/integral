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

            bg.Render(mainGraphics);
            

            //for (int i = eps; i <= containerSize.Width - eps; i+=eps)
            //{

            //mainGraphics.DrawLine(pen, new Point(1,2), new Point(100, 200));
            //}
            //та самая последняя линия - не забыть!

        }
    }
}
