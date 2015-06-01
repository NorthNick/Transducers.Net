using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using NUnit.Framework;
using static Transducers.Net.Transducers;

namespace Tests
{
    [TestFixture]
    public class PerformanceTests
    {
        [Test]
        public void SelectWhereTakeTest()
        {
            var transducer = Compose(Compose(Select<int, int, int>(x => x + 1), Where<int, int>(x => x % 2 == 0)), Take<int, int>(100000));
            var input = UpTo(100000);
            var trSw = Stopwatch.StartNew();
            var trOutput = input.Transduce(transducer, (x, y) => x + y, 0);
            trSw.Stop();
            var trTime = trSw.ElapsedMilliseconds;
            trSw = Stopwatch.StartNew();
            var linqOutput = input.Select(x => x + 1).Where(x => x%2 == 0).Take(100000).Aggregate(0, (x, y) => x + y);
            trSw.Stop();
            var linqTime = trSw.ElapsedMilliseconds;
            Assert.AreEqual(trOutput, linqOutput);
        }

        private IEnumerable<int> UpTo(int n)
        {
            int count = 0;
            while (count <= n) {
                yield return count;
                count++;
            }
        }
    }
}
