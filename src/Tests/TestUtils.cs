using System.Collections.Generic;

namespace Tests
{
    static class TestUtils
    {
        public static IEnumerable<int> UpTo(int n)
        {
            int count = 0;
            while (count <= n) {
                yield return count;
                count++;
            }
        }
    }
}
