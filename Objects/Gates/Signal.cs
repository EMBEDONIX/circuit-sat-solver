using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace SatSolver.Objects.Gates
{
    public class Signal
    {
        public string Name { get; private set; }
        public int Id { get; private set; }

        private Gate _inGate;
        private Gate _outGate;
        private SignalValue _value = SignalValue.NotSet;
        private bool _isMiddleSignal;        

        /// <summary>
        /// Constructs a signal object with a given name and id
        /// </summary>
        /// <param name="name">Name of the signal, e.g. "a" or "a_0"</param>
        /// <param name="id">Id of the signal in the netlist</param>
        public Signal(string name, int id)
        {
            Name = name;
            Id = id;
            _isMiddleSignal = false;
        }

        /// <summary>
        /// Construct a Signal which is a middle signal (not a top input/final output)
        /// </summary>
        /// <param name="id">Id of the signal in the netlist</param>
        public Signal(int id)
        {
            Id = id;
            _isMiddleSignal = true;
        }

        /// <summary>
        /// Construct a middle signal with input and output gates
        /// </summary>
        /// <param name="id">Id of the signal in the netlist</param>
        /// <param name="inGate">The gate that this signal is used as an input to it</param>
        /// <param name="outGate">the gate that this signal is comming out from it</param>
        public Signal(int id, Gate inGate, Gate outGate)
        {
            Id = id;
            _inGate = inGate;
            _outGate = outGate;
            _isMiddleSignal = true;
        }

        /// <summary>
        /// Constructs a signal object with a given name and id, and specify the gates
        /// which this signal is input of and output to.
        /// </summary>
        /// <param name="name">Name of the signal, e.g. "a" or "a_0"</param>
        /// <param name="id">Id of the signal in the netlist</param>
        /// <param name="inGate">The gate that this signal is used as an input to it</param>
        /// <param name="outGate">the gate that this signal is comming out from it</param>
        public Signal(string name, int id, Gate inGate, Gate outGate)
        {
            Name = name;
            Id = id;
            _inGate = inGate;
            _outGate = outGate;
        }

        /// <summary>
        /// Set the value of the signal
        /// </summary>
        /// <param name="value"><see cref="SignalValue"/></param>
        public void SetValue(SignalValue value)
        {
            _value = value;
        }

        /// <summary>
        /// Set the gate that this signal is comming out from it
        /// </summary>
        /// <param name="gate"></param>
        public void SetOutputGate(Gate gate)
        {
            _outGate = gate;
        }

        /// <summary>
        /// Set the gate that this signal is used as an input to it
        /// </summary>
        /// <param name="gate"></param>
        public void SetInputGate(Gate gate)
        {
            _inGate = gate;
        }
    }
}
