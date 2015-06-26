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
        //keyboard!
        private bool _keyControllPressed;


        //minimum space from left side of box
        private const int MinDistanceFromLeft = 50;
        //minimum space from top side of box
        private const int MinDistanceFromTop = 50;
        private float _zoomFactor = 1;
        private int _spaceBetweenGates = 20;

        private Circuit _circuit;
        private List<GateShape> _drawnShapes;
        private PointF _dPoint = new Point(MinDistanceFromLeft, MinDistanceFromTop);

        private const int _tooltipDuration = 2000;
        private bool _tooltipShown;
        private Timer _tooltipTimer;

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
                if (_keyControllPressed)
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
                }
            };

            box.MouseMove += (sender, args) =>
            {
                if (_drawnShapes != null && _drawnShapes.Count > 0)
                {
                    var p = (PointF)args.Location;
#if DEBUG
                    Debug.WriteLine($"Mouse move on {args.X},{args.Y}");
#endif
                    foreach (var gate in _drawnShapes)
                    {
                        if (gate.GetDrawingRectangle().Contains(p))
                        {
                            //tooltip.RemoveAll();
                            //this.Capture = true;
                            if (!_tooltipShown)
                            {
                                if (_tooltipTimer != null)
                                {
                                    _tooltipTimer.Stop();
                                }
                                tooltip.RemoveAll();
                                tooltip.Show(gate.GetAssignedGate().GetCnf(0).ToString(), this, (int) p.X + 5, (int) p.Y,
                                    _tooltipDuration);
                                _tooltipShown = true;

                                _tooltipTimer = new Timer();
                                _tooltipTimer.Interval = _tooltipDuration;
                                _tooltipTimer.Tick += (o, eventArgs) =>
                                {
                                    _tooltipShown = false;
                                };
                                _tooltipTimer.Start();
                            }
                            return;
                        }
                    }

                }
            };


            box.KeyDown += ShematicControl_KeyDown;
            box.KeyUp += ShematicControl_KeyUp;

            //To prevent box from repainting itself while panel 
            //is being scrolled by mousewheel
            panel.MouseWheel += (sender, e) =>
            {

                box.SuspendDrawing(panel);
                if (_keyControllPressed)
                {
                    ((HandledMouseEventArgs) e).Handled = true;
                }

                box.ResumeDrawing(panel);
                
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

                Pen pen = new Pen(Color.Red, 0.8f);
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
            _spaceBetweenGates = 80;
            //after each gate is drawn its shape should go in this list for further use!
            _drawnShapes = new List<GateShape>();
            //keep trak of how many gates are drawn in x axis, if near left side of
            //the control, move to next line!
            float spaceTaken = MinDistanceFromLeft;
            //keep track of how many rows are drawn so far
            int rowsTaken = 1;

            //First of all, the input gates must be drawn in a column
            var inGates = _circuit.GetInputGates();

            //special case if the circuit only has 1 gate!
            if (_circuit.GetGatesCount() == 1)
            {
                //GateShape shape = new GateShape(inGates[i], _dPoint.X, _dPoint.Y, _zoomFactor, box, p);
                shape = new GateShape(_circuit.GetGates().FirstOrDefault(), _dPoint, _zoomFactor, box, p);
                shape.Draw();
                _drawnShapes.Add(shape);

                _dPoint.Y += _spaceBetweenGates;

                return;
            }



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
            var midGates =_circuit.GetMiddleGates();
            var mgCount = midGates.Count;
            //Now find the gates that are connected to the input gates
            //and repeat this for the gates in next column

            //do this untill count of middle gates is 0 
            //in each itteration one gate willbe drawnand this cound will decrease
            while (mgCount > 0)
            {
                //see
                var drawnGates = _drawnShapes.Select(t => t.GetAssignedGate()).ToArray();
                
                List<Gate> matchGates = new List<Gate>();
                for (int i = 0; i < drawnGates.Length; i++)
                {
                    Gate drawnGate = drawnGates[i];
                    
                    //TODO using midGates here is a performance hit....fix this ASAP because it itterase over all
                    foreach (var gate in midGates)
                    {
                        //if already drawn, skip!
                        //if (_drawnShapes.Any(x => x.GetAssignedGate().Equals(gate)))
                        //    continue;

                        if (gate.GetInputNets().Any(x => x.Id == drawnGate.GetOutputNet().Id))
                        {
                            matchGates.Add(gate);
                        }
                    }

                    //if found any gates
                    if (matchGates.Count > 0)
                    {

                        MoveDrawerPointToNextColumnOfAShape(_drawnShapes[i]);

                        //draw gate(s)
                        foreach (var gate in matchGates)
                        {                                                  
                                shape = new GateShape(gate, _dPoint, _zoomFactor, box, p);
                                shape.Draw();           
                                _drawnShapes.Add(shape);
                                MoveDrawerPointToNextColumnOfAShape(shape);
                        }

                        //decrement while condition
                        mgCount -= matchGates.Count;
                    }

                    //clear the list of matched gates for next itteration!
                    matchGates.Clear();
                }
            }

            

            

            ////if we have any
            ////draw them! 
            ////TODO draw based on net connection with input gates and respective other middle gates!
            ////start by the gates that are connected to the input gates
            //while (midGates.Count != 0)
            //{
            //    for (int i = 0; i < midGates.Count; i++)
            //    {
            //        for (int j = 0; j < _drawnShapes.Count; j++)
            //        {
            //            int id = _drawnShapes[j].GetAssignedGate().GetOutputNet().Id;
            //            if (midGates[i].GetInputNets().Any(net => net.Id == id))
            //            {
            //                shape = new GateShape(midGates[i], _dPoint, _zoomFactor, box, p);
            //                shape.Draw();
            //                _drawnShapes.Add(shape);
            //                _dPoint.Y += _spaceBetweenGates;
            //                midGates.RemoveAt(i);
            //            }

            //            //The last remaining middle shape should be connected to the output gate's inputs!!!
            //            if (midGates.Count == 1)
            //            {
            //                shape = new GateShape(midGates[0], _dPoint, _zoomFactor, box, p);
            //                shape.Draw();
            //                _drawnShapes.Add(shape); 
            //                midGates.Clear();
            //                break;
            //            }
            //        }
                    
            //    }
            //}   
        }

        private void MoveDrawerPointToNextColumnOfAShape(GateShape shape)
        {
            _dPoint.X = shape.GetDrawingRectangle().X 
            + (shape.GetDrawingRectangle().Width * 2) + _spaceBetweenGates;
            _dPoint.Y = shape.GetDrawingRectangle().Y  +  shape.GetDrawingRectangle().Height;
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

        private void panel_Paint(object sender, PaintEventArgs e)
        {
            //box.Invalidate(); //redraw picturebox
        }

        private void ShematicControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (sender != null)
            {
                if (sender == box)
                {
                    _keyControllPressed = true;
#if DEBUG
                    Debug.WriteLine("KeyDown '" + e.KeyCode + "' in PictureBox");
#endif
                }
            }
        }

        private void ShematicControl_KeyUp(object sender, KeyEventArgs e)
        {
            if (sender != null)
            {
                if (sender == box)
                {
                    _keyControllPressed = false;
#if DEBUG
                    Debug.WriteLine("KeyUp '" + e.KeyCode + "' in PictureBox");
#endif
                }
            }
        }
    }
}
