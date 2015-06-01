using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Transducers.Net;
using static Transducers.Net.Transducers;

namespace Tests
{
    [TestFixture]
    public class TakeTests
    {
        [Test]
        public void TakeEmptyListTest()
        {
            var transducer = Take<List<int>, int>(4);
            var input = Enumerable.Empty<int>();
            var output = input.Transduce(transducer, Utils.Appender, new List<int>());
            Assert.That(0, Is.EqualTo(output.Count));
        }

        [Test]
        public void TakeNoneTest()
        {
            var transducer = Take<List<int>, int>(0);
            var input = new List<int> { 3, 5 };
            var output = input.Transduce(transducer, Utils.Appender, new List<int>());
            Assert.That(0, Is.EqualTo(output.Count));
        }

        [Test]
        public void TakeTooManyTest()
        {
            var transducer = Take<List<int>, int>(4);
            var input = new List<int> { 3, 5 };
            var output = input.Transduce(transducer, Utils.Appender, new List<int>());
            Assert.That(input , Is.EqualTo(output));
        }

        [Test]
        public void TakeAllTest()
        {
            var transducer = Take<List<int>, int>(2);
            var input = new List<int> { 3, 5 };
            var output = input.Transduce(transducer, Utils.Appender, new List<int>());
            Assert.That(input, Is.EqualTo(output));
        }

        [Test]
        public void TakeSomeTest()
        {
            var transducer = Take<List<bool>, bool>(3);
            var input = new List<bool> {true, true, false, true, false};
            var output = input.Transduce(transducer, Utils.Appender, new List<bool>());
            var expected = new List<bool> {true, true, false};
            Assert.That(expected, Is.EqualTo(output));
        }

        [Test]
        public void TakeWhileEmptyListTest()
        {
            var transducer = TakeWhile<List<int>, int>(n => n == 0);
            var input = Enumerable.Empty<int>();
            var output = input.Transduce(transducer, Utils.Appender, new List<int>());
            Assert.That(0, Is.EqualTo(output.Count));
        }

        [Test]
        public void TakeWhileAllTest()
        {
            var transducer = TakeWhile<List<bool>, bool>(b => b);
            var input = new List<bool> { true, true, true, true };
            var output = input.Transduce(transducer, Utils.Appender, new List<bool>());
            var expected = new List<bool> { true, true, true, true };
            Assert.That(expected, Is.EqualTo(output));
        }

        [Test]
        public void TakeWhileSomeTest()
        {
            var transducer = TakeWhile<List<bool>, bool>(b => b);
            var input = new List<bool> { true, true, false, true, false };
            var output = input.Transduce(transducer, Utils.Appender, new List<bool>());
            var expected = new List<bool> { true, true };
            Assert.That(expected, Is.EqualTo(output));
        }

    }
}
