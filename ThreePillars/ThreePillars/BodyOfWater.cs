using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreePillars
{
    public abstract class BodyOfWater
    {
        private double _width;
        private double _length;
        private double _depth;

        public BodyOfWater(double width, double length, double depth)
        {
            Width = width;
            Length = length;
            Depth = depth;
        }
        
        public bool IsWater { get { return true; }}
        public virtual bool IsFresh { get; set; }
        public double EstimatedVolume { get; protected set; }
        public double Width
        {
            get { return _width; }
            set
            {
                EstimatedVolume = EstimateVolume(value, Length, Depth);
                _width = value;
            }
        }
        public double Length
        {
            get { return _length; }
            set
            {
                EstimatedVolume = EstimateVolume(Width, value, Depth);
                _length = value;
            }
        }
        public double Depth
        {
            get { return _depth; }
            set
            {
                EstimatedVolume = EstimateVolume(Width, Length, value);
                _depth = value;
            }
        }

        /// <summary>
        /// Creates an estimated average volume for a body of water by caluculating an estimated
        /// volume of an ellipsoid and taking half of that.
        /// Not super accurate, but gives us something to work with!
        /// </summary>
        /// <param name="width">X-dimension of the Body of Water</param>
        /// <param name="length">Y-dimension of the Body of Water</param>
        /// <param name="depth">Z-dimension of the Body of Water</param>
        /// <returns>An estimated volume as a double.</returns>
        public virtual double EstimateVolume(double width, double length, double depth)
        {
            // Our formula is 4/3 * pi * r^3, so our constants are:
            double constants = (Math.PI * 4) / 3;
            // Then our radii are our half of our width and length as they are currently diameters,
            // times our depth which is already a radius.
            double radii = (width / 2) * (length / 2) * depth;
            // Bring it all together:
            double ellipsoidVolume = constants * radii;
            // Remove the projected half that would be above the water:
            return ellipsoidVolume/2;
        }

    }
}
