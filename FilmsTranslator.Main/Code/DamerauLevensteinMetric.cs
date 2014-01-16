using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmsTranslator.Main.Code
{
    public class DamerauLevensteinMetric
    {
        public int GetDistance(string str1, string str2)
        {
            if (str1 == str2) return 0;

            if (String.IsNullOrEmpty(str1) || String.IsNullOrEmpty(str2))
                return (str1 ?? String.Empty).Length + (str2 ?? String.Empty).Length;

            if (str1.Length > str2.Length)
            {
                var tmp = str1;
                str1 = str2;
                str2 = tmp;
            }

            if (str2.Contains(str1)) return str2.Length - str1.Length;

            int m = str1.Length;
            int n = str2.Length;
            int[,] H = new int[m + 2, n + 2];
            int INF = m + n;
            H[0, 0] = INF;

            for (int i = 0; i <= m; i++) { H[i + 1, 1] = i; H[i + 1, 0] = INF; }
            for (int j = 0; j <= n; j++) { H[1, j + 1] = j; H[0, j + 1] = INF; }

            SortedDictionary<char, int> sd = new SortedDictionary<char, int>();
            foreach (char l in (str1 + str2))
                if (!sd.ContainsKey(l))
                    sd.Add(l, 0);

            for (int i = 1; i <= m; i++)
            {
                int db = 0;
                for (int j = 1; j <= n; j++)
                {
                    int i1 = sd[str2[j - 1]];
                    int j1 = db;

                    if (str1[i - 1] == str2[j - 1])
                    {
                        H[i + 1, j + 1] = H[i, j];
                        db = j;
                    }
                    else H[i + 1, j + 1] = Math.Min(H[i, j], Math.Min(H[i + 1, j], H[i, j + 1])) + 1;
                    H[i + 1, j + 1] = Math.Min(H[i + 1, j + 1], H[i1, j1] + (i - i1 - 1) + 1 + (j - j1 - 1));
                }
                sd[str1[i - 1]] = i;
            }
            return H[m + 1, n + 1];
        }

    }
}
