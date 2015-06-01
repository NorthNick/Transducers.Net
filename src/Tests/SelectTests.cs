using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Transducers.Net;
using static Transducers.Net.Transducers;

namespace Tests
{
    [TestFixture]
    public class SelectTests
    {
        [Test]
        public void EmptyListTest()
        {
            var transducer = Select<List<bool>, bool, bool>(b => !b);
            var input = Enumerable.Empty<bool>();
            var output = input.Transduce(transducer, Utils.Appender, new List<bool>());
            Assert.That(0, Is.EqualTo(output.Count));
        }

        [Test]
        public void IntBoolTest()
        {
            var transducer = Select<List<bool>, int, bool>(n => n > 12);
            var input = new List<int> {93, 12, 0, 17, 4, 5, -6};
            var output = input.Transduce(transducer, Utils.Appender, new List<bool>());
            var expected = new List<bool> {true, false, false, true, false, false, false};
            Assert.That(expected, Is.EqualTo(output));
        }

        [Test]
        public void StringStringTest()
        {
            var transducer = Select<List<string>, string, string>(s => s + " world");
            var input = new List<string> {"hello", "small", "", "whole wide"};
            var output = input.Transduce(transducer, Utils.Appender, new List<string>());
            var expected = new List<string> { "hello world", "small world", " world", "whole wide world" };
            Assert.That(expected, Is.EqualTo(output));
        }
    }
}
