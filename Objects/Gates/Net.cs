using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace SatSolver.Objects.Gates
{
    public class Net
    {
        public string Name { get; private set; }
        public int Id { get; private set; }

        private Gate _inGate;
        private Gate _outGate;
        private NetValue _value = NetValue.NotSet;
        private bool _isMiddleNet;        

        /// <summary>
        /// Constructs a net object with a given name and id
        /// </summary>
        /// <param name="name">Name of the net, e.g. "a" or "a_0"</param>
        /// <param name="id">Id of the net in the netlist</param>
        public Net(string name, int id)
        {
            Name = name;
            Id = id;
            _isMiddleNet = false;
        }

        /// <summary>
        /// Construct a Net which is a middle net (not a top input/final output)
        /// </summary>
        /// <param name="id">Id of the net in the netlist</param>
        public Net(int id)
        {
            Id = id;
            _isMiddleNet = true;
        }

        /// <summary>
        /// Construct a middle net with input and output gates
        /// </summary>
        /// <param name="id">Id of the net in the netlist</param>
        /// <param name="inGate">The gate that this net is used as an input to it</param>
        /// <param name="outGate">the gate that this net is comming out from it</param>
        public Net(int id, Gate inGate, Gate outGate)
        {
            Id = id;
            _inGate = inGate;
            _outGate = outGate;
            _isMiddleNet = true;
        }

        /// <summary>
        /// Constructs a net object with a given name and id, and specify the gates
        /// which this net is input of and output to.
        /// </summary>
        /// <param name="name">Name of the net, e.g. "a" or "a_0"</param>
        /// <param name="id">Id of the net in the netlist</param>
        /// <param name="inGate">The gate that this net is used as an input to it</param>
        /// <param name="outGate">the gate that this net is comming out from it</param>
        public Net(string name, int id, Gate inGate, Gate outGate)
        {
            Name = name;
            Id = id;
            _inGate = inGate;
            _outGate = outGate;
        }

        /// <summary>
        /// Set the value of the net
        /// </summary>
        /// <param name="value"><see cref="NetValue"/></param>
        public void SetValue(NetValue value)
        {
            _value = value;
        }

        /// <summary>
        /// Set the gate that this net is comming out from it
        /// </summary>
        /// <param name="gate"></param>
        public void SetOutputGate(Gate gate)
        {
            _outGate = gate;
        }

        /// <summary>
        /// Set the gate that this net is used as an input to it
        /// </summary>
        /// <param name="gate"></param>
        public void SetInputGate(Gate gate)
        {
            _inGate = gate;
        }
    }
}
