using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SatSolver.Objects.Gates;

namespace SatSolver.Objects
{
    public class MiterCircuit : Circuit
    {
        private bool _hasSingleOutput;
        private bool _hasDualOutputs;
        private bool _hasThreeOrMoreOutputs;
        private Circuit _circA, _circB;
        private List<Gate> _finalOutputsA = new List<Gate>();
        private List<Gate> _finalOutputsB = new List<Gate>();
        private GateOr _orFinal = new GateOr(GateType.Or);
        private List<GateXor> _xorGates = new List<GateXor>();
        private List<GateOr> _orGates = new List<GateOr>();

        public MiterCircuit(Circuit circA, Circuit circB)
        { 
            _gates = new List<Gate>();
            _circA = circA;
            _circB = circB;
            var maxB = _circB.GetHighestNetId();

            foreach (var gate in _circA.GetGates())
            {
                this.AddGate(gate, 0);
            }

            foreach (var gate in _circB.GetGates())
            {
                this.AddGate(gate, 0);
            }

            _finalOutputsA.AddRange(_circA.GetGates().Where(gate => gate.IsLastOutputGate()));
            _finalOutputsB.AddRange(_circB.GetGates().Where(gate => gate.IsLastOutputGate()));
            
            var c1 = _finalOutputsA.Count;
            var c2 = _finalOutputsB.Count;

            if (c1 != c2)
            {
                throw new Exception("Output Nets of 'CIRCUIT A' does not match the count of Output Nets of 'CIRCUIT B'");
            }

            
            if (_finalOutputsA.Count == 1 && _finalOutputsB.Count == 1)
            {

                _hasSingleOutput = true;
                _xorGates = new List<GateXor>();

                GateXor gx = new GateXor(GateType.Xor);
                gx.AddInputNet(_finalOutputsA[0].GetOutputNet());
                gx.AddInputNet(_finalOutputsB[0].GetOutputNet());
                gx.SetOutputNet(new Net("XORFINAL", ++maxB));
                _xorGates.Add(gx);
                _gates.Add(gx);
            }
            else if (_finalOutputsA.Count == 2 && _finalOutputsB.Count == 2) //CircuitA and B both have only 1 output
            {
                _hasDualOutputs = true;

                _xorGates = new List<GateXor>();
                _orGates = new List<GateOr>();

                for (int i = 0; i < _finalOutputsA.Count; i++)
                {
                    GateXor gx = new GateXor(GateType.Xor);

                    gx.AddInputNet(_finalOutputsA[i].GetOutputNet());
                    gx.AddInputNet(_finalOutputsB[i].GetOutputNet());
                    gx.SetOutputNet(new Net("XOR" + i, ++maxB));
                    _xorGates.Add(gx);

                    if (i >= 1 && i%2 != 0) //add or gate!
                    {
                        var og = new GateOr(GateType.Or);
                        og.IsFinalMiterOutput = true;
                        og.AddInputNet(_xorGates[i - 1].GetOutputNet());
                        og.AddInputNet(_xorGates[i].GetOutputNet());
                        og.SetOutputNet(new Net("OR", ++maxB));
                        _orGates.Add(og);
                    }
                }

                _gates.AddRange(_xorGates);
                _gates.AddRange(_orGates);
            }
            else
            {
                _hasThreeOrMoreOutputs = true;

                _xorGates = new List<GateXor>();

                for (int i = 0; i < _finalOutputsA.Count; i++)
                {
                    GateXor gx = new GateXor(GateType.Xor);

                    gx.AddInputNet(_finalOutputsA[i].GetOutputNet());
                    gx.AddInputNet(_finalOutputsB[i].GetOutputNet());
                    gx.SetOutputNet(new Net("XOR" + i, ++maxB));
                    _xorGates.Add(gx);
                }

                _gates.AddRange(_xorGates);
            }


        }

        public override List<CNF> GetMitterForInputs()
        {
            List<List<int>> cnf = new List<List<int>>(); //temporary
            List<CNF> cnfs = new List<CNF>(); //the cnf to return 
            List<Net> inputNetsA = new List<Net>();
            List<Net> inputNetsB = new List<Net>();
            inputNetsA.AddRange(_circA.GetInputGates().Where(g => g.IsTopLevelGate()).SelectMany(x => x.GetInputNets()));
            inputNetsB.AddRange(_circB.GetInputGates().Where(g => g.IsTopLevelGate()).SelectMany(x => x.GetInputNets()));


            var inA = inputNetsA.Distinct(new NetComparer()).ToList();
            var inB = inputNetsB.Distinct(new NetComparer()).ToList();

            //connect inputs!
            if (inA.Count != inB.Count)
            {
                throw new Exception("Input Nets of 'CIRCUIT' A does not match the count of Input Nets of 'CIRCUIT B'");
            }

            for (int i = 0; i < inA.Count; i++)
            {
                cnf.Add(new List<int> { inA[i].Id, -inB[i].Id });
                cnf.Add(new List<int> { -inA[i].Id, inB[i].Id });
            }


            if (_hasSingleOutput)
            {
                cnf.Add(new List<int> {_xorGates[0].GetOutputNet().Id});
            }
            else if(_hasDualOutputs)
            {
                cnf.Add(new List<int> { _orGates[0].GetOutputNet().Id });
            }
            else if(_hasThreeOrMoreOutputs)
            {
                for (int i = 0; i < _xorGates.Count; i++)
                {
                    cnf.Add(new List<int> { _xorGates[i].GetOutputNet().Id });
                }
            }

            cnfs.Add(new CNF(cnf));
            return cnfs;
        }
    }
}