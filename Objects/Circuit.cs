using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SatSolver.Objects.Gates;

namespace SatSolver.Objects
{
    public class Circuit
    {
        private IList<Gate> _gates;
        private string _file;

        public Circuit(string file)
        {
            _gates = new List<Gate>();
            _file = file;
        }

        public void AddGate(Gate gate)
        {
            _gates.Add(gate);
        }


        /// <summary>
        /// Get the file name which this circuit is generated from
        /// </summary>
        /// <returns>Name of the file</returns>
        public string GetName()
        {
            return Path.GetFileNameWithoutExtension(_file);
        }

        /// <summary>
        /// Get number of gates in this circuit
        /// </summary>
        /// <returns>Number of gates in this circuit</returns>
        public int GetGatesCount()
        {
            return _gates.Count;
        }

        public Gate GetGateAt(int i)
        {
            return _gates[i];
        }

        /// <summary>
        /// Get total number of signals in all of the gates in this circuit
        /// </summary>
        /// <returns>number of total signals in every gate in this circuit</returns>
        public int GetSignalsCount()
        {
            return _gates.Sum(gate => gate.GetSignalsCount());
        }

        public IEnumerable GetGates()
        {
            return _gates;
        }
    }
}
