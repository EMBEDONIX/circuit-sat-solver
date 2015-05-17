using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SatSolver.UserInterface.CustomControls
{
    /// <summary>
    /// An abstract implementation of TreeNode which is used in this application
    /// </summary>
    public abstract class BaseTreeNode : TreeNode
    {
        public NodeType NodeType { get; private set; }
        public TreeView ContainerTreeView { get; private set; } 

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nodeType"></param>
        /// <param name="containerTreeView"></param>
        protected BaseTreeNode(NodeType nodeType, TreeView containerTreeView)
        {
            NodeType = nodeType;
            ContainerTreeView = containerTreeView;
        }
    }
}
