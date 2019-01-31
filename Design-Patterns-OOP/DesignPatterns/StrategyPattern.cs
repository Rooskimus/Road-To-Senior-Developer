using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns
{
    // Encapsulates family of algorithms so they may be used interchangeably.
    // To demonstrate, we're making a simple game.

    public interface IUnitBehavior
    {
        void ReactToOpponent();
    }

    public class Unit
    {
        private string name;
        private IUnitBehavior behavior;
        public Unit (string name, IUnitBehavior behavior)
        {
            this.behavior = behavior;
            this.name = name;
        }

        public void Render()
        {
            Console.WriteLine(@"\o/");
            Console.WriteLine(@" O ");
            Console.WriteLine(@"/ \");
        }

        public void Move()
        {
            Console.WriteLine($"{name} moves...");
        }

        public void ReactToOpponent()
        {
            behavior.ReactToOpponent();
        }
        // Note that Render and Move are always the same, but
        // ReactToOpponent is injected through the constructor.
    }

    // Now let's define some strategies
    public class WarriorBehavior : IUnitBehavior
    {
        public void ReactToOpponent()
        {
            Console.WriteLine("\"ATTACK!!!\"");
        }
    }

    public class DefenderBehavior : IUnitBehavior
    {
        public void ReactToOpponent()
        {
            Console.WriteLine("\"Hold the line!\"");
        }
    }

    public class Game
    {
        public void RunGame()
        {
            Unit warrior = new Unit("Warrior", new WarriorBehavior());
            Unit defender = new Unit("Defender", new DefenderBehavior());
            // It's easy to add new "strategies" here by creating a new class
            // that implements IUnitBehavior, initializing above and adding to 
            // the list below.

            List<Unit> units = new List<Unit>();
            units.Add(warrior);
            units.Add(defender);
            foreach (Unit unit in units)
            {
                unit.Render();
                Console.WriteLine();
            }

            Console.WriteLine("Your troops are on an important mission!");
            Console.WriteLine("Press Escape to quite or any other key to continue.");
            Console.WriteLine();
            Random rnd = new Random();
            
            while (Console.ReadKey().Key != ConsoleKey.Escape)
            {
                foreach (Unit unit in units)
                {
                    unit.Move();
                }
                Console.WriteLine();
                
                // Returns 0, 1, or 2.
                if (rnd.Next(3) == 0)
                {
                    Console.WriteLine("The enemy attacks!");
                    foreach (Unit unit in units)
                    {
                        unit.ReactToOpponent();
                    }
                }
                else Console.WriteLine("Nothing happened...");
                Console.WriteLine();

            }
        }
    }
}
