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


        /// <summary>
        /// The <see cref="Circuit"/> that will be used as data in this node
        /// </summary>
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

        /// <summary>
        /// Constructor for Circuit node
        /// </summary>
        /// <param name="textToShow"></param>
        /// <param name="treeViewContainer"></param>
        public CircuitTreeNode(string textToShow, NetListTreeView treeViewContainer)
            : base(NodeType.Default, treeViewContainer)
        {
            Text = textToShow;
            _hasData = false;
        }

        public GateTreeNode[] GetGateNodes()
        {
            IList<GateTreeNode> gateNodes =
                (from Gate gate in Circuit.GetGates() select new GateTreeNode(gate, ContainerTreeView)).ToList();

            return gateNodes.ToArray();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("File:" + Environment.NewLine + Circuit.GetFilePath());
            sb.AppendLine("Gates:\t" + Circuit.GetGates().Count);
            sb.AppendLine("Nets:\t" + Circuit.GetNetsCount());

            return sb.ToString();
        }
    }
}