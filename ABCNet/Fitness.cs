namespace ABCNet
{
	public class Fitness
	{
		/// <summary>
		/// A delegate method used for custom handling the fitness algorithm.
		/// </summary>
		/// <param name="foodSource">The food source to test</param>
		/// <param name="bee">The bee performing the test</param>
		/// <returns>Returns a value > 0 to denote fitness of a particular food source.  
		/// The larger the fitness value, the better the food source</returns>
		public delegate double Get(FoodSource foodSource, Bee bee);
	}
}
