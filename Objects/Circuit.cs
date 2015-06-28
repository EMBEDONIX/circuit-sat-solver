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
        protected bool _gatesAssignedToNets;
        protected List<Gate> _gates;
        protected List<Gate> _inputGates;
        protected List<Gate> _middleGates;
        protected Gate _outputGate;
        protected string _file;
        protected int _offset;


        public Circuit(string file, int offset)
        {
            _offset = offset;
            _gates = new List<Gate>();
            _file = file;
        }

        public Circuit(string file)
        {
            _offset = 0;
            _gates = new List<Gate>();
            _file = file;
        }

        internal void AddGate(Gate gate, int offset)
        {
            foreach (var net in gate.GetAllNets())
            {
                net.Id += offset;
            }

            _gates.Add(gate);
        }

        public Circuit()
        {
            _gates = new List<Gate>();
            _file = "MITER CIRCUIT";
        }

        public void AddGate(Gate gate)
        {
            _gates.Add(gate);
        }

        public int GetHighestNetId()
        {
            int max = Int32.MinValue;
            foreach (var gate in _gates)
            {
                foreach (var net in gate.GetAllNets())
                {
                    max = Math.Max(max, net.Id);
                }
            }

            return max;
        }


        //TODO move this method to the Circuit object
        /// <summary>
        /// Assigns gates to nets (input and output nets)
        /// </summary>
        private void AssignGatesToNets()
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

        /// <summary>
        /// Get gates that are top level input gates
        ///     These are the gates that their input net(s) has a name assigned.
        /// </summary>
        /// <returns></returns>
        public IList<Gate> GetInputGates()
        {
            if (_inputGates == null || _inputGates.Count == 0)
            {
                _inputGates = new List<Gate>();
                _inputGates = _gates.Where(gate => gate.GetInputNets().Any(x => x.HasName())).ToList();
            }

            if (_inputGates == null || _inputGates.Count == 0)
                throw new Exception(GetName() + " Does not have any top level input gates!");
            return _inputGates;
        }

        /// <summary>
        /// Get the gate that is the final gate of the circuit which its output will determine
        /// the circuit result. This is the gate that its output net has a name assigned.
        /// </summary>
        /// <returns></returns>
        public Gate GetOutputGate()
        {
            if (_outputGate == null)
            {
                _outputGate = _gates.FirstOrDefault(g => g.GetOutputNet().HasName());
            }


            if (_outputGate == null)
                throw new Exception(GetName() + " Does not have an output gate!");
            return _outputGate;
            
        }

        /// <summary>
        /// Middle gates are the gates that sit between top level and output gate
        /// </summary>
        /// <returns></returns>
        public List<Gate> GetMiddleGates()
        {
            if (_middleGates == null || _middleGates.Count == 0)
            {
                _middleGates = new List<Gate>();
              
                foreach (var gate in _gates)
                {
                    bool shouldAdd = true;

                    foreach (var net in gate.GetAllNets())
                    {
                        if (net.HasName())
                            shouldAdd = false;
                    }

                    if(shouldAdd)
                        _middleGates.Add(gate);
                }
            }

            return _middleGates;



        }

        public Gate GetFinalOrGateId()
        {
            return _gates.Where(gate => gate.GetGateType() == GateType.Or).FirstOrDefault(gate => gate.IsFinalMiterOutput);
        }

        //TODO this is fucking bad OO design....but my deadline closes!
        public virtual List<CNF> GetMitterForInputs()
        {
            return null;
        }

    }
}
