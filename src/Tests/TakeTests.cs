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
            var transducer = Take<int>(4);
            var input = Enumerable.Empty<int>();
            var output = input.Transduce(transducer, Utils.Appender, new List<int>());
            Assert.That(0, Is.EqualTo(output.Count));
        }

        [Test]
        public void TakeNoneTest()
        {
            var transducer = Take<int>(0);
            var input = new List<int> { 3, 5 };
            var output = input.Transduce(transducer, Utils.Appender, new List<int>());
            Assert.That(0, Is.EqualTo(output.Count));
        }

        [Test]
        public void TakeTooManyTest()
        {
            var transducer = Take<int>(4);
            var input = new List<int> { 3, 5 };
            var output = input.Transduce(transducer, Utils.Appender, new List<int>());
            Assert.That(input , Is.EqualTo(output));
        }

        [Test]
        public void TakeAllTest()
        {
            var transducer = Take<int>(2);
            var input = new List<int> { 3, 5 };
            var output = input.Transduce(transducer, Utils.Appender, new List<int>());
            Assert.That(input, Is.EqualTo(output));
        }

        [Test]
        public void TakeSomeTest()
        {
            var transducer = Take<bool>(3);
            var input = new List<bool> {true, true, false, true, false};
            var output = input.Transduce(transducer, Utils.Appender, new List<bool>());
            var expected = new List<bool> {true, true, false};
            Assert.That(expected, Is.EqualTo(output));
        }

        [Test]
        public void TakeWhileEmptyListTest()
        {
            var transducer = TakeWhile<int>(n => n == 0);
            var input = Enumerable.Empty<int>();
            var output = input.Transduce(transducer, Utils.Appender, new List<int>());
            Assert.That(0, Is.EqualTo(output.Count));
        }

        [Test]
        public void TakeWhileAllTest()
        {
            var transducer = TakeWhile<bool>(b => b);
            var input = new List<bool> { true, true, true, true };
            var output = input.Transduce(transducer, Utils.Appender, new List<bool>());
            var expected = new List<bool> { true, true, true, true };
            Assert.That(expected, Is.EqualTo(output));
        }

        [Test]
        public void TakeWhileSomeTest()
        {
            var transducer = TakeWhile<bool>(b => b);
            var input = new List<bool> { true, true, false, true, false };
            var output = input.Transduce(transducer, Utils.Appender, new List<bool>());
            var expected = new List<bool> { true, true };
            Assert.That(expected, Is.EqualTo(output));
        }

        [Test]
        public void TakeTwiceTest()
        {
            var transducer = Take<int>(3);
            var input1 = new List<int> {1, 2, 3, 4, 5};
            var output1 = input1.Transduce(transducer, Utils.Appender, new List<int>());
            var expected1 = new List<int> {1, 2, 3};
            var input2 = new List<int> { 11, 12, 13, 14, 15 };
            var output2 = input2.Transduce(transducer, Utils.Appender, new List<int>());
            var expected2 = new List<int> {11, 12, 13};
            Assert.That(output1, Is.EqualTo(expected1));
            Assert.That(output2, Is.EqualTo(expected2));
        }
    }
}
