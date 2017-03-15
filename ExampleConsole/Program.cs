using System;
using ABCNet;
using System.Collections.Generic;

namespace ExampleConsole
{
    class Program
    {
        public double FitnessCheck(FoodSource foodSource, Bee bee) {
            var geo = foodSource.Location.GeoCoordinate;
            Console.WriteLine(string.Format("{0:0.##}, {1:0.##}", geo.Latitude, geo.Longitude));
            return 0d;
        }
        static void Main(string[] args)
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());

            List<FoodSource> foodSources = new List<FoodSource>();
            for (int i=0; i < 10; i++) {
                foodSources.Add(new FoodSource(FoodSourceLocation.GenerateRandom(rand)));
            }

            Program p = new Program();

            Colony colony = new Colony(20, foodSources, p.FitnessCheck);

            var fittestSources = colony.Run();
            fittestSources.ForEach(x => {
                Console.WriteLine(x.FitnessValue);
            });

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Press enter to exit.");
            // Console.ReadLine();
            return;
        }
    }
}
