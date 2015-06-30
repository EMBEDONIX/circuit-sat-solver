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
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_dp != null && _dp.IsSolving())
            {
                _dp.Stop();
                _dp.Report -= null;
            }
        }

        private void startSolvingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tbDebug.Clear();
            _lineCounter = 0;
            tabPages.SelectedTab = tabSolution;

            if (netControl1.Circuit == null || netControl2.Circuit == null)
            {
                MessageBox.Show("Please load both net lists!");
                return;
            }

            List<CNF> cnft = _circuitM.GetGates().Select(gate => gate.GetCnf()).ToList();
            //TODO this is a hack...and shity OO programing...I should take care of this asap!!!
            //cnft.Add(new CNF(new List<List<int>> { new List<int>() { _circuitM.GetFinalOrGateId().GetOutputNet().Id } }));
            cnft.AddRange(_circuitM.GetMitterForInputs());

            //Must flatten the cnf list to a single cnf!
            var flatCnf = new CNF();
            foreach (var cnf in cnft)
            {
                flatCnf.AddCnf(cnf);
            }

            _dp = new DavisPutnam(flatCnf);
            _dp.Report += DavisPutnamReport;
            _dp.Start();
        }

        private void stopSolvingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_dp != null)
            {
                _dp.Stop();
                _dp.Report -= DavisPutnamReport;
                _dp = null;
            }
        }


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

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox ab = new AboutBox();
            ab.ShowDialog();
        }
    }
}