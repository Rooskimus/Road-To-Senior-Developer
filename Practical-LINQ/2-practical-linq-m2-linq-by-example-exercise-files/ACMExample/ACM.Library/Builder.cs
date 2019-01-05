using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACM.Library
{
    public class Builder
    {
        public IEnumerable<int> BuildIntegerSequence()
        {
            var integers = Enumerable.Range(0, 10)
                            .Select(i=> 5+(10*i));
            return integers;
            //Range works only with integers.
        }

        public IEnumerable<int> BuildRepeatedIntegerSequence()
        {
            var integers = Enumerable.Repeat(-1, 10);
            return integers;
            //Repeat can work with ANY object, not just numbers.  See strings below.
        }

        public IEnumerable<string> BuildStringSequence()
        {
            var strings = Enumerable.Range(0, 10)
                            .Select(i => ((char)('A' + i)).ToString());
            return strings;
        }

        public IEnumerable<string> BuildRandomStringSequence()
        {
            Random rand = new Random();
            var strings = Enumerable.Range(0, 10)
                            .Select(i => ((char)('A' + rand.Next(0,26))).ToString());
            return strings;
        }

        public IEnumerable<string> BuildRepeatedStringSequence()
        {
            var strings = Enumerable.Repeat("A", 10);
            return strings;
        }
    }
}
