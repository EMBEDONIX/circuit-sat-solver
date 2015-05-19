using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SatSolver.Objects.Gates
{
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
    }
}
