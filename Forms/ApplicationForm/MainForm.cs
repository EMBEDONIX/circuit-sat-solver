using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MetroFramework.Forms;
using SatSolver.Objects;
using SatSolver.Objects.Gates;
using SatSolver.UserInterface.CustomControls;

namespace SatSolver.UserInterface.ApplicationForm
{
    /// <summary>
    /// 
    /// </summary>
    public partial class MainForm : MetroForm
    {
        private Circuit _circuitA, _circuitB, _circuitM;
        

        /// <summary>
        /// Default Constructor for the MainForm
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            Focus();
            tabPages.Focus();
            tabPages.Controls[0].Focus();
            netControl1.Focus();
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {
            netControl1.setId(1);
            netControl1.SetParent(this);
            netControl2.setId(2);
            netControl2.SetParent(this);

            netControl1.CircuitLoaded += (o, args) =>
            {
                _circuitA = args.Circuit;
               netControl2.SetNetIdOffset(netControl1.Circuit.GetHighestNetId());
            };

            netControl2.CircuitLoaded += (o, args) =>
            {
                _circuitB = args.Circuit;

                try
                {
                    _circuitM = new MiterCircuit(_circuitA, _circuitB);
                }
                catch (Exception exc)
                {
                    AddLine(exc.Message, 0);
                }
                miterControl.AddCircuit(_circuitM);
                schematicControl.SetCircuit(_circuitM, args.TreeId);

            };

            miterControl.CircuitLoaded += (o, args) =>
            {
                solveToolStripMenuItem.PerformClick();
                netControl1.ExpandAll();
                netControl2.ExpandAll();
                miterControl.ExpandAll();

            };




#if DEBUG
            netControl1.LoadNetListFromFile(@"D:\WORK\SatSolver\Forms\SampleNetlists\test1.net");
            netControl2.LoadNetListFromFile(@"D:\WORK\SatSolver\Forms\SampleNetlists\test2.net");
#endif
        }



        private void solveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (netControl1.Circuit == null || netControl2.Circuit == null)
            {
                MessageBox.Show("Please load both net lists!");
                return;
            }

            AddLine("Solving...", 0);
            AddLine("Net 1 Count: " + netControl1.Circuit.GetGatesCount(), 1);
            AddLine("Net 2 Count: " + netControl2.Circuit.GetGatesCount(), 1);
            
            List<CNF> cnf1 = _circuitA.GetGates().Select(gate => gate.GetCnf()).ToList();
            List<CNF> cnf2 = _circuitB.GetGates().Select(gate => gate.GetCnf()).ToList();
            List<CNF> cnft = _circuitM.GetGates().Select(gate => gate.GetCnf()).ToList();
            //TODO this is a hack...and shity OO programing...I should take care of this asap!!!
            cnft.Add(new CNF(new List<List<int>> {new List<int>() {_circuitM.GetFinalOrGateId().GetOutputNet().Id}}));
            
                
            AddLine("CNF 1", 2);
            foreach (var cnf in cnf1)
            {
                AddLine(cnf.ToString(), 3);
            }
            
            AddLine("", 0);

            AddLine("CNF 2", 2);
            foreach (var cnf in cnf2)
            {
                AddLine(cnf.ToString(), 3);
            }

            AddLine("", 0);

            AddLine("MITER CNF", 2);
            foreach (var cnf in cnft)
            {
                AddLine(cnf.ToString(), 3);
            }
        }

        private void AddLine(string line, int level)
        {
            var indent = "";
            for (int i = 0; i < level; i++)
            {
                indent += "  ";
            }
            tbDebug.AppendText(indent + line + Environment.NewLine);
        }
    }
}
