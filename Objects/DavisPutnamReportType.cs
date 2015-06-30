using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SatSolver.Objects
{
    public enum DpType
    {
        PerformingUnitClause,
        PerformingPureLiteralRule,
        RemovingUintClause,
        RemovingPureLiteral,
        SolutionFound,
        Stopped,
        OnlyMessage,
        RemovingClause,
        FindingRightMostVariable,
        PerformingBacktrack,
        FoundUnitClause,
        Starting
    }
}