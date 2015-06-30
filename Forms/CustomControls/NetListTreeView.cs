using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SatSolver.Objects;
using SatSolver.Objects.Gates;

namespace SatSolver.UserInterface.CustomControls
{
    public class NetListTreeView : TreeView
    {
        /// <summary>
        /// The last TreeNode which was selected by user
        /// </summary>
        public TreeNode LastSelectedNode { get; private set; }

        private Circuit _circuit;
        private CircuitTreeNode _circuitNode;

        /// <summary>
        /// Default constructor necessary for design time
        /// </summary>
        public NetListTreeView()
        {
            InitializeEvents();
        }

        private void InitializeEvents()
        {
            //after each nodes becomes selected, it will be the LastSelectedNode
            AfterSelect += (sender, args) =>
            {
                if (LastSelectedNode != null)
                    LastSelectedNode.BackColor = Color.White;

                LastSelectedNode = args.Node;
            };

            //Change the background of LastSelected Node, to help user to remember which node was 
            //selected
            LostFocus += (sender, args) =>
            {
                if (LastSelectedNode != null)
                    LastSelectedNode.BackColor = Color.LightPink;
            };
        }

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="circuit"></param>
        public void AddCircuit(Circuit circuit)
        {
            _circuit = circuit;

            BeginUpdate();

            Nodes.Clear();
            CircuitTreeNode root = new CircuitTreeNode(circuit, this) {ImageIndex = 0};
            TreeNode countGatesNode = new TreeNode("Gate = " + circuit.GetGatesCount())
            {ImageIndex = 1, SelectedImageIndex = 1};
            TreeNode countNetsNode = new TreeNode("Nets = " + circuit.GetNetsCount())
            {ImageIndex = 1, SelectedImageIndex = 1};
            root.Nodes.AddRange(new TreeNode[] {countGatesNode, countNetsNode});

            GateTreeNode[] gateNodes = root.GetGateNodes();

            root.Nodes.AddRange(gateNodes);

            Nodes.Add(root);

            EndUpdate();
        }
    }
}