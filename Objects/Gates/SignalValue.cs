using System.ComponentModel;

namespace SatSolver.Objects.Gates
{
    public enum SignalValue
    {
        [Description("0")]
        Low = 0,
        [Description("1")]
        High = 1,
        [Description("N/A")]
        NotSet = 2
    }
}
