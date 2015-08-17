using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace JoZhTranslit
{
    internal sealed class TranslitData
    {
        private readonly IDictionary<int, string> _reverseMap;
        private readonly int _maxGraphemeLength;

        public TranslitData(string mapJson)
        {
            IDictionary<string, string[]> map;
            if (String.IsNullOrWhiteSpace(mapJson))
            {
                throw new ArgumentNullException("mapJson");
            }

            try
            {
                var serializer = new JavaScriptSerializer();
                map = serializer.Deserialize<IDictionary<string, string[]>>(mapJson);
            }
            catch (Exception e)
            {
                throw new Exception("Error parsing 'mapJson' argument.", e);
            }

            _reverseMap = new Dictionary<int, string>();
            int maxGraphemeLength = 0;
            foreach (var mapEntry in map)
            {
                foreach (string valueEntry in mapEntry.Value)
                {
                    _reverseMap[HashHelper.GetHashCodeAsCharArray(valueEntry)] = mapEntry.Key;
                    if (valueEntry.Length > maxGraphemeLength)
                    {
                        maxGraphemeLength = valueEntry.Length;
                    }
                }
            }

            _maxGraphemeLength = maxGraphemeLength;
        }

        public int MaxGraphemeLength
        {
            get { return _maxGraphemeLength; }
        }

        public string FindGrapheme(int inGraphemeHash)
        {
            string outGrapheme;
            _reverseMap.TryGetValue(inGraphemeHash, out outGrapheme);
            return outGrapheme;
        }
    }
}