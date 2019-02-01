using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreePillars
{
    public class River : BodyOfWater
    {
        public River(double width, double length, double depth) : base(width, length, depth)
        {
        }
        public override bool IsFresh { get { return true; } }
       

        /// <summary>
        /// For a river, let's use a cylinder to approximate the volume instead since it's long
        /// and the bottom is curved.
        /// </summary>
        /// <param name="width">s</param>
        /// <param name="length"></param>
        /// <param name="depth"></param>
        /// <returns></returns>
        public override double EstimateVolume(double width, double length, double depth)
        {
            // The volume of a cylinder is pi * r^2 * length.  Our r^2 will be half of our width
            // times our depth (depth is already representing a radius rather than a diameter).  This
            // is essentially a vertical cross-section of the river.
            double crossSectionArea = Math.PI * (width / 2) * depth;
            // Now simply multiply by length to have the volume of a cylinder:
            double cylinderVolume = crossSectionArea * length;
            // But here we need to cut it in half because we've projected a cylinder that goes above
            // the water as far as it goes below:
            return cylinderVolume / 2;            
        }
    }
}
