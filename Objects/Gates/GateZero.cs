﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SatSolver.Objects.Gates
{
    public class GateZero : Gate
    {
        public GateZero(GateType type) : base(type)
        {
        }

        public override void CalculateOutput()
        {
            throw new NotImplementedException();
        }
    }
}
