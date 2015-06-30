using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SatSolver.Objects.Gates
{
    [Serializable]
    public class NetComparer : EqualityComparer<Net>
    {
        public override bool Equals(Net x, Net y)
        {
            return (x.Id == y.Id && x.Name == y.Name);
        }

        public override int GetHashCode(Net obj)
        {
            //return obj.Id.GetHashCode() ^ obj.Name.GetHashCode();
            return 0;
            //return 13 * Id.GetHashCode() + 7 * Name.GetHashCode();
        }
    }

}
