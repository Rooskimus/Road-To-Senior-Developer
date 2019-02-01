using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreePillars
{
    public class Sea : BodyOfWater
    {
        public Sea(double width, double length, double depth) : base(width, length, depth)
        {
        }

        public override bool IsFresh { get { return false; } }

    }
}
