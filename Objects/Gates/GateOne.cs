using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SatSolver.Objects.Gates
{
    public class GateOne : Gate
    {
        public GateOne(GateType type) : base(type)
        {
        }

        public override void CalculateOutput()
        {
            throw new NotImplementedException();
        }

        public override string GetSymbol()
        {
            return "1";
        }

        public override CNF GetCnf(int offset)
        {
            if (_lastCnfOffset == offset && _cnf != null)
                return _cnf;

            _lastCnfOffset = offset;

            return null;
        }
    }
}
