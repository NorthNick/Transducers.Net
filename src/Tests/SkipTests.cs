using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Transducers.Net;
using static Transducers.Net.Transducers;

namespace Tests
{
    [TestFixture]
    public class SkipTests
    {
        [Test]
        public void SkipEmptyListTest()
        {
            var transducer = Skip<bool>(2);
            var input = Enumerable.Empty<bool>();
            var output = input.Transduce(transducer, Utils.Appender, new List<bool>());
            Assert.That(0, Is.EqualTo(output.Count));
        }

        [Test]
        public void SkipNoneTest()
        {
            var transducer = Skip<int>(0);
            var input = new List<int> { 3, 5 };
            var output = input.Transduce(transducer, Utils.Appender, new List<int>());
            var expected = input;
            Assert.That(output, Is.EqualTo(expected));
        }

        [Test]
        public void SkipAllTest()
        {
            var transducer = Skip<int>(4);
            var input = new List<int> {4, 2, 45, 6};
            var output = input.Transduce(transducer, Utils.Appender, new List<int>());
            Assert.That(0, Is.EqualTo(output.Count));
        }

        [Test]
        public void SkipSomeTest()
        {
            var transducer = Skip<int>(2);
            var input = new List<int> { 4, 2, 45, 6 };
            var output = input.Transduce(transducer, Utils.Appender, new List<int>());
            var expected = new[] {45, 6};
            Assert.That(output, Is.EqualTo(expected));
        }

        [Test]
        public void SkipWhileSomeTest()
        {
            var transducer = SkipWhile<int>(x => x < 4);
            var input = new List<int> {-3, 2, 4, 5};
            var output = input.Transduce(transducer, Utils.Appender, new List<int>());
            var expected = new[] { 4, 5 };
            Assert.That(output, Is.EqualTo(expected));
        }

        [Test]
        public void SkipTwiceTest()
        {
            var transducer = Skip<string>(2).Then(Skip<string>(1));
            var input = new List<string> {"a", "b", "", "d", "e"};
            var output = input.Transduce(transducer, Utils.Appender, new List<string>());
            var expected = new[] { "d", "e" };
            Assert.That(output, Is.EqualTo(expected));
        }
    }
}
