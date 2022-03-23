using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DialougeEditor
{
    class ControlMover
    {

        public enum Direction
        {
            Any,
            Horizontal,
            Vertical
        }

        public static void Init(Control control)
        {
            Init(control, Direction.Any);
        }

        public static void Init(Control control, Direction direction)
        {
            Init(control,control, Direction.Any);
        }

        public static void Init(Control control,Control container, Direction direction)
        {
            bool Dragging = false;
            Point DraggingStart = Point.Empty;

            control.MouseDown += delegate (object sender, MouseEventArgs e)
            {
                Dragging = true;
                DraggingStart = new Point(e.X, e.Y);
                control.Capture = true;
                System.Diagnostics.Debug.WriteLine("Pressed Down");
            };

            control.MouseUp += delegate (object sender, MouseEventArgs e)
            {
                System.Diagnostics.Debug.WriteLine("Pressed Up");
                Dragging = false;
                control.Capture = false;
            };

            control.MouseMove += delegate (object sender, MouseEventArgs e)
            {
                System.Diagnostics.Debug.WriteLine("Moving");
                if (Dragging)
                {
                    if (direction != Direction.Vertical)
                    {
                        container.Left = Math.Max(0, e.X + container.Left - DraggingStart.X);
                    }
                    if (direction != Direction.Horizontal)
                    {
                        container.Top = Math.Max(0, e.Y + container.Top - DraggingStart.Y);
                    }
                }
            };
        }

    }
}
