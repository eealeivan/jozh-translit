using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;

namespace JoZhTranslit
{
    internal sealed class TranslitData
    {
        private readonly IDictionary<string, string> _reverseMap;
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

            _reverseMap = new Dictionary<string, string>();
            foreach (var mapEntry in map)
            {
                foreach (string valueEntry in mapEntry.Value)
                {
                    _reverseMap[valueEntry] = mapEntry.Key;
                }
            }

            _maxGraphemeLength = _reverseMap.Keys.Max(x => x.Length);
        }

        public int MaxGraphemeLength
        {
            get { return _maxGraphemeLength; }
        }

        public string FindGrapheme(string inGrapheme)
        {
            string outGrapheme;
            _reverseMap.TryGetValue(inGrapheme, out outGrapheme);
            return outGrapheme;
        }
    }
}