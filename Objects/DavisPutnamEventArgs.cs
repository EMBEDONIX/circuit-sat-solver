using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SatSolver.Objects
{
    public class DavisPutnamEventArgs : EventArgs
    {
        public String Message { get; private set; }
        public int Level { get; private set; }
        public CNF CurrentCnf { get; private set; }
        public DpType Type { get; private set; }
        public List<KeyValuePair<int, bool>> UsedValues { get; private set; }

        public DavisPutnamEventArgs(string message, int level, CNF currentCnf, DpType type,
            List<KeyValuePair<int, bool>> usedValues)
        {
            Message = message;
            Level = level;
            CurrentCnf = currentCnf;
            Type = type;
            UsedValues = usedValues;
        }
    }
}