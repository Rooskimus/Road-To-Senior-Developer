using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreePillars
{
    class Program
    {
        static void Main(string[] args)
        {
            // This program was written as a way to demonstrate the Three Pillars of Object
            // Oriented Programing.  I could have chosen a simpler example, but I would have just been
            // mimicking those I'd seen before, so I chose bodies of water instead.
            // I created a base abstract class called BodyOfWater and gave it some properties: width, length,
            // depth, estimated volume, and IsFresh.  It also has a method to estimate the volume.
            //
            // This brings us to our first Pillar: Inheritance.  There isn't any body of water which is
            // simply a body of water, they have names like lakes, rivers, oceans, puddles, etc.  So
            // the BodyOfWater class serves as the basis for these other types in an "is a" relationship.
            // Meaning, a lake "is a" body of water.  So all of the class members of BodyOfWater are
            // recieved by the class Lake.  The same for River and Sea.  Let's make a Lake and view
            // its properties to verify this. Notice the Lake class is nearly empty, only containing a 
            // constructor (which is inherited from the base constructor) and an overload for the
            // constructor for the rare case of a salt lake.

            // Max width and length, average depth taken from Wikipedia:
            Lake Titicaca = new Lake(190, 80, .107);
            Console.WriteLine($"The estimated volume of Lake Titicaca is {Titicaca.EstimatedVolume, 0:0.00} km^3. " +
                            $"\n The real volume according to Wikipedia is 893 km^3.");

            Console.ReadLine();

            // So you can see we can access EstimatedVolume even though it isn't defined in the Lake class itself.
            //
            // The next Pillar is Encapsulation.  Encapsulation is controlling what parts of the program will
            // have access to a class or its individual members.  If you look at the BodyOfWater class, you'll
            // see that the EstimatedVolume property is has a protected set.  This means that any class outside
            // of that class or any class that inherits it cannot set the EstimatedVolume property. The following
            //code gives an error (uncomment to view):

            // Titicaca.EstimatedVolume = 500;

            // This is because in the Main method here we are not inside a class that inherits (or is) BodyOfWater.
            // We have effectively encapsulated this property.  On a more technical note, it's designed to update
            // any time a dimension of the body of water is altered, so we can manipulate it via the dimensions.

            Titicaca.Depth = 1;

            Console.WriteLine($"The estimated volume of Lake Titicaca if it were 1 km deep is {Titicaca.EstimatedVolume,0:0.00} km^3. ");
            Console.ReadLine();

            // So in this example, encapsulation is used to maintain data consistency, as volume relies on the other
            // dimensions and not the other way around.
            //
            // The final pillar is polymorphism.  This just means that one object or datatype can be used in a number
            // of different ways.  For example, let's say I want to find out how long it would take you to drink
            // whatever body of water was input.  We already have a pretty good estimation of Lake Titicaca, let's fix our
            // depth:

            Titicaca.Depth = .107;

            // Now let's approximate the Amazon River's volume, using some very rough averages I found (rivers are living beasts):

            River Amazon = new River(24, 6400, .045);
            Console.WriteLine($"The Amazon's estimated volume is {Amazon.EstimatedVolume} km^3. \n");


            // We'll make a method below Main that accepts BodyOfWater types and gives a how-long-to-drink answer.
            // And now we'll call it here:

            decimal drinkTiticaca = HowLongToDrinkIt(Titicaca);
            decimal drinkAmazon = HowLongToDrinkIt(Amazon);

            Console.WriteLine($"It would take {drinkTiticaca, 0:0.00} millenia to drink Lake Titicaca.");
            Console.WriteLine($"It would take {drinkAmazon, 0:0.00} millenia to drink the Amazon river.");

            Console.ReadLine();

            // So you can see, even though Titicaca is type Lake and Amazon is type River, they can both still be used
            // as type BodyOfWater due to polymorphism leant by the mechanism of inheritance.  Note that the biggest
            // difference between the river and the lake is that I used a cylinder for the volume of a river, and an
            // ellipse for the volume of a lake, which means that they have different volume methods but are still
            // able to be used the same way.
            //
            // And there you have it, those are the Three Pillars in a nutshell!

        }

        public static decimal HowLongToDrinkIt(BodyOfWater body)
        {
            // Let's assume it takes 30 seconds to chug 500 ml (about a 12 oz can).  I've been using km^3 for our volumes.
            // so 1 minute = 1000 ml; 1 hour = 60,0000 ml; 1 day = 1,440,000 ml; 1 year (at 365.25 days) is 525,960,000 ml.
            // and one milennia is 525,960,000,000.  That's finally a number we can work with, 0.00052596 km^3.
            decimal drinkRate = 0.00052596m;
            decimal timeTaken = (decimal)body.EstimatedVolume / drinkRate;
            // Remember, our unit is Millenia.
            return timeTaken;
        }
    }
}
