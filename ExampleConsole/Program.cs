using System;
using ABCNet;
using System.Collections.Generic;

namespace ExampleConsole
{
    class Program
    {
		private static Random random = new Random(Guid.NewGuid().GetHashCode());

        public static double FitnessCheck(FoodSource foodSource, Bee bee) {
            var geo = foodSource.Location.GeoCoordinate;
            //Console.WriteLine(string.Format("{0:0.##}, {1:0.##}", geo.Latitude, geo.Longitude));
			return random.NextDouble();
        }

        static void Main(string[] args)
        {
            List<FoodSource> foodSources = new List<FoodSource>();
            for (int i=0; i < 10; i++) {
				foodSources.Add(new FoodSource(FoodSourceLocation.GenerateRandom(random)));
            }
			for (int i = 0; i < 50; i++)
			{
				Colony colony = new Colony(100, foodSources, FitnessCheck);

				var fittestSources = colony.Run();
				fittestSources.ForEach(x =>
				{
					Console.WriteLine(string.Format("{0:0.000} => {1}", x.FitnessValue, x.ToString()));
				});
				Console.WriteLine("==============================");
			}
            //Console.ForegroundColor = ConsoleColor.Yellow;
            //Console.WriteLine("Press enter to exit.");
            // Console.ReadLine();
            return;
        }
    }
}
