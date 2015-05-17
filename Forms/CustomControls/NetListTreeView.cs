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

        private Circuit _circuit;
        private CircuitTreeNode _circuitNode;

        /// <summary>
        /// Default constructor necessary for design time
        /// </summary>
        public NetListTreeView()
        {

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
                { ImageIndex = 1, SelectedImageIndex = 1};
            TreeNode countSignalsNode = new TreeNode("Signals = " + circuit.GetSignalsCount())
                { ImageIndex = 1, SelectedImageIndex = 1 };
            root.Nodes.AddRange(new TreeNode[]{countGatesNode, countSignalsNode});

            GateTreeNode[] gateNodes = root.GetGateNodes();

            root.Nodes.AddRange(gateNodes);

            Nodes.Add(root);

            EndUpdate();
        } 
    }
}
