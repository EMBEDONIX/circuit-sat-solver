using System;
using System.Collections.Generic;
using System.Text;

namespace SatSolver.Objects
{
    [Serializable]
    public class CNF
    {
        public List<List<int>> Data;

        public CNF()
        {
            Data = new List<List<int>>();
        }

        public CNF(List<List<int>> data)
        {
            Data = data;
        }

        public void AddCnf(CNF cnf)
        {
            Data.AddRange(cnf.Data);
        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.Append("{");
            for (int i = 0; i < Data.Count; i++)
            {
                sb.Append("{");

                for (int j = 0; j < Data[i].Count; j++)
                {
                    sb.Append(Data[i][j]);
                    if (j != Data[i].Count - 1)
                    {
                        sb.Append(",");
                    }
                }


                if (i == Data.Count - 1)
                {
                    sb.Append("}");
                }
                else
                {
                    sb.Append("},");
                }
            }

            sb.Append("}");
            return sb.ToString();
        }

        public List<string> ToStringAsLines()
        {
            var ret = new List<string>();
            var str = this.ToString();
            return ret;
        }
    }
}