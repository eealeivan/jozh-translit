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

        public static int GetHashCodeAsCharArray(CharArray ca)
        {
            if (ca == null || ca.Length <= 0)
            {
                return 0;
            }

            unchecked
            {
                int hash = ca[0];
                for (int i = 1; i < ca.Length; i++)
                {
                    hash = hash * 397 ^ ca[i];
                }
                return hash;
            }
        }
    }
}