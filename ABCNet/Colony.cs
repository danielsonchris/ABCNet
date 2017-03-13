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

		public int MaxVisits { get; set; } = 50;

		public int MaxCycles { get; set; } = 10000;

		private int scoutCount = 0;
		private int employedCount = 0;
		private int onlookerCount = 0;
		private List<global::FoodSource> foodSources;
		private Random rand = new Random(Guid.NewGuid().GetHashCode());
		// private List<IFoodSource> bestFoodSources;

		public Colony(int size, List<global::FoodSource> foodSources, Fitness.Get fitnessGetFunction)
		{
			this.foodSources = foodSources;
			//Initialization
			//Generate our bee counts
			scoutCount = (int)(.15 * size);
			onlookerCount = (int)(.10 * size);
			employedCount = (int)(.75 * size);

			for (int i = 0; i < scoutCount; i++) {
				Bees.Add(new Bee(Bee.StatusType.SCOUT, 
					GetUniqueRandoms(rand, 0, foodSources.Count)));
			}
			//Employeed Bees
			for (int i = 0; i < employedCount; i++) {
				Bees.Add(new Bee(Bee.StatusType.EMPLOYED, 
					GetUniqueRandoms(rand, 0, foodSources.Count)));
			}
			//Onlooker Bees
			for (int i = 0; i < onlookerCount; i++) {
				Bees.Add(new Bee(Bee.StatusType.ONLOOKER,
					GetUniqueRandoms(rand, 0, foodSources.Count)));
			}
		}

		public List<global::FoodSource> Run()
		{
			//Send Employeed Bees
			Bees.Where(x => x.Status == Bee.StatusType.EMPLOYED).ToList().ForEach(x => {
				
			});
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
		/// Used for generating a unique list of integer values starting at `start` value.
		/// </summary>
		/// <param name="random"></param>
		/// <param name="start">The number to start counting from. </param>
		/// <param name="count">Total number of elements to put in array.</param>
		/// <returns>A list of unique integers randomly sorted.</returns>
		public List<int> GetUniqueRandoms(Random random, int start, int count)
		{
			var list = Enumerable.Range(start, count).ToList();
			list.Sort((x, y) => random.Next(-1, 1));
			return list;
		}
	}
}
