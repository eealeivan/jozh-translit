using System;
using System.Text;

namespace JoZhTranslit
{
    public sealed class Transliterator
    {
        private readonly TranslitData _translitData;

        public Transliterator(string mapJson)
        {
            _translitData = new TranslitData(mapJson);
        }

        public string Transliterate(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }

            var processor = new TranslitProcessor(_translitData.FindGrapheme, _translitData.MaxGraphemeLength);
            var output = new StringBuilder();
            int lastGrahemeLength = 0;

            foreach (char c in input)
            {
                var addResult = processor.AddChar(c);
                switch (addResult.Status)
                {
                    case TranslitProcessor.AddCharStatus.NoGraphemeFound:
                        output.Append(c);
                        lastGrahemeLength = 0;
                        break;
                    case TranslitProcessor.AddCharStatus.NewGrapheme:
                        output.Append(addResult.Grapheme);
                        lastGrahemeLength = addResult.Grapheme.Length;
                        break;
                    case TranslitProcessor.AddCharStatus.SubstitutePreviousGrapheme:
                        output.Remove(output.Length - lastGrahemeLength, lastGrahemeLength);
                        output.Append(addResult.Grapheme);
                        lastGrahemeLength = addResult.Grapheme.Length;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            return output.ToString();
        }
    }
}
