using System.Collections.Generic;
using NUnit.Framework;

namespace JoZhTranslit.Tests
{
    [TestFixture]
    public class JsonMapParserTests
    {
        private const string MapJson = @"
{
    ""А"": [""A""],
	""а"": [""a""],
	""Ё"": [""Jo"", ""Yo""],
	""ё"": [""jo"", ""yo""]
}
";

        [Test]
        public void DeserializeGraphemesMap_DeserializesMapJson()
        {
            var parseResult = JsonMapParser.Parse(MapJson);

            Assert.That(parseResult.MaxGraphemeLength, Is.EqualTo(2));

            IDictionary<int, string> map = parseResult.GraphemesMap;
            Assert.That(map.Count, Is.EqualTo(6));
            Assert.That(map[new CharArray("A").GetMutableHashCode()], Is.EqualTo("А"));
            Assert.That(map[new CharArray("a").GetMutableHashCode()], Is.EqualTo("а"));
            Assert.That(map[new CharArray("Jo").GetMutableHashCode()], Is.EqualTo("Ё"));
            Assert.That(map[new CharArray("Yo").GetMutableHashCode()], Is.EqualTo("Ё"));
            Assert.That(map[new CharArray("jo").GetMutableHashCode()], Is.EqualTo("ё"));
            Assert.That(map[new CharArray("yo").GetMutableHashCode()], Is.EqualTo("ё"));
        }
    }
}