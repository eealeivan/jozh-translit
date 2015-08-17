using System;

namespace JoZhTranslit
{
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

        public int this[int index]
        {
            get { return _chars[index]; }
        }
    }
}