using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SatSolver.Objects
{
    public static class Extensions
    {
        public static string DumpList(this List<int> list)
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < list.Count; i++)
            {
                if (i == 0)
                {
                    sb.Append("{");
                }

                sb.Append(list[i]);

                if (i != list.Count - 1)
                {
                    sb.Append(",");
                }
                else
                {
                    sb.Append("}");
                }
            }

            return sb.ToString();
        }

        public static IList<T> Clone<T>(this IList<T> listToClone) where T : ICloneable
        {
            return listToClone.Select(item => (T)item.Clone()).ToList();
        }
    }
}
