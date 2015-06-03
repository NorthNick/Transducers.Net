using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Transducers.Net;
using static Transducers.Net.Transducers;

namespace Tests
{
    [TestFixture]
    class FilterTests
    {
        [Test]
        public void EvensTest()
        {
            var transducer = Where<int>(x => x % 2 == 0);
            var input = Enumerable.Range(0, 6).ToList();
            var output = input.Transduce(transducer, Utils.Appender, new List<int>());
            var expected = new List<int> {0, 2, 4};
            Assert.That(output, Is.EqualTo(expected));
        }
    }
}
