using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integral
{
    internal class Painter
    {
        public double S = 0.0;
        public int X_MIN, X_MAX, Y_MIN, Y_MAX, ACC;
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

        public void paint()
        {
            bg.Graphics.Clear(Color.White);
            Function f = new Function(X_MIN, X_MAX, Y_MIN, Y_MAX);
            showAxes(f);

            int step = ACC;

            for (int i = 0; i <= containerSize.Width - step; i+= step)
            {

                bg.Graphics.DrawLine(pen, i, find_y(f,i), i + step, find_y(f,i + step));
            }

            for (int i = 0; i <= containerSize.Width - step; i += step)
            {
                Decarts s;
                bg.Graphics.DrawLine(pen, i, containerSize.Height/2, i, find_y(f, i));
                if(find_y(f, i) < containerSize.Height / 2 && find_y(f, i+step) < containerSize.Height / 2)
                {
                    if (find_y(f, i) > find_y(f, i + step))
                    {
                        bg.Graphics.DrawLine(pen, i, find_y(f, i), i + step, find_y(f, i));

                        s = find_y(f,i,step);
                        S += (-f.maxX + s.X) * s.Y;
                    }
                    else
                    {
                        bg.Graphics.DrawLine(pen, i, find_y(f, i + step), i + step, find_y(f, i + step));
                        
                        s = find_y(f, i, step);
                        S += (-f.maxX + s.X) * s.Y;
                    }
                }
                else if(find_y(f, i) > containerSize.Height / 2 && find_y(f, i + step) > containerSize.Height / 2)
                {
                    if (find_y(f, i) < find_y(f, i + step))
                    {
                        bg.Graphics.DrawLine(pen, i, find_y(f, i), i + step, find_y(f, i));
                        
                        s = find_y(f, i, step);
                        S += (-f.maxX + s.X) * s.Y;
                    }
                    else
                    {
                        bg.Graphics.DrawLine(pen, i, find_y(f, i + step), i + step, find_y(f, i + step));
                        
                        s = find_y(f, i, step);
                        S += (-f.maxX + s.X) * s.Y;
                    }
                }
                else
                {
                    bg.Graphics.DrawLine(pen, i, containerSize.Height / 2, i + step, containerSize.Height / 2);
                    s = find_y(f, i, step);
                    S += (-f.maxX + s.X) * s.Y;
                }
            }

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
        private Decarts find_y(Function f, int i, int x)
        {

            ScreenPoints xS = new ScreenPoints(i,x);
            Decarts xD = new Decarts(xS, ContainerSize, f);
            double y = f.func(xD.X);
            Decarts yD = new Decarts(xD.Y, y);
            return yD;
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
