using JoZhTranslit.TransliterationMaps;
using NUnit.Framework;

namespace JoZhTranslit.Tests
{
    [TestFixture]
    public class TransliteratorLiveTests
    {
        private TransliteratorLive _t;

        [SetUp]
        public void SetUp()
        {
            _t = new TransliteratorLive(EnRu.MapJson);
        }

        [Test]
        public void AddChar_WordDifferentGraphemes_CorrectValueReturned()
        {
            AddCharAndCheck('C', AddCharStatus.NewGrapheme, "Ц");
            AddCharAndCheck('h', AddCharStatus.SubstitutePreviousGrapheme, "Ч");
            AddCharAndCheck('a', AddCharStatus.NewGrapheme, "а");
            AddCharAndCheck('h', AddCharStatus.NewGrapheme, "х");
            AddCharAndCheck('l', AddCharStatus.NewGrapheme, "л");
            AddCharAndCheck('y', AddCharStatus.NewGrapheme, "ы");
            AddCharAndCheck('j', AddCharStatus.NewGrapheme, "й");
            AddCharAndCheck(' ', AddCharStatus.NoGraphemeFound, null);
            AddCharAndCheck('J', AddCharStatus.NewGrapheme, "Й");
            AddCharAndCheck('e', AddCharStatus.SubstitutePreviousGrapheme, "Э");
            AddCharAndCheck('d', AddCharStatus.NewGrapheme, "д");
            AddCharAndCheck('i', AddCharStatus.NewGrapheme, "и");
            AddCharAndCheck('p', AddCharStatus.NewGrapheme, "п");
            AddCharAndCheck(',', AddCharStatus.NoGraphemeFound, null);
            AddCharAndCheck(' ', AddCharStatus.NoGraphemeFound, null);
            AddCharAndCheck('s', AddCharStatus.NewGrapheme, "с");
            AddCharAndCheck('y', AddCharStatus.NewGrapheme, "ы");
            AddCharAndCheck('n', AddCharStatus.NewGrapheme, "н");
            AddCharAndCheck(' ', AddCharStatus.NoGraphemeFound, null);
            AddCharAndCheck('L', AddCharStatus.NewGrapheme, "Л");
            AddCharAndCheck('a', AddCharStatus.NewGrapheme, "а");
            AddCharAndCheck('j', AddCharStatus.NewGrapheme, "й");
            AddCharAndCheck('a', AddCharStatus.SubstitutePreviousGrapheme, "я");
            AddCharAndCheck(',', AddCharStatus.NoGraphemeFound, null);
            AddCharAndCheck(' ', AddCharStatus.NoGraphemeFound, null);
            AddCharAndCheck('v', AddCharStatus.NewGrapheme, "в");
            AddCharAndCheck('#', AddCharStatus.NewGrapheme, "ъ");
            AddCharAndCheck('e', AddCharStatus.NewGrapheme, "е");
            AddCharAndCheck('z', AddCharStatus.NewGrapheme, "з");
            AddCharAndCheck('z', AddCharStatus.NewGrapheme, "з");
            AddCharAndCheck('h', AddCharStatus.SubstitutePreviousGrapheme, "ж");
            AddCharAndCheck('a', AddCharStatus.NewGrapheme, "а");
            AddCharAndCheck('e', AddCharStatus.NewGrapheme, "е");
            AddCharAndCheck('t', AddCharStatus.NewGrapheme, "т");
            AddCharAndCheck(' ', AddCharStatus.NoGraphemeFound, null);
            AddCharAndCheck('v', AddCharStatus.NewGrapheme, "в");
            AddCharAndCheck(' ', AddCharStatus.NoGraphemeFound, null);
            AddCharAndCheck('D', AddCharStatus.NewGrapheme, "Д");
            AddCharAndCheck('e', AddCharStatus.NewGrapheme, "е");
            AddCharAndCheck('l', AddCharStatus.NewGrapheme, "л");
            AddCharAndCheck('\'', AddCharStatus.NewGrapheme, "ь");
            AddCharAndCheck('f', AddCharStatus.NewGrapheme, "ф");
            AddCharAndCheck('y', AddCharStatus.NewGrapheme, "ы");
            AddCharAndCheck('.', AddCharStatus.NoGraphemeFound, null);
        }

        [Test]
        public void Reset_Works()
        {
            AddCharAndCheck('s', AddCharStatus.NewGrapheme, "с");
            _t.Reset();
            AddCharAndCheck('h', AddCharStatus.NewGrapheme, "х");
        }

        private void AddCharAndCheck(char c, AddCharStatus expectedStatus, string expectedGrapheme)
        {
            AddCharResult res = _t.AddChar(c);
            Assert.That(res.Status, Is.EqualTo(expectedStatus));
            if (expectedGrapheme == null)
            {
                Assert.That(res.Grapheme, Is.Null);
            }
            else
            {
                Assert.That(res.Grapheme, Is.EqualTo(expectedGrapheme));
            }
        }
    }
}