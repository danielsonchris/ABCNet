using System;
using System.Collections.Generic;
using System.Linq;

namespace ABCNet
{
	/// <summary>
	/// The colony class is used to start and control the colony of bees.
	/// </summary>
	public class Colony
	{
		public double AverageFitness { get; set; }

		public int Dimensions { get; set; } = 2;

		public List<Bee> Bees { get; } = new List<Bee>();

		private const int DEFAULT_SCOUT_COUNT = 2;
		private int scouts = DEFAULT_SCOUT_COUNT;
		private List<IFoodSource> foodSources;
		private Random rand = new Random(Guid.NewGuid().GetHashCode());
		private List<IFoodSource> bestFoodSources;

		public Colony(int scouts, int size, List<IFoodSource> foodSources, Fitness.Get fitnessGetFunction)
		{
			this.scouts = scouts;
			this.foodSources = foodSources;
			//Initialization
			//Generate the scout bees.
			if (scouts == DEFAULT_SCOUT_COUNT) {
				scouts = (int)(.2 * size);
			}
			for (int i = 0; i < scouts; i++) {
				Bees.Add(new Bee(Bee.StatusType.SCOUT, GetUniqueRandoms(rand, foodSources.Count)));
			}
			//Employeed Bees
			int employedBees = (int)(.65 * size);
			for (int i = 0; i < employedBees; i++) {
				Bees.Add(new Bee(Bee.StatusType.EMPLOYED));
			}
			//Onlooker Bees
			for (int i = (scouts + employedBees); i < size; i++) {
				Bees.Add(new Bee(Bee.StatusType.ONLOOKER));
			}
		}

		public List<IFoodSource> Run()
		{
			//Send Employeed Bees
			//Calculate probabilities
			//Send Onlooker Bees 
			//Memorize best foodSources
			//Send Scouts 
			return null;
		}

		public List<T> Run<T>() {
			var list = new List<T>();

			return list;
		}
		
		/// <summary>
		/// Used for generating a unique list of integer values.
		/// </summary>
		/// <param name="random"></param>
		/// <param name="count"></param>
		/// <returns>A list ordered </returns>
		public List<int> GetUniqueRandoms(Random random, int count)
		{
			var list = Enumerable.Range(0, count).ToList();
			list.Sort((x, y) => random.Next(-1, 1));
			return list;
		}
	}
}
