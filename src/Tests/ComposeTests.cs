using NUnit.Framework;
using System.Collections.Generic;
using Transducers.Net;
using static Transducers.Net.Transducers;

namespace Tests
{
    [TestFixture]
    class ComposeTests
    {
        [Test]
        public void DoubleThenIncrementTest()
        {
            var transducer = Select<int, int>(x => x * 2).Compose(Select<int, int>(x => x + 1));
            var input = new List<int> {3, 5};
            var output = input.Transduce(transducer, Utils.Appender, new List<int>());
            var expected = new List<int> {7, 11};
            Assert.That(output, Is.EqualTo(expected));
        }
    }
}
