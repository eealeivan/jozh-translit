using NUnit.Framework;

namespace JoZhTranslit.Tests
{
    [TestFixture]
    public class HashHelperTests
    {
        [Test]
        public void GetCharCollectionHashCode_WorksForString()
        {
            var hash1 = HashHelper.GetHashCodeAsCharArray("ab");
            var hash2 = HashHelper.GetHashCodeAsCharArray("ba");
            var hash3 = HashHelper.GetHashCodeAsCharArray("abc");

            Assert.That(hash1, Is.Not.EqualTo(hash2));
            Assert.That(hash2, Is.Not.EqualTo(hash3));
        }

        [Test]
        public void GetCharCollectionHashCode_WorksForCharArray()
        {
            var ca1 = new CharArray(2);
            ca1.Append('a');
            ca1.Append('b');
            var hash1 = HashHelper.GetHashCodeAsCharArray(ca1);

            var ca2 = new CharArray(2);
            ca2.Append('b');
            ca2.Append('a');
            var hash2 = HashHelper.GetHashCodeAsCharArray(ca2);

            var ca3 = new CharArray(3);
            ca3.Append('a');
            ca3.Append('b');
            ca3.Append('c');
            var hash3 = HashHelper.GetHashCodeAsCharArray(ca3);

            Assert.That(hash1, Is.Not.EqualTo(hash2));
            Assert.That(hash2, Is.Not.EqualTo(hash3));
        }
    }
}