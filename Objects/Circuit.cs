using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SatSolver.Objects.Gates;

namespace SatSolver.Objects
{
    public class Circuit
    {
        private bool _gatesAssignedToNets;
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
        /// Assigns gates to nets (input and output nets)
        /// </summary>
        public void AssignGatesToNets()
        {
            //itterate over all gates
            for (int i = 0; i < _gates.Count; i++)
            {
                Gate gate = _gates[i]; //current gate in the i loop
                Gate nextGate;

                //try to get next Gate
                try
                {
                    nextGate = _gates[i + 1]; //next gate in the i loop 
                }
                catch (Exception)
                {
                    nextGate = null;
                }

                List<Net> inputs = gate.GetInputNets(); //input nets of current gate
                Net output = gate.GetOutputNet(); //output net of current gate

                //Assign inputs of gates
                for (int j = 0; j < inputs.Count - 1; j++)
                {
                    Net net = inputs[j];

                    //First gate should have input nets with names e.g. 'a' or 'a_0'
                    if (i == 0)
                    {
                        if (net.HasName())
                            net.SetInputGate(gate);
                        else
                            throw new Exception("Top Level Nets should have a numerical name! Can not find this" +
                                            " for " + gate.GetGateType() + " with net id " + net.Id);
                    }
                    else //not dealing with the first gate anymore
                    {
                        net.SetInputGate(gate);
                    }
                }

                //Assign ouput gate, usualy the next gate in _gates list

                if(nextGate == null) //if nextGate is null then we have covered all gates
                    break; //breaking out of the loop

                //if next gate is not null, do assignement                
                Net[] inputsOfNextGate = nextGate.GetInputNets().Where(net => output.Id == net.Id).ToArray();
                if (inputsOfNextGate.Contains(output))
                    output.SetInputGate(nextGate);
            }

            //Assignements are done!
            _gatesAssignedToNets = true;
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
        /// Get total number of nets in all of the gates in this circuit
        /// </summary>
        /// <returns>number of total nets in every gate in this circuit</returns>
        public int GetNetsCount()
        {
            return _gates.Sum(gate => gate.GetNetsCount());
        }

        public IList<Gate> GetGates()
        {
            if (!_gatesAssignedToNets)
            {
                AssignGatesToNets();
            }

            return _gates;
        }

        public string GetFilePath()
        {
            return _file;
        }
    }
}
