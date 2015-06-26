using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SatSolver.Objects.Gates
{
    /// <summary>
    /// Represents an inverter gate
    /// </summary>
    public class GateInv : Gate
    {
        public GateInv(GateType type) : base(type)
        {
        }

        public override void CalculateOutput()
        {   
            throw new NotImplementedException();
        }

        public override string GetSymbol()
        {
            return "!1";
        }

        public override CNF GetCnf(int offset)
        {
            if (_lastCnfOffset == offset && _cnf != null)
                return _cnf;

            _lastCnfOffset = offset;

            List<List<int>> cnf = new List<List<int>>();

            cnf.Add(new List<int> { _inNets[0].Id + offset , _outNet.Id + offset });
            cnf.Add(new List<int> { -(_inNets[0].Id + offset), -(_outNet.Id + offset) });

            _cnf = new CNF(cnf);
            return _cnf;
        }
    }
}
