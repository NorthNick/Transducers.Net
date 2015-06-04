using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Transducers.Net;
using static Transducers.Net.Transducers;

namespace Tests
{
    [TestFixture]
    class EnumerableTests
    {
        [Test]
        public void TakeTest()
        {
            var transducer = Take<int>(3);
            var input = TestUtils.UpTo(5);
            var output = input.Transduce(transducer);
            var expected = TestUtils.UpTo(2);
            Assert.That(expected, Is.EqualTo(output));
        }

        [Test]
        public void LazyTakeTest()
        {
            var transducer = Take<int>(20);
            var input = AllInts();
            var output = input.Transduce(transducer);
            var expected = TestUtils.UpTo(19);
            Assert.That(expected, Is.EqualTo(output));
        }

        [Test]
        public void LazyComposeTest()
        {
            var transducer = Select<int, int>(x => x * 2).Compose(Take<int>(10));
            var input = AllInts();
            var output = input.Transduce(transducer);
            var expected = TestUtils.UpTo(9).Select(x => x * 2);
            Assert.That(expected, Is.EqualTo(output));
        }

        private static IEnumerable<int> AllInts()
        {
            int counter = 0;
            while (true) {
                yield return counter++;
            }
            // ReSharper disable once FunctionNeverReturns
        }
    }
}
