using NUnit.Framework;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using static Transducers.Net.Reactive.Transducers;
using static Transducers.Net.Transducers;


namespace Tests
{
    [TestFixture]
    public class ObservableTests
    {
        [Test]
        public void TakeTest()
        {
            var transducer = Take<int>(5);
            var input = Observable.Range(1, 10);
            var output = input.Transduce(transducer);
            var expected = Enumerable.Range(1, 5);
            Assert.That(output.ToEnumerable(), Is.EqualTo(expected));
        }

        [Test]
        public void OverTakeTest()
        {
            var transducer = Take<int>(15);
            var input = Observable.Range(1, 10);
            var output = input.Transduce(transducer);
            var expected = Enumerable.Range(1, 10);
            Assert.That(output.ToEnumerable(), Is.EqualTo(expected));
        }

        [Test]
        public void WhereTest()
        {
            var transducer = Where<int>(x => x > 0);
            var input = Observable.Range(-3, 7);
            var output = input.Transduce(transducer);
            var expected = Enumerable.Range(1, 3);
            Assert.That(output.ToEnumerable(), Is.EqualTo(expected));
        }

        [Test]
        public void SelectTest()
        {
            var transducer = Select<int, bool>(x => x%2 == 0);
            var input = Observable.Range(1, 10);
            var output = input.Transduce(transducer);
            var expected = new[] {false, true, false, true, false, true, false, true, false, true};
            Assert.That(output.ToEnumerable(), Is.EqualTo(expected));
        }

        [Test]
        public void DoubleTakeTest()
        {
            var transducer = Take<int>(9).Then(Take<int>(5));
            var input = Observable.Range(1, 10);
            var output = input.Transduce(transducer);
            var expected = Enumerable.Range(1, 5);
            Assert.That(output.ToEnumerable(), Is.EqualTo(expected));
        }

        [Test]
        public void TakeTwiceTest()
        {
            var transducer = Take<int>(3);
            var input1 = Observable.Range(1, 10);
            var output1 = input1.Transduce(transducer);
            var expected1 = new[] { 1, 2, 3 };
            var input2 = Observable.Range(11, 5);
            var output2 = input2.Transduce(transducer);
            var expected2 = new[] { 11, 12, 13 };
            Assert.That(output1.ToEnumerable(), Is.EqualTo(expected1));
            Assert.That(output2.ToEnumerable(), Is.EqualTo(expected2));
        }

        [Test]
        public void ComposeTest()
        {
            var transducer = Select<int, int>(x => x + 3).Then(Where<int>(x => x%2 == 1)).Then(Take<int>(4));
            var input = Observable.Range(1, 5);
            var output = input.Transduce(transducer);
            var expected = new[] {5, 7};
            Assert.That(output.ToEnumerable(), Is.EqualTo(expected));
        }

        [Test]
        public void SumTest()
        {
            var transducer = Where<int>(x => true);
            var input = Observable.Range(1, 6);
            var output = input.Transduce(transducer, (x, y) => x + y, 0);
            var expected = new[] {21};
            Assert.That(output.ToEnumerable(), Is.EqualTo(expected));
        }

        [Test]
        public void SumTakeTest()
        {
            var transducer = Take<int>(4);
            var input = Observable.Range(1, 6);
            var output = input.Transduce(transducer, (x, y) => x + y, 0);
            var expected = new[] { 10 };
            Assert.That(output.ToEnumerable(), Is.EqualTo(expected));
        }

        [Test]
        public void SumOverTakeTest()
        {
            var transducer = Take<int>(10);
            var input = Observable.Range(1, 6);
            var output = input.Transduce(transducer, (x, y) => x + y, 0);
            var expected = new[] { 21 };
            Assert.That(output.ToEnumerable(), Is.EqualTo(expected));
        }

        [Test]
        public void SumComposeTest()
        {
            var transducer = Select<int, int>(x => x + 1).Then(Where<int>(x => x < 4));
            var input = Observable.Range(1, 6);
            var output = input.Transduce(transducer, (x, y) => x + y, 0);
            var expected = new[] { 5 };
            Assert.That(output.ToEnumerable(), Is.EqualTo(expected));
        }
    }
}
