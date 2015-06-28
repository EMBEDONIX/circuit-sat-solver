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
        private Circuit _circA, _circB;
        private Circuit _miter;

        public MiterCircuit(Circuit circA, Circuit circB)
        {
            _circA = circA;
            _circB = circB;
            var maxB = _circB.GetHighestNetId();

            _miter = new Circuit();

            foreach (var gate in _circA.GetGates())
            {
                _miter.AddGate(gate, 0);
            }

            foreach (var gate in _circB.GetGates())
            {
                _miter.AddGate(gate, 0);
            }

            List<Gate> finalOutputsA, finalOutputsB;
           //finalOutputsA = finalOutputsB = new List<Gate>(); //lol this make them joied! WTF!
           finalOutputsA = new List<Gate>();
           finalOutputsB = new List<Gate>();

            finalOutputsA.AddRange(_circA.GetGates().Where(gate => gate.IsLastOutputGate()));
            finalOutputsB.AddRange(_circB.GetGates().Where(gate => gate.IsLastOutputGate()));

            if (finalOutputsA.Count != finalOutputsB.Count)
            {
                throw new Exception(
                    $"Output net count of {_circA.GetName()} does not match to {_circB.GetName()} ouput counts!");
            }


            var xor1 = new GateXor(GateType.Xor);
            var xor2 = new GateXor(GateType.Xor);

            if (finalOutputsA.Count == 2 && finalOutputsB.Count == 2)
            {
                xor1.AddInputNet(finalOutputsA[0].GetOutputNet());
                xor1.AddInputNet(finalOutputsB[0].GetOutputNet());

                xor2.AddInputNet(finalOutputsA[1].GetOutputNet());
                xor2.AddInputNet(finalOutputsB[1].GetOutputNet());

                xor1.SetOutputNet(new Net("XOR1", ++maxB));
                xor2.SetOutputNet(new Net("XOR2", ++maxB));

                _miter.AddGate(xor1);
                _miter.AddGate(xor2);

                GateOr or = new GateOr(GateType.Or);
                or.IsFinalMiterOutput = true;
                or.AddInputNet(xor1.GetOutputNet());
                or.AddInputNet(xor2.GetOutputNet());
                or.SetOutputNet(new Net("FINAL", ++maxB));
                _miter.AddGate(or);
            }

            _gates = new List<Gate>(_miter.GetGates());
        }

        public override List<CNF> GetMitterForInputs()
        {
            List<CNF> cnfs = new List<CNF>();
            List<Gate> inputGates =  new List<Gate>();
            inputGates.AddRange(_circB.GetInputGates());
            inputGates.AddRange(_circA.GetInputGates());

            List<Net> inNets = new List<Net>();

            foreach (var gate in inputGates)
            {
                inNets.AddRange(gate.GetInputNets());
            }

            List<int> netIds = new List<int>();

            foreach (var inNet in inNets)
            {
                netIds.Add(inNet.Id);
            }

            var uid = netIds.Distinct();
            
            var shit = uid.ToList();
            shit.Sort();
            var f = shit;


            if (inputGates.Count == 4)
            {
                List<List<int>> cnf = new List<List<int>>();

                cnf.Add(new List<int> { -f[0], f[2] });
                cnf.Add(new List<int> { f[0], -f[2] });

                cnfs.Add(new CNF(cnf));

                cnf = new List<List<int>>();

                cnf.Add(new List<int> { -f[1], f[3] });
                cnf.Add(new List<int> { f[1], -f[3] });

                cnfs.Add(new CNF(cnf));

            }

            return cnfs;
        }
    }


}
