using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreePillars
{
    public class Lake : BodyOfWater
    {
        public Lake(double width, double length, double depth) : base(width, length, depth)
        {
            IsFresh = true;
        }
        public Lake(double width, double length, double depth, bool isFresh) : base(width, length, depth)
        {
            IsFresh = isFresh;
        }
    }
}
