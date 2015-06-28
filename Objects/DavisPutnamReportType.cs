using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SatSolver.Objects
{
    public enum DpType
    {
        PerformingUnitClause,
        FindingPureLiterals,
        RemovingUintClause,
        RemovingPureLiteral,
        SolutionFound,
        Stopped,
        OnlyMessage,
        RemovingClause,
        FindingRightMostVariable,
        Backtracking,
        FoundUnitClause,
        Starting
    }
}
