using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SatSolver.Objects.Gates
{
    public class GateXor : Gate
    {
        public GateXor(GateType type) : base(type)
        {
        }

        public override void CalculateOutput()
        {
            throw new NotImplementedException();
        }

        public override string GetSymbol()
        {
            return "=1";
        }
    }
}
