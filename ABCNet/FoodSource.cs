using System;
using GeoCoordinatePortable;
using System.Collections.Generic;

namespace ABCNet
{
    public class FoodSourceLocation
    {
        public GeoCoordinate GeoCoordinate { get; set; }

        public static FoodSourceLocation GenerateWithValues(double latitude, double longitude) {
            var fs = new FoodSourceLocation();
            fs.GeoCoordinate = new GeoCoordinate(latitude, longitude);
            return fs;
        }

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
        public bool IsAbandoned { get; set; }

		private string uniqueName = Guid.NewGuid().ToString("N");

		public override string ToString()
		{
			return uniqueName;
		}
    }

    public class FoodSourceComparer : IComparer<FoodSource> {
        public int Compare(FoodSource x, FoodSource y) {
            if (x.FitnessValue < y.FitnessValue) return 1; //y is greater
            else if (x.FitnessValue > y.FitnessValue) return -1; //x is greater
            else return 0; //equal
        }
    }
}