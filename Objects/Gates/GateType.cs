using System.ComponentModel;

namespace SatSolver.Objects.Gates
{
    public enum GateType
    {
        [Description("INV")]
        [Gate("inv", 1)]
        Inv,
        [Description("OR")]
        [Gate("or", 2)]
        Or,
        [Description("AND")]
        [Gate("and", 2)]
        And,
        [Description("XOR")]
        [Gate("xor", 2)]
        Xor,
        [Description("ONE")]
        [Gate("one", 1)]
        One,
        [Description("ZERO")]
        [Gate("one", 1)]
        Zero
    }
}