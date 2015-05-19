using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Windows.Forms;
using MetroFramework.Controls;
using SatSolver.Objects;
using SatSolver.Objects.Gates;
using SatSolver.SchematicsDrawer.Shapes;

namespace SatSolver.SchematicsDrawer
{
    public partial class ShematicControl : MetroUserControl
    {
        //minimum space from left side of box
        private const int MinDistanceFromLeft = 20;
        //minimum space from top side of box
        private const int MinDistanceFromTop = 20;
        private float _zoomFactor = 1;
        private int _spaceBetweenGates = 20;

        private Circuit _circuit;
        private List<GateShape> _drawnGates;
        private Point _drawingPoint = new Point(MinDistanceFromLeft, MinDistanceFromTop);

        public ShematicControl()
        {
            InitializeComponent();

            Debug.WriteLine("_zoomFactor is now " + _zoomFactor);

            box.Paint += Draw; //draw grid

            box.MouseEnter += (sender, args) => { box.Focus(); };

            box.MouseWheel += (sender, args) =>
            { 
                if (args.Delta > 0)
                {
                    _zoomFactor += 0.1f;
                } 
                else
                {
                    _zoomFactor -= 0.1f;
                }

                if (_zoomFactor <= 0)
                    _zoomFactor = 1f;
               

                Debug.WriteLine("_zoomFactor is now " + _zoomFactor);
            };
        }

        private void Draw(object sender, PaintEventArgs p)
        {
            p.Graphics.ScaleTransform(_zoomFactor, _zoomFactor);

            _drawingPoint = new Point(MinDistanceFromLeft, MinDistanceFromTop);
            DrawGrid(p);
            if (_circuit != null && _circuit.GetGatesCount() > 0)
            {
                DrawCircuit(p);
                DrawNetConnections(p);
            }

        }

        private void DrawNetConnections(PaintEventArgs p)
        {   
            GateShape[] shapes = _drawnGates.ToArray();
            GateShape cs; //current shape
            GateShape ns = null; //next shape
            Gate cg; //current gate
            Gate ng; //next gate
            Graphics g = p.Graphics;
            for (int i = 0; i < shapes.Length - 1; i++)
            {

                Pen pen = new Pen(Color.Red, 2);
                pen.ScaleTransform(_zoomFactor, _zoomFactor);
                pen.Brush = new SolidBrush(Color.Green);
                

                cs = shapes[i];
                ns = shapes[i + 1];
                cg = cs.GetAssignedGate();
                ng = ns.GetAssignedGate();

                //Connections of firts gate
                PointF pi1; //point for start of input 1
                PointF pi2; //point for start of input 2
                PointF po; //point for output of current gate
                if (i == 0)
                {
                    pen.Color = Color.Blue;
                    if (cg.IsSingleInput()) //if has onlye 1 input e.g. inv gate
                    {
                        pi1 = new PointF(5, cs.PortInA.Y );
                        pi2 = new PointF(0, 0);
                        g.DrawLine(pen, pi1, cs.PortInA);
                    }
                    else
                    {
                        pi1 = new PointF(5, cs.PortInA.Y);
                        pi2 = new PointF(5, cs.PortInB.Y);
                        g.DrawLine(pen, pi1, cs.PortInA);
                        g.DrawLine(pen, pi2, cs.PortInB);
                    }

                    pen.Color = Color.Orange;
                    g.DrawLine(pen, cs.PortOut, ns.PortInA);
                }
                else
                {
                    pen.Color = Color.SaddleBrown;
                    if (cg.IsSingleInput()) //if has onlye 1 input e.g. inv gate
                    {
                        
                        pi1 = cs.PortOut;
                        pi2 = new PointF(50, 50);
                        g.DrawLine(pen, pi1, ns.PortOut);
                    }
                    else
                    {
                        pi1 = ns.PortInA;
                        pi2 = ns.PortInB;
                        g.DrawLine(pen, pi1, cs.PortInA);
                        g.DrawLine(pen, pi2, cs.PortInB);
                    }

                    pen.Color = Color.Orange;
                    g.DrawLine(pen, cs.PortOut, ns.PortInA);

                }
            }

        }

        private void DrawCircuit(PaintEventArgs p)
        {
            _drawnGates = new List<GateShape>();
            //keep trak of how many gates are drawn in x axis, if near left side of
            //the control, move to next line!
            float spaceTaken = MinDistanceFromLeft;
            //keep track of how many rows are drawn so far
            int rowsTaken = 1;
            int newHeight = _drawingPoint.Y;

            _spaceBetweenGates = 30;

            Gate[] gates = _circuit.GetGates().ToArray();
            for (int i = 0; i < gates.Length; i++)
            {
                var gate = gates[i];
                //construct shape object for each gate
                GateShape shape = new GateShape(gate, _drawingPoint.X, newHeight, _zoomFactor, box, p);
                shape.Draw();

                //calculate positon of next gate

                spaceTaken += (shape.GetDrawingRectangle().Width + _spaceBetweenGates);


                if (spaceTaken >
                    box.Width - (_spaceBetweenGates - shape.GetDrawingRectangle().Width)*p.Graphics.PageScale)
                {
                    //move to new row
                    rowsTaken++;
                    newHeight = (rowsTaken*MinDistanceFromTop) + ((rowsTaken - 1)*shape.GetDrawingRectangle().Height);

                    _drawingPoint = new Point(MinDistanceFromLeft, newHeight);
                    spaceTaken = MinDistanceFromLeft;
                }
                else
                    _drawingPoint =
                        new Point((_drawingPoint.X += _spaceBetweenGates) + shape.GetDrawingRectangle().Width,
                            newHeight);

                 _drawnGates.Add(shape);
            }
        }

        private void DrawGrid(PaintEventArgs p)
        {
            int gridWidth = 10;
            Pen pen = new Pen(Color.LightGray);
            int h = box.Height;
            int w = box.Width;

            int numVertLines = w/gridWidth;
            int numHorLines = h/gridWidth;
            int x = 0;
            int y = h;


            for (int i = 1; i < numVertLines; i++)
            {
                p.Graphics.DrawLine(pen, x, 0, x, h);
                x += gridWidth;
            }

            x = y = 0;
            for (int i = 1; i < numHorLines; i++)
            {
                p.Graphics.DrawLine(pen, 0, y + gridWidth, w, y + gridWidth);
                y += gridWidth;
            }

            pen.Dispose();
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }

        private void ShematicControl_Load(object sender, EventArgs e)
        {
            
        }

        public void SetCircuit(Circuit circuit, int treeId)
        {   
            _circuit = circuit;
            _drawnGates = new List<GateShape>();

            box.Invalidate();
        }

    }
}
