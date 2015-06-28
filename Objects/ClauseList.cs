using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SatSolver.Objects
{
    public class ClauseList
    {
        public List<Clause> Clauses { get; private set; }

        public ClauseList()
        {
            Clauses = new List<Clause>();
        }

        public ClauseList(List<Clause> clauses)
        {
            Clauses = clauses;
        }

        public void AddClause(Clause clause)
        {
            Clauses.Add(clause);
        }
    }
}
