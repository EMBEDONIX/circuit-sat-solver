using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using MetroFramework.Forms;
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
            netControl1.LoadNetListFromFile(@"D:\WORK\SatSolver\Forms\SampleNetlists\xor2_nand.net");
            #endif
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
}
