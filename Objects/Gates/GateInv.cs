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
    }
}
