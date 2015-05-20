using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace SatSolver.SchematicsDrawer
{
    public class SchematicBox : PictureBox
    {
        /*
         * I put this hack to prevent drawing when the container of this control is sending draw message
         * to it. In this case the panel which contains this, will not request paint if it is being scrolled
         * to this picturebox!
         * 
         */
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, Int32 wMsg, bool wParam, Int32 lParam);

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

    }
}
