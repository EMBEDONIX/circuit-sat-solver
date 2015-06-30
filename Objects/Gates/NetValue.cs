using System;
using System.ComponentModel;

namespace SatSolver.Objects.Gates
{
    [Serializable]
    public enum NetValue
    {
        [Description("0")] Low = 0,
        [Description("1")] High = 1,
        [Description("N/A")] NotSet = 2
    }
}