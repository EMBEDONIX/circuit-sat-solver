using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SatSolver.Objects.Gates;

namespace SatSolver.UserInterface.CustomControls
{
    [DefaultProperty("Text")]
    class NetTreeNode : BaseTreeNode
    {
        private Net _net;

        public NetTreeNode(Net net, TreeView containerTreeView) : base(NodeType.Net, containerTreeView)
        {
            _net = net;
            
            if (!string.IsNullOrWhiteSpace(net.Name))
                Text = net.Name + "/" + net.Id;
            else
                Text = net.Id.ToString();

            ImageIndex = 2;
            SelectedImageIndex = 2;
        }
    }
}
