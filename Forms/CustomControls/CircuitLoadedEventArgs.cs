using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SatSolver.Objects;

namespace SatSolver.UserInterface.CustomControls
{
    /// <summary>
    /// Event Arguments when a circuit is loaded in <see cref="NetListTreeView"/>
    /// </summary>
    public class CircuitLoadedEventArgs : EventArgs
    {
        /// <summary>
        /// The <see cref="Circuit"/> which is loaded
        /// </summary>
        public Circuit Circuit { get; private set; }

        /// <summary>
        /// Id of the <see cref="NetListTreeView"/> that loaded the <see cref="Circuit"/>
        /// </summary>
        public int TreeId { get; private set; }

        /// <summary>
        /// Construct a circuit load event arguments
        /// </summary>
        /// <param name="circuit"></param>
        /// <param name="id"></param>
        public CircuitLoadedEventArgs(Circuit circuit, int id)
        {
            Circuit = circuit;
            TreeId = id;
        }
    }
}