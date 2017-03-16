# ABCNet  [![Build Status](https://travis-ci.org/danielsonchris/ABCNet.svg?branch=master)](https://travis-ci.org/danielsonchris/ABCNet)
An extensible Artificial Bee Colony .Net Library.  Initial usage was for a distributed clustering solution.  We effectively required this algorithm for spreading processes across a multitude of servers that may or may not have been available.

A parent process dynamically acted as the hive entity and from that vantage point healthy foraging resources were selected for processing long running scheduled items or event driven tasks.

[Available from Nuget](https://www.nuget.org/packages/ABCNet)

# Simple Example
```csharp
using ABCNet;

public static double FitnessCheck(FoodSource foodSource, Bee bee) {
    //TODO return something meaningful
    return 1d;
}
//snip...
var random = new Random();
List<FoodSource> foodSources = new List<FoodSource>();
for (int i=0; i < 10; i++)
    foodSources.Add(new FoodSource(FoodSourceLocation.GenerateRandom(random)));
Colony colony = new Colony(100, foodSources, FitnessCheck);
var fittestSources = colony.Run();
```

# More Complete Example Usage
```csharp
using System;
using System.Collections.Generic;
using ABCNet;

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
        return;
    }
}
```

# Troubleshooting Notes
* If using dotnet core, make sure to apply a "dotnet restore" in order to reload the nuget dependencies.

