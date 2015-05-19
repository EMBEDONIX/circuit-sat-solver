using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SatSolver.Objects;

namespace SatSolver.UserInterface.CustomControls
{
    public class CircuitLoadedEventArgs : EventArgs
    {
        public Circuit Circuit { get; private set; }
        public int TreeId { get; private set; }

        public CircuitLoadedEventArgs(Circuit circuit, int id)
        {
            Circuit = circuit;
            TreeId = id;
        }
    }
}
