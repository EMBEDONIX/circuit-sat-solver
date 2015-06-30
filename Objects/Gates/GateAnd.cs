using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SatSolver.Objects.Gates
{
    [Serializable]
    public class GateAnd : Gate
    {
        public GateAnd(GateType type) : base(type)
        {
        }

        public override void CalculateOutput()
        {
            throw new NotImplementedException();
        }

        public override string GetSymbol()
        {
            return "&";
        }

        public override CNF GetCnf(int offset)
        {
            if (_lastCnfOffset == offset && _cnf != null)
                return _cnf;

            _lastCnfOffset = offset;

            List<List<int>> cnf = new List<List<int>>();

            foreach (var i in _inNets)
            {
                cnf.Add(new List<int>()
                {
                    i.Id + offset,
                    -(_outNet.Id + offset)
                });
            }

            if (_inNets.Count == 2)
            {
                cnf.Add(new List<int>()
                {
                    -(_inNets[0].Id + offset),
                    -(_inNets[1].Id + offset),
                    _outNet.Id + offset
                });
            }

            _cnf = new CNF(cnf);
            return _cnf;
        }
    }
}