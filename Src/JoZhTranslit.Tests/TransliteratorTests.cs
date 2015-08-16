using JoZhTranslit.TransliterationMaps;
using NUnit.Framework;

namespace JoZhTranslit.Tests
{
    [TestFixture]
    public class TransliteratorTests
    {
        private Transliterator _t;

        [SetUp]
        public void SetUp()
        {
            _t = new Transliterator(EnRu.MapJson);
        }
        
        [Test]
        public void Transliterate_LetterSingleGrapheme_CorrectValueReturned()
        {
            Assert.That(_t.Transliterate("s"), Is.EqualTo("с"));
        } 
        
        [Test]
        public void Transliterate_LetterUpperCaseSingleGrapheme_CorrectValueReturned()
        {
            Assert.That(_t.Transliterate("S"), Is.EqualTo("С"));
        } 
        
        [Test]
        public void Transliterate_LetterDoubleGrapheme_CorrectValueReturned()
        {
            Assert.That(_t.Transliterate("sh"), Is.EqualTo("ш"));
        }  
        
        [Test]
        public void Transliterate_LetterUpperCaseDoubleGrapheme_CorrectValueReturned()
        {
            Assert.That(_t.Transliterate("Sh"), Is.EqualTo("Ш"));
        } 
        
        [Test]
        public void Transliterate_LetterTripleGrapheme_CorrectValueReturned()
        {
            Assert.That(_t.Transliterate("shh"), Is.EqualTo("щ"));
        }
        
        [Test]
        public void Transliterate_LetterUpperCaseTripleGrapheme_CorrectValueReturned()
        {
            Assert.That(_t.Transliterate("Shh"), Is.EqualTo("Щ"));
        }

        [Test]
        public void Transliterate_LetterNonGrapheme_CorrectValueReturned()
        {
            Assert.That(_t.Transliterate("!"), Is.EqualTo("!"));
            Assert.That(_t.Transliterate("?"), Is.EqualTo("?"));
            Assert.That(_t.Transliterate(" "), Is.EqualTo(" "));
            Assert.That(_t.Transliterate("-"), Is.EqualTo("-"));
            Assert.That(_t.Transliterate("\""), Is.EqualTo("\""));
        }

        [Test]
        public void Transliterate_WordDifferentGraphemes_CorrectValueReturned()
        {
            Assert.That(_t.Transliterate("Chereshnja"), Is.EqualTo("Черешня"));
        }

        [Test]
        public void Transliterate_WordsDifferentCasesDifferentGraphemes_CorrectValueReturned()
        {
            Assert.That(
                _t.Transliterate("Shhuchka i jozh bol'shie druz'ja!"),
                Is.EqualTo("Щучка и ёж большие друзья!"));
        }
    }
}
