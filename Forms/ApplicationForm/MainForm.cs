using System.Drawing;
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

        public ImageList TreeImageList;
            
        /// <summary>
        /// Default Constructor for the MainForm
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {
            netControl1.setId(1);
            netControl1.SetParent(this);
            netControl2.setId(2);
            netControl2.SetParent(this);
        }




    }
}
