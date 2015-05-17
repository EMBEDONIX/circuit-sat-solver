using System.Windows.Forms;
using SatSolver.UserInterface.CustomControls;

namespace SatSolver.UserInterface.ApplicationForm
{
    public partial class MainForm
    {
        private void netTree1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node != netTree1.Nodes[0])
                return;

            if (((BaseTreeNode) (e.Node)).NodeType == NodeType.Default ||
                ((BaseTreeNode) (e.Node)).NodeType == NodeType.Circuit)
            {
                ShowBrowseNetListFileDialog(1);
            }

        }

        private void netTree2_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //Check if node type is derived from BaseTreeNode
            if (e.Node != netTree2.Nodes[0])
                return;

            if (((BaseTreeNode)(e.Node)).NodeType == NodeType.Default ||
                ((BaseTreeNode)(e.Node)).NodeType == NodeType.Circuit)
            {
                ShowBrowseNetListFileDialog(2);
            }
        }
    }
}