using NUnit.Framework;

namespace JoZhTranslit.Tests
{
    [TestFixture]
    public class CharArrayTests
    {
        [Test]
        public void GetMutableHashCode_Works()
        {
            var ca1 = new CharArray(2);
            ca1.Append('a');
            ca1.Append('b');
          
            var ca2 = new CharArray(2);
            ca2.Append('b');
            ca2.Append('a');

            Assert.That(ca1.GetMutableHashCode(), Is.Not.EqualTo(ca2.GetMutableHashCode()));

            var ca3 = new CharArray(3);
            ca3.Append('a');
            ca3.Append('b');
            ca3.Append('c');

            Assert.That(ca2.GetMutableHashCode(), Is.Not.EqualTo(ca3.GetMutableHashCode()));
            
            var ca4 = new CharArray(3);
            ca4.Append('a');
            ca4.Append('b');
            ca4.Append('c');

            Assert.That(ca3.GetMutableHashCode(), Is.EqualTo(ca4.GetMutableHashCode()));
            
        }
    }
}