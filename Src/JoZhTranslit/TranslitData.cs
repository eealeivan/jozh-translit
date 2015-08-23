using System;
using System.Collections.Generic;

namespace JoZhTranslit
{
    internal sealed class TranslitData
    {
        private readonly IDictionary<int, string> _graphemesMap;
        private readonly int _maxGraphemeLength;

        public TranslitData(string mapJson)
        {
            if (String.IsNullOrEmpty(mapJson))
            {
                throw new ArgumentNullException("mapJson");
            }

            var parseResult = JsonMapParser.Parse(mapJson);

            _graphemesMap = parseResult.GraphemesMap;
            _maxGraphemeLength = parseResult.MaxGraphemeLength;
        }

        public int MaxGraphemeLength
        {
            get { return _maxGraphemeLength; }
        }

        public string FindGrapheme(int inGraphemeHash)
        {
            string outGrapheme;
            _graphemesMap.TryGetValue(inGraphemeHash, out outGrapheme);
            return outGrapheme;
        }
    }
}