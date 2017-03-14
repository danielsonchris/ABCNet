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
		private List<FoodSource> foodSources;
		// private Random rand = new Random(Guid.NewGuid().GetHashCode());
		// private List<IFoodSource> bestFoodSources;

		private Fitness.Get fitnessGetFunction;

		public Random Rand { get; set; } = new Random(Guid.NewGuid().GetHashCode());

		public Colony(int size, List<FoodSource> foodSources, Fitness.Get fitnessGetFunction)
		{
			this.foodSources = foodSources;
			this.fitnessGetFunction = fitnessGetFunction;
			//Initialization
			//Generate our bee counts and supply a random memory set for each bee.
			scoutCount = (int)(.15 * size);
			onlookerCount = (int)(.10 * size);
			employedCount = size - (scoutCount + onlookerCount);

			for (int i = 0; i < scoutCount; i++) {
				Bees.Add(new Bee(Bee.StatusType.SCOUT, 
					GetUniqueRandoms(Rand, 0, foodSources.Count)));
			}
			//Employeed Bees
			for (int i = 0; i < employedCount; i++) {
				Bees.Add(new Bee(Bee.StatusType.EMPLOYED, 
					GetUniqueRandoms(Rand, 0, foodSources.Count)));
			}
			//Onlooker Bees
			for (int i = 0; i < onlookerCount; i++) {
				Bees.Add(new Bee(Bee.StatusType.ONLOOKER,
					GetUniqueRandoms(Rand, 0, foodSources.Count)));
			}
		}

		public List<FoodSource> Run()
		{
			List<FoodSource> employedBeeSelection = new List<FoodSource>();
			//Send Employeed Bees
			Bees.Where(x => x.Status == Bee.StatusType.EMPLOYED).ToList().ForEach(bee => {
				var primaryFoodSource = bee.MemorizedSolution[0];
				var primaryFoodSourceCoordinate = bee.MemorizedSolution[0].Location.GeoCoordinate;
				double distanceLocation = double.MaxValue;
				FoodSource neighbor = null;
				for (int i=1; i < bee.MemorizedSolution.Count; i++) {
					//locate the nearest neighbor. 
					double distance = bee.MemorizedSolution[i].Location.GeoCoordinate.GetDistanceTo(primaryFoodSourceCoordinate);
					if (distance < distanceLocation) {
						neighbor = bee.MemorizedSolution[i];
					}
				}
				primaryFoodSource.FitnessValue = this.fitnessGetFunction(primaryFoodSource, bee);
				if (neighbor == null) return; // nothing to do.
				//Each employed bee goes to a food source in her memory and determines a neighbour source, 
				//then evaluates its nectar amount and dances in the hive
				neighbor.FitnessValue = this.fitnessGetFunction(neighbor, bee);

			});

			//Each onlooker watches the dance of employed bees and chooses one of their sources depending on the dances, 
			//and then goes to that source. After choosing a neighbour around that, she evaluates its nectar amount.
			
			//Abandoned food sources are determined and are replaced with the new food sources discovered by scouts.

			//Calculate probabilities
			//Send Onlooker Bees 
			//Memorize best foodSources
			//Send Scouts 
			return null;
		}

		public List<T> Run<T>() {
			var list = new List<T>();

			/*
Initial food sources are produced for all employed bees
REPEAT
Each employed bee goes to a food source in her memory and determines a neighbour source, then evaluates its nectar amount and dances in the hive
Each onlooker watches the dance of employed bees and chooses one of their sources depending on the dances, and then goes to that source. After choosing a neighbour around that, she evaluates its nectar amount.
Abandoned food sources are determined and are replaced with the new food sources discovered by scouts.
The best food source found so far is registered.
UNTIL (requirements are met)
 */

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
			list.ForEach(y => Console.Write(string.Format("{0},",y)));
			Console.WriteLine("");
			return list;
		}
	}
}
