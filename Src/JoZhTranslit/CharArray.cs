using System;
using System.Text;

namespace JoZhTranslit
{
    /// <summary>
    /// Lightweight version of a StringBuilder. If <see cref="GetMutableHashCode"/> is used
    /// then capacity should be small. Used for holding grapheme char values.
    /// </summary>
    internal sealed class CharArray
    {
        private readonly int _capacity;
        private readonly char[] _chars;
        private int _length;

        public int Capacity { get { return _capacity; } }
        public int Length { get { return _length; } }

        public CharArray(int capacity)
        {
            _capacity = capacity;
            _chars = new char[capacity];
            _length = 0;
        }

        public CharArray(string s)
        {
            _chars = s.ToCharArray();
            _capacity = _chars.Length;
            _length = _chars.Length;
        }

        public CharArray(StringBuilder sb)
        {
            _length = sb.Length;
            _capacity = _length;
            _chars = new char[_length];
            for (int i = 0; i < _length; i++)
            {
                _chars[i] = sb[i];
            }
        }

        public void Append(char c)
        {
            if (_length >= _capacity)
            {
                throw new InvalidOperationException(
                    string.Format("Length can't be greater than {0}", _capacity));
            }

            _chars[_length++] = c;
        }

        public void Clear()
        {
            _length = 0;
        }
        
        /// <summary>
        /// Calculates a hash code based on values of inner array of chars.
        /// </summary>
        public int GetMutableHashCode()
        {
            if (_length <= 0)
            {
                return 0;
            }

            unchecked
            {
                int hash = _chars[0];
                for (int i = 1; i < _length; i++)
                {
                    hash = hash * 397 ^ _chars[i];
                }
                return hash;
            }
        }
    }
}