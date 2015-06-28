using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SatSolver.Objects.Gates;

namespace SatSolver.Objects
{
    public class Clause
    {
        public Gate Gate { get; private set; }
        public CNF Cnf { get; private set; }

        public Clause(Gate gate, CNF cnf)
        {
            Gate = gate;
            Cnf = cnf;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(Gate.GetGateTypeAsString());
            sb.AppendLine(Cnf.ToString());
            return sb.ToString();
        }
    }
}
