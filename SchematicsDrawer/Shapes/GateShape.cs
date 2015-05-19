using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SatSolver.Objects.Gates;

namespace SatSolver.SchematicsDrawer.Shapes
{
    public sealed class GateShape
    {
        private readonly Gate _gate;
        private readonly PaintEventArgs P;

        /// <summary>
        /// Ratio of painted objects compared to the container size
        /// </summary>
        private float _zoomFactor = 1;

        private Point StartPoint { get; set; }
        private Size ShapeSize { get; set; }
        private Rectangle DrawingRect { get; set; }

        private Bitmap _imageBuffer;


        /// <summary>
        /// Color of the shape
        /// </summary>
        protected Color ShapeColor;

        /// <summary>
        /// Type of the gate to draw
        /// </summary>
        protected GateType GateType;

        public PointF PortInA {get; private set;}
        public PointF PortInB { get; private set; }
        public PointF PortOut { get; private set; }

        public GateShape(Gate gate, int x, int y, float zoomFactor, PictureBox box, PaintEventArgs p)
        {
            _gate = gate;
            P = p;
            StartPoint = new Point(x, y);
            ShapeSize = new Size(20 , 20);
            Draw();
        }

        /// <summary>
        /// Draw the intended shape of gate on the given surface
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public GateShape Draw()
        {

            #if DEBUG
            Debug.WriteLine("Drawing gate '" + _gate.GetGateType() + "' with symbol '" + _gate.GetSymbol() + "'");
            #endif

            Pen pen = new Pen(Color.Red);
            DrawingRect = new Rectangle(StartPoint.X, StartPoint.Y, ShapeSize.Width, ShapeSize.Height);
            P.Graphics.DrawRectangle(pen, DrawingRect);

            StringFormat sf = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            };
            using(Font font = new Font(FontFamily.GenericMonospace, 8))
            P.Graphics.DrawString(_gate.GetSymbol(), font, Brushes.Black, DrawingRect, sf);

            if (_gate.IsSingleInput())
            {
                PortInA = new PointF(DrawingRect.X, StartPoint.Y + DrawingRect.Height / 2f);
                PortInB = new PointF(0,0);
            }
            else
            {
                PortInA = new PointF(DrawingRect.X, StartPoint.Y + DrawingRect.Height / 4f);
                PortInB = new PointF(DrawingRect.X, StartPoint.Y + (DrawingRect.Height/1.3f));
            }  

            PortOut = new PointF(DrawingRect.X + DrawingRect.Width, StartPoint.Y + (DrawingRect.Height / 2f));

            return this;
        }

        public Rectangle GetDrawingRectangle()
        {
            return DrawingRect;
        }

        public Point GetTopRight()
        {
            return new Point(DrawingRect.Width, DrawingRect.Top);
        }

        public Gate GetAssignedGate()
        {
            return _gate;
        }
    }
}
