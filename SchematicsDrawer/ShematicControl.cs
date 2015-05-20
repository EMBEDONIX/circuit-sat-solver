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
        private const int MinDistanceFromLeft = 50;
        //minimum space from top side of box
        private const int MinDistanceFromTop = 50;
        private float _zoomFactor = 1;
        private int _spaceBetweenGates = 20;

        private Circuit _circuit;
        private List<GateShape> _drawnShapes;
        private Point _dPoint = new Point(MinDistanceFromLeft, MinDistanceFromTop);

        public ShematicControl()
        {
            InitializeComponent();

            Debug.WriteLine("_zoomFactor is now " + _zoomFactor);

            //Pain event
            box.Paint += Draw; //draw grid

            //capture focus when mouse enters in box
            box.MouseEnter += (sender, args) => { box.Focus(); };

            //Handle zoom using mouse wheel
            box.MouseWheel += (sender, args) =>
            { 
                if (args.Delta > 0)
                {
                    _zoomFactor += 0.2f;
                } 
                else
                {
                    _zoomFactor -= 0.2f;
                }

                if (_zoomFactor <= 0)
                    _zoomFactor = 1f; 

                Debug.WriteLine("_zoomFactor is now " + _zoomFactor);
            };
        }

        private void Draw(object sender, PaintEventArgs p)
        {
            p.Graphics.ScaleTransform(_zoomFactor, _zoomFactor);

            _dPoint = new Point(MinDistanceFromLeft, MinDistanceFromTop);
            DrawGrid(p);
            if (_circuit != null && _circuit.GetGatesCount() > 0)
            {
                DrawCircuit(p);
                DrawNetConnections(p);
            }

        }

        private void DrawNetConnections(PaintEventArgs p)
        {   
            GateShape[] shapes = _drawnShapes.ToArray();
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
            GateShape shape = null;
            //space between gates both vertical and horizontal
            _spaceBetweenGates = 30;
            //after each gate is drawn its shape should go in this list for further use!
            _drawnShapes = new List<GateShape>();
            //keep trak of how many gates are drawn in x axis, if near left side of
            //the control, move to next line!
            float spaceTaken = MinDistanceFromLeft;
            //keep track of how many rows are drawn so far
            int rowsTaken = 1;

            //First of all, the input gates must be drawn in a column
            var inGates = _circuit.GetInputGates();
            for (int i = 0; i < inGates.Count; i++)
            {
                //GateShape shape = new GateShape(inGates[i], _dPoint.X, _dPoint.Y, _zoomFactor, box, p);
                shape = new GateShape(inGates[i], _dPoint, _zoomFactor, box, p);
                shape.Draw();
                _drawnShapes.Add(shape);

                _dPoint.Y += _spaceBetweenGates;
            }

            //now draw output gate!
            var outGate = _circuit.GetOutputGate();                                    
            //but we need to check how many middle gates do we have to make enoughspace
            var mgc = _circuit.GetMiddleGates().Count;
            if (mgc == 0) //if there are no middle gates!
                mgc = 1; //just not to screw up calculation
            //worst case scenario is when all middle gates go in one row! so we take that for now :P
            _dPoint.X += (_spaceBetweenGates * (mgc * 2)) + (_drawnShapes.FirstOrDefault().GetDrawingRectangle().Width * mgc);
            _dPoint.Y = MinDistanceFromTop;
            shape = new GateShape(outGate, _dPoint, _zoomFactor, box, p);
            shape.Draw();
            _drawnShapes.Add(shape);                                     


            //move draw point to next column
            _dPoint.X = MinDistanceFromLeft + _spaceBetweenGates + _drawnShapes.FirstOrDefault().GetDrawingRectangle().Width;
            _dPoint.Y = MinDistanceFromTop;

            //now we need to know how manny middle gate we have
            //we need to copy by value this time!
            var midGates = new List<Gate>(_circuit.GetMiddleGates());
            //if we have any
            //draw them! 
            //TODO draw based on net connection with input gates and respective other middle gates!
            //start by the gates that are connected to the input gates
            while (midGates.Count != 0)
            {
                for (int i = 0; i < midGates.Count; i++)
                {
                    for (int j = 0; j < _drawnShapes.Count; j++)
                    {
                        int id = _drawnShapes[j].GetAssignedGate().GetOutputNet().Id;
                        if (midGates[i].GetInputNets().Any(net => net.Id == id))
                        {
                            shape = new GateShape(midGates[i], _dPoint, _zoomFactor, box, p);
                            shape.Draw();
                            _drawnShapes.Add(shape);
                            _dPoint.Y += _spaceBetweenGates;
                            midGates.RemoveAt(i);
                        }

                        //The last remaining middle shape should be connected to the output gate's inputs!!!
                        if (midGates.Count == 1)
                        {
                            shape = new GateShape(midGates[0], _dPoint, _zoomFactor, box, p);
                            shape.Draw();
                            _drawnShapes.Add(shape); 
                            midGates.Clear();
                            break;
                        }
                    }
                    
                }
            }





            //Gate[] gates = _circuit.GetGates().ToArray();
            //for (int i = 0; i < gates.Length; i++)
            //{
            //    var gate = gates[i];
            //    //construct shape object for each gate
            //    GateShape shape = new GateShape(gate, _dPoint.X, newHeight, _zoomFactor, box, p);
            //    shape.Draw();

            //    //calculate positon of next gate

            //    spaceTaken += (shape.GetDrawingRectangle().Width + _spaceBetweenGates);


            //    if (spaceTaken >
            //        box.Width - (_spaceBetweenGates - shape.GetDrawingRectangle().Width)*p.Graphics.PageScale)
            //    {
            //        //move to new row
            //        rowsTaken++;
            //        newHeight = (rowsTaken*MinDistanceFromTop) + ((rowsTaken - 1)*shape.GetDrawingRectangle().Height);

            //        _dPoint = new Point(MinDistanceFromLeft, newHeight);
            //        spaceTaken = MinDistanceFromLeft;
            //    }
            //    else
            //        _dPoint =
            //            new Point((_dPoint.X += _spaceBetweenGates) + shape.GetDrawingRectangle().Width,
            //                newHeight);

            //     _drawnShapes.Add(shape);
            //}
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
            _drawnShapes = new List<GateShape>();

            box.Invalidate();
        }

        private void box_Click(object sender, EventArgs e)
        {
            box.Focus();
        }

    }
}
