using System;
using ABCNet;
using System.Collections.Generic;

namespace ExampleConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rand = new Random(Guid.NewGuid().GetHashCode());

            List<FoodSource> foodSources = new List<FoodSource>();
            for (int i=0; i < 10; i++) {
                foodSources.Add(new FoodSource(FoodSourceLocation.GenerateRandom(rand)));
            }

            Colony colony = new Colony(20, foodSources, null);

            return;
        }
    }
}
