using GeoCoordinatePortable;

namespace ABCNet
{
    public class FoodSourceLocation
    {
        public GeoCoordinate GeoCoordinate { get; set; }
        public double X { get; set; }
        public double Y { get; set; }

    }

    public abstract class FoodSource
    {
        public FoodSource(FoodSourceLocation location) {
            Location = location;
        }
        public FoodSourceLocation Location { get; set; }
        public int TrialsCount { get; set; }
        public double FitnessValue { get; set; }
    }
}