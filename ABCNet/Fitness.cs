
namespace ABCNet
{
	public class Fitness
	{
		/**
		 * Used for creating a custom implmementation of the fitness level.
		 */
		public delegate double Get(IFoodSource foodSource, Bee bee);
	}
}
