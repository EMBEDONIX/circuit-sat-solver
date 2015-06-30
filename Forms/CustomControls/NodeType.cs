using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SatSolver.UserInterface.CustomControls
{
    /// <summary>
    /// Enumeration of possible different node types
    /// </summary>
    public enum NodeType
    {
        /// <summary>
        /// Represents a <see cref="Circuit"/>
        /// </summary>
        Circuit,

        /// <summary>
        /// Represents a <see cref="Gate"/>
        /// </summary>
        Gate,

        /// <summary>
        /// represents a <see cref="Net"/>
        /// </summary>
        Net,

        /// <summary>
        /// If a node is default, it means no data is loaded
        /// </summary>
        Default
    }
}