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

        private ImageList _nodesImageList;
            
        /// <summary>
        /// Default Constructor for the MainForm
        /// </summary>
        public MainForm()
        {
            InitializeComponent();

            _nodesImageList = new ImageList();
            _nodesImageList.TransparentColor = Color.White;
            _nodesImageList.Images.Add(new Bitmap(Properties.Resources.circuit)); //0
            _nodesImageList.Images.Add(new Bitmap(Properties.Resources.count)); //1
            _nodesImageList.Images.Add(new Bitmap(Properties.Resources.wiring)); //2
            _nodesImageList.Images.Add(new Bitmap(Properties.Resources.gate_and)); //3
            _nodesImageList.Images.Add(new Bitmap(Properties.Resources.gate_or)); //4
            _nodesImageList.Images.Add(new Bitmap(Properties.Resources.gate_xor)); //5
            _nodesImageList.Images.Add(new Bitmap(Properties.Resources.gate_inv)); //6
            _nodesImageList.Images.Add(new Bitmap(Properties.Resources.gate_one)); //7


            netTree1.ImageList = netTree2.ImageList = _nodesImageList;
        }




    }
}
