using System;

namespace SatSolver.Objects.Gates
{
    public class GateAttribute : Attribute
    {

        public string Name { get; private set; }
        public int MaxInputs { get; private set; }

        public GateAttribute(string name, int maxInputs)
        {
            Name = name;
            MaxInputs = maxInputs;
        }
    }
}
