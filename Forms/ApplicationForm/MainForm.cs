using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using MetroFramework.Forms;
using SatSolver.Objects.Gates;
using SatSolver.UserInterface.CustomControls;

namespace SatSolver.UserInterface.ApplicationForm
{
    /// <summary>
    /// 
    /// </summary>
    public partial class MainForm : MetroForm
    {          
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
               schematicControl.SetCircuit(args.Circuit, args.TreeId);
            };

            #if DEBUG
            //netControl1.LoadNetListFromFile(@"D:\WORK\SatSolver\Forms\SampleNetlists\xor2_nand.net");
            netControl1.LoadNetListFromFile(@"D:\WORK\SatSolver\Forms\SampleNetlists\xor2_nand.net");
            netControl2.LoadNetListFromFile(@"D:\WORK\SatSolver\Forms\SampleNetlists\xor2_nand_wrong.net");
#endif
        }

        private void solveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (netControl1.Circuit == null || netControl2.Circuit == null)
            {
                MessageBox.Show("Please load both net lists!");
                return;
            }

            var c1 = netControl1.Circuit;
            var c2 = netControl1.Circuit;

            AddLine("Solving...", 0);
            AddLine("Net 1 Count: " + netControl1.Circuit.GetGatesCount(), 1);
            AddLine("Net 2 Count: " + netControl2.Circuit.GetGatesCount(), 1);

            List<CNF> cnf = new List<CNF>();

            foreach (var gate in c1.GetGates())
            {
                cnf.Add(new CNF(gate.GetCnf()));
            }

            foreach (var cnf1 in cnf)
            {
                AddLine(cnf1.ToString(), 2);
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

        ///// <summary>
        /////  http://stackoverflow.com/questions/3718380/winforms-double-buffering
        ///// </summary>
        //protected override CreateParams CreateParams
        //{
        //    get
        //    {
        //        CreateParams cp = base.CreateParams;
        //        cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
        //        return cp;
        //    }
        //}
    }

    //TODO move this to a file later
    public class CNF
    {
        public List<List<int>> HAHA;

        public CNF(List<List<int>> haha)
        {
            HAHA = haha;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append("{");
            for (int i = 0; i < HAHA.Count; i++)
            {
                sb.Append("{");

                for (int j = 0; j < HAHA[i].Count; j++)
                {
                    sb.Append(HAHA[i][j]);
                    if (j != HAHA[i].Count - 1)
                    {
                        sb.Append(",");
                    }

                }


                if (i == HAHA.Count - 1)
                {
                    sb.Append("}");
                }
                else
                {
                    sb.Append("},");
                }
                
            }

            sb.Append("}");

            return sb.ToString();
        }
    }
}
