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
    class SignalTreeNode : BaseTreeNode
    {
        private Signal _signal;

        public SignalTreeNode(Signal signal, TreeView containerTreeView) : base(NodeType.Signal, containerTreeView)
        {
            _signal = signal;
            
            if (!string.IsNullOrWhiteSpace(signal.Name))
                Text = signal.Name + "/" + signal.Id;
            else
                Text = signal.Id.ToString();

            ImageIndex = 2;
            SelectedImageIndex = 2;
        }
    }
}
