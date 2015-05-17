using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SatSolver.Objects.Gates;

namespace SatSolver.SchematicsDrawer.Shapes
{
    public abstract class GateShapeBase
    {
        
        /// <summary>
        /// Ratio of painted objects compared to the container size
        /// </summary>
        protected float Ratio = 1;

        /// <summary>
        /// Color of the shape
        /// </summary>
        protected Color ShapeColor;

        /// <summary>
        /// Type of the gate to draw
        /// </summary>
        protected GateType GateType;

        protected PointF PortInA;
        protected PointF PortInB;
        protected PointF PortOut;

        protected GateShapeBase(Panel panel)
        {
            
        }

        /// <summary>
        /// Draw the intended shape of gate on the given surface
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public abstract Bitmap Draw(PaintEventArgs p);
    }
}
