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

        private PointF StartPoint { get; set; }
        private SizeF ShapeSize { get; set; }
        private RectangleF DrawingRect { get; set; }

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
            _zoomFactor = zoomFactor;
            //Draw();
        }

        public GateShape(Gate gate, PointF dPoint, float zoomFactor, PictureBox box, PaintEventArgs p)
        {
            _gate = gate;
            P = p;
            StartPoint = dPoint;
            ShapeSize = new SizeF(20 + (20 * _zoomFactor), 20 + (20 * _zoomFactor));
            
            _zoomFactor = zoomFactor;
            //Draw();
            
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

            Color color;
            if (_gate.IsTopLevelGate())
            {
                color = Color.Blue;
            }
            else if (_gate.IsLastOutputGate())
            {
                color = Color.Blue;
            }
            else
            {
                color = Color.Red; //for middle gates
            }


            Pen pen = new Pen(color, 3);
            DrawingRect = new RectangleF(StartPoint.X, StartPoint.Y, ShapeSize.Width, ShapeSize.Height);
            P.Graphics.DrawRectangle(pen, DrawingRect.X, DrawingRect.Y, DrawingRect.Width, DrawingRect.Height);

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

        public RectangleF GetDrawingRectangle()
        {
            return DrawingRect;
        }

        public PointF GetTopRight()
        {
            return new PointF(DrawingRect.Width, DrawingRect.Top);
        }

        public Gate GetAssignedGate()
        {
            return _gate;
        }
    }
}
