using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SatSolver.Objects;
using SatSolver.Objects.Gates;

namespace SatSolver.UserInterface.CustomControls
{
    /// <summary>
    /// Drived from <see cref="TreeNode"/> to help maintaining <see cref="NetListTreeView"/>
    /// </summary>
    [DefaultProperty("Text")]
    public class CircuitTreeNode : BaseTreeNode
    {
        private bool _hasData = false;
        

        public Circuit Circuit { get; private set; }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="circuit">The circuit object to build this node for</param>
        /// <param name="netListTreeView"></param>
        /// <param name="treeViewContainer">The TreeView that this node is going to be inside it</param>
        public CircuitTreeNode(Circuit circuit, TreeView treeViewContainer)
        : base(NodeType.Circuit, treeViewContainer)
        {
            Circuit = circuit;
            Text = circuit.GetName();
            _hasData = true;
        }

        public CircuitTreeNode(string textToShow, NetListTreeView treeViewContainer) 
            : base(NodeType.Default, treeViewContainer)
        {
            Text = textToShow;
            _hasData = false;
        }

        public GateTreeNode[] GetGateNodes()
        {
            IList<GateTreeNode> gateNodes = (from Gate gate in Circuit.GetGates() select new GateTreeNode(gate, ContainerTreeView)).ToList();

            return gateNodes.ToArray();
        }
    }
}
