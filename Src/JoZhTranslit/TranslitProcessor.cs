using System;
using System.Diagnostics;
using System.Text;

namespace JoZhTranslit
{
    internal sealed class TranslitProcessor
    {
        private readonly Func<string, string> _mapFn;
        private readonly int _maxGraphemeLength;

        private readonly StringBuilder _inGrapheme;

        public TranslitProcessor(Func<string, string> mapFn, int maxGraphemeLength)
        {
            Debug.Assert(mapFn != null);

            _mapFn = mapFn;
            _maxGraphemeLength = maxGraphemeLength;

            _inGrapheme = new StringBuilder(_maxGraphemeLength);
        }

        public AddCharResult AddChar(char c)
        {
            if (_inGrapheme.Length >= _maxGraphemeLength)
            {
                _inGrapheme.Clear();
            }

            _inGrapheme.Append(c);
            string outGrapheme = _mapFn(_inGrapheme.ToString());

            if (outGrapheme != null)
            {
                return _inGrapheme.Length == 1
                    ? new AddCharResult(AddCharStatus.NewGrapheme, outGrapheme)
                    : new AddCharResult(AddCharStatus.SubstitutePreviousGrapheme, outGrapheme);
            }

            if (_inGrapheme.Length > 1)
            {
                _inGrapheme.Clear();
                return AddChar(c);
            }

            return new AddCharResult(AddCharStatus.NoGraphemeFound, null);
        }

        public void Reset()
        {
            _inGrapheme.Clear();
        }
    }
}