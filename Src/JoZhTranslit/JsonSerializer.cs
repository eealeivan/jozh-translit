using System.Collections.Generic;
using System.IO;
using System.Text;
#if (NET40 || NET35)
using System.Web.Script.Serialization;
#else
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
#endif

namespace JoZhTranslit
{
    internal static class JsonSerializer
    {
#if (NET40 || NET35)
        /// <summary>
        /// Deserializes a graphemes map dictionary from JSON
        /// </summary>
        public static IDictionary<string, string[]> DeserializeGraphemesMap(string mapJson)
        {
            var serializer = new JavaScriptSerializer();
            return serializer.Deserialize<IDictionary<string, string[]>>(mapJson);
        }
#else
        /// <summary>
        /// Deserializes a graphemes map dictionary from JSON
        /// </summary>
        public static IDictionary<string, string[]> DeserializeGraphemesMap(string mapJson)
        {
            var wrappedMapJson = @"{ ""map"": " + mapJson + "}";

            var settings = new DataContractJsonSerializerSettings { UseSimpleDictionaryFormat = true };
            var serializer = new DataContractJsonSerializer(typeof(MapWrapper), settings);

            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(wrappedMapJson)))
            {
                var mapWrapper = serializer.ReadObject(stream) as MapWrapper;
                return mapWrapper == null ? null : mapWrapper.Map;
            }
        }

        [DataContract]
        private class MapWrapper
        {
            [DataMember(Name = "map")]
            public IDictionary<string, string[]> Map { get; set; }
        }
#endif
    }
}