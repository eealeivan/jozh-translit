using System.Diagnostics;

namespace JoZhTranslit
{
    internal sealed class TranslitProcessor
    {
        private readonly TranslitData _translitData;
        private readonly CharArray _inGrapheme;

        public TranslitProcessor(TranslitData translitData)
        {
            Debug.Assert(translitData != null);
            
            _translitData = translitData;
            _inGrapheme = new CharArray(_translitData.MaxGraphemeLength);
        }

        public AddCharResult AddChar(char c)
        {
            if (_inGrapheme.Length >= _translitData.MaxGraphemeLength)
            {
                _inGrapheme.Clear();
            }

            _inGrapheme.Append(c);
            string outGrapheme = _translitData.FindGrapheme(
                HashHelper.GetHashCodeAsCharArray(_inGrapheme));

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