using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorithms.String.Search
{
    public static class BoyerMoore
    {
        public static int IndexOf(IEnumerable<char> hayStack, IEnumerable<char> needle)
        {
            return IndexOf(hayStack.ToArray(), needle.ToArray());
        }

        public static int IndexOf(char[] hayStack, char[] needle)
        {
            if (needle.Length == 0)
                return 0;

            int[] characterTable = ConstructCharacterTable(needle);
            int[] offsetTable = ConstructOffsetTable(needle);

            for (int i = needle.Length - 1, j; i < hayStack.Length;)
            {
                for (j = needle.Length - 1; needle[j] == hayStack[i]; --i, --j)
                {
                    if (j == 0)
                    {
                        return i;
                    }
                }
                // i += needle.length - j; // For naive method
                i += Math.Max(offsetTable[needle.Length - 1 - j], characterTable[hayStack[i]]);
            }
            return -1;
        }

        private static int[] ConstructCharacterTable(char[] needle)
        {
            int size = char.MaxValue + 1;
            int[] table = new int[size];
            for (int i = 0; i < needle.Length; i++)
            {
                table[i] = needle.Length;
            }

            for (int i = 0; i < needle.Length; i++)
            {
                table[needle[i]] = needle.Length - 1 - i;
            }

            return table;
        }

        private static int[] ConstructOffsetTable(char[] needle)
        {
            int[] table = new int[needle.Length];
            int lastPrefixPosition = needle.Length;
            for (int i = needle.Length; i > 0; --i)
            {
                if (isPrefix(needle, i))
                {
                    lastPrefixPosition = i;
                }
                table[needle.Length - i] = lastPrefixPosition - i + needle.Length;
            }
            for (int i = 0; i < needle.Length - 1; ++i)
            {
                int slen = suffixLength(needle, i);
                table[slen] = needle.Length - 1 - i + slen;
            }
            return table;
        }

        private static bool isPrefix(char[] needle, int p)
        {
            for (int i = p, j = 0; i < needle.Length; ++i, ++j)
            {
                if (needle[i] != needle[j])
                {
                    return false;
                }
            }
            return true;
        }

        private static int suffixLength(char[] needle, int p)
        {
            int len = 0;
            for (int i = p, j = needle.Length - 1;
                     i >= 0 && needle[i] == needle[j]; --i, --j)
            {
                len += 1;
            }
            return len;
        }
    }
}
