using System;
using System.Text;

namespace JoZhTranslit
{
    /// <summary>
    /// Transliterator that can be used to transliterate static texts.
    /// </summary>
    public sealed class Transliterator
    {
        private readonly TranslitData _translitData;

        /// <summary>
        /// Creates an instance of <see cref="Transliterator"/> class.
        /// </summary>
        /// <param name="mapJson">
        /// Graphemes map in JSON format. Example: { "б": ["b"], "ё": ["jo", "yo"] }
        /// </param>
        public Transliterator(string mapJson)
        {
            _translitData = new TranslitData(mapJson);
        }

        /// <summary>
        /// Transliterates <paramref name="input"/> string according to provided map.
        /// </summary>
        /// <param name="input">Text that will be transliterated.</param>
        public string Transliterate(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }

            var processor = new TranslitProcessor(_translitData);
            var output = new StringBuilder();
            int lastGrahemeLength = 0;

            foreach (char c in input)
            {
                var addResult = processor.AddChar(c);
                switch (addResult.Status)
                {
                    case AddCharStatus.NoGraphemeFound:
                        output.Append(c);
                        lastGrahemeLength = 0;
                        break;
                    case AddCharStatus.NewGrapheme:
                        output.Append(addResult.Grapheme);
                        lastGrahemeLength = addResult.Grapheme.Length;
                        break;
                    case AddCharStatus.SubstitutePreviousGrapheme:
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
