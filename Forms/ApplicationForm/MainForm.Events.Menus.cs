using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SatSolver.Objects;
using SatSolver.UserInterface.CustomControls;
using SatSolver.Utilities;

namespace SatSolver.UserInterface.ApplicationForm
{
    partial class MainForm
    {
        

        private void openNetList1ToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            netControl1.ShowBrowseNetListFileDialog();
        }

        private void openNetList2ToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            netControl1.ShowBrowseNetListFileDialog();
        }

        private void exitToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            //TODO if in calculation mode, make sure to exit safely!
            Application.Exit();
        }

        private void toggleTreesVisibilityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            splitContainer.Panel1Collapsed = !splitContainer.Panel1Collapsed;
        }


    }
}
