using System;
using GeoCoordinatePortable;

namespace ABCNet
{
    public class FoodSourceLocation
    {
        public GeoCoordinate GeoCoordinate { get; set; }

        public static FoodSourceLocation GenerateRandom(Random rand) {
            var fs = new FoodSourceLocation();
            int lat = rand.Next(516400146, 630304598);
            int lon = rand.Next(224464416, 341194152);
            fs.GeoCoordinate = new GeoCoordinate(18d + lat / 1000000000d, -72d - lon / 1000000000d);
            return fs;
        }
    }

    public class FoodSource
    {
        public FoodSource(FoodSourceLocation location) {
            Location = location;
        }
        public FoodSourceLocation Location { get; set; }
        public int TrialsCount { get; set; }
        public double FitnessValue { get; set; }
    }
}