using System.Text;

namespace JoZhTranslit
{
    internal sealed class HashHelper
    {
        public static int GetHashCodeAsCharArray(string s)
        {
            if (s == null || s.Length <= 0)
            {
                return 0;
            }

            unchecked
            {
                int hash = s[0];
                for (int i = 1; i < s.Length; i++)
                {
                    hash = hash * 397 ^ s[i];
                } 
                return hash;
            }
        }

        public static int GetHashCodeAsCharArray(StringBuilder sb)
        {
            if (sb == null || sb.Length <= 0)
            {
                return 0;
            }

            unchecked
            {
                int hash = sb[0];
                for (int i = 1; i < sb.Length; i++)
                {
                    hash = hash * 397 ^ sb[i];
                }
                return hash;
            }
        }
    }
}