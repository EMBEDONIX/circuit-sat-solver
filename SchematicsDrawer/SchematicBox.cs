using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using SatSolver.Objects;

namespace SatSolver.SchematicsDrawer
{
    public class SchematicBox : PictureBox
    {
        //rows and columns where drawing happens
        private int _rows; //rows
        private int _cols; //columns
        

        //Circuits A and B
        private Circuit _circuitA;
        private Circuit _circuitB;

        /// <summary>
        /// Default constructor needed for VS designer
        /// </summary>
        public SchematicBox()
        {
            
        }

        /// <summary>
        /// Add Circuit 'A' for drawing
        /// </summary>
        /// <param name="circuit">The <see cref="Circuit"/> to be drawn</param>
        public void AddCircuitA(Circuit circuit)
        {
            _circuitA = circuit;
            Invalidate(); //invalidate to force redraw
        }

        /// <summary>
        /// Add Circuit 'B' for drawing
        /// </summary>
        /// <param name="circuit">The <see cref="Circuit"/> to be drawn</param>
        public void AddCircuitB(Circuit circuit)
        {
            _circuitB = circuit;
            Invalidate(); //invalidate to force redraw
        }

        /// <summary>
        /// Add both Circuits for drawing
        /// </summary>
        /// <param name="circuit">The <see cref="Circuit"/> objects to be drawn</param>
        public void AddCircuits(Circuit circuit)
        {
            _circuitA = circuit;
            Invalidate(); //invalidate to force redraw
        }


#region MARSHALS
        /*
         * I put this hack to prevent drawing when the container of this control is sending draw message
         * to it. In this case the panel which contains this, will not request paint if it is being scrolled
         * to this picturebox!
         * 
         */
        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, Int32 wMsg, bool wParam, Int32 lParam);
        private const int WM_SETREDRAW = 11;                                                      

        public void SuspendDrawing(Control parent)
        {
            SendMessage(parent.Handle, WM_SETREDRAW, false, 0);
        }

        public void ResumeDrawing(Control parent)
        {
            SendMessage(parent.Handle, WM_SETREDRAW, true, 0);
            parent.Refresh();
        }
#endregion

    }
}
