using System.Text;
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
        public void GetCharCollectionHashCode_WorksForStringBuilder()
        {
            var hash1 = HashHelper.GetHashCodeAsCharArray(new StringBuilder("ab"));
            var hash2 = HashHelper.GetHashCodeAsCharArray(new StringBuilder("ba"));
            var hash3 = HashHelper.GetHashCodeAsCharArray(new StringBuilder("abc"));

            Assert.That(hash1, Is.Not.EqualTo(hash2));
            Assert.That(hash2, Is.Not.EqualTo(hash3));
        }
    }
}