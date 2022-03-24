
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DialougeEditor
{
    internal class SocketVisual
    {
        public const float SocketHeight = 16;

        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public string Name { get; set; }
        public Type Type { get; set; }
        public bool Input { get; set; }
        public object Value { get; set; }
        public bool IsMainExecution { get; set; }

        public bool IsExecution
        {
            get { return Type.Name.Replace("&", "") == typeof(ExecutionPath).Name; }
        }

        public void Draw(Graphics g, Point mouseLocation, MouseButtons mousebuttons)
        {
            var socketRect = new RectangleF(X, Y, Width, Height);
            var hover = socketRect.Contains(mouseLocation);
            var fontbrush = Brushes.Black;

            if (hover)
            {
                socketRect.Inflate(4, 4);
                fontbrush = Brushes.Blue;
            }

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;

            if (Input)
            {
                var sf = new StringFormat();
                sf.Alignment = StringAlignment.Near;
                sf.LineAlignment = StringAlignment.Center;
                g.DrawString(Name, SystemFonts.SmallCaptionFont, fontbrush, new RectangleF(X + Width + 2,Y, 1000, Height), sf);
            }
            else
            {
                var sf = new StringFormat();
                sf.Alignment = StringAlignment.Far;
                sf.LineAlignment = StringAlignment.Center;
                g.DrawString(Name, SystemFonts.SmallCaptionFont, fontbrush, new RectangleF(X - 1000, Y, 1000, Height), sf);
            }

            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            if (IsExecution)
            {
                g.DrawImage(Resources.exec, socketRect);
            }
            else
            {
                g.DrawImage(Resources.Socket, socketRect);
            }
        }

        public RectangleF GetBounds()
        {
            return new RectangleF(X, Y, Width, Height);
        }
    }
}
