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
    /// Node that deals with gates
    /// </summary>
    [DefaultProperty("Text")]
    public class GateTreeNode : BaseTreeNode
    {
        private Gate _gate;

        public GateTreeNode(Gate gate, TreeView containerTreeView) : base(NodeType.Gate, containerTreeView)
        {
            _gate = gate;
            Text = gate.GetGateTypeAsString();

            switch (gate.GetGateType())
            {
                case GateType.Inv:
                    ImageIndex = 6;
                    SelectedImageIndex = 6;
                    break;
                case GateType.Or:
                    ImageIndex = 4;
                    SelectedImageIndex = 4;
                    break;
                case GateType.And:
                    ImageIndex = 3;
                    SelectedImageIndex = 3;
                    break;
                case GateType.Xor:
                    ImageIndex = 5;
                    SelectedImageIndex = 5;
                    break;
                case GateType.One:
                    ImageIndex = 7;
                    SelectedImageIndex = 7;
                    break;
                case GateType.Zero: //TODO find a good image for zero!
                    ImageIndex = 7;
                    SelectedImageIndex = 7;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            List<SignalTreeNode> signalNodes = new List<SignalTreeNode>();
            
            foreach (Signal signal in _gate.GetAllSignals())
            {
                signalNodes.Add(new SignalTreeNode(signal, ContainerTreeView));
            }

            Nodes.AddRange(signalNodes.ToArray());
        }
    }
}
