using Algorithms.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.GCD
{
    public static class GCD
    {
        public static int Euclidean(int u, int v)
        {
            if (u == 0)
                return v;
            return Euclidean(v % u, u);
        }

        public static int Binary(int u, int v)
        {
            if (v == 0)
                return u;
            if (u == 0)
                return v;

            if (NumberHelper.IsEven(u) && NumberHelper.IsEven(v))
                return Binary(u >> 1, v >> 1) << 1;
            else if (NumberHelper.IsEven(u))
                return Binary(u >> 1, v);
            else if (NumberHelper.IsEven(v))
                return Binary(u, v >> 1);
            else if (u >= v)
                return Binary((u - v) >> 1, v);
            else
                return Binary(u, (v - u) >> 1);
        }
    }
}
