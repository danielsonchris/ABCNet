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
		//public double AverageFitness { get; set; }

		//public int Dimensions { get; set; } = 2;

		public List<Bee> Bees { get; } = new List<Bee>();

		public int MaxVisits { get; set; } = 50;

		public int MaxCycles { get; set; } = 10000;

		private int scoutCount = 0;
		private int employedCount = 0;
		private int onlookerCount = 0;
		private List<FoodSource> foodSources;
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

		/// <summary>
		/// Performs the colony search for the optimal food source.
		/// </summary>
		/// <returns>A List of FoodSources that have been sorted by the fittest at the top.</returns>
		public List<FoodSource> Run()
		{
			List<FoodSource> employedBeeSelection = new List<FoodSource>();
			List<FoodSource> onlookerBeeSelection = new List<FoodSource>();
			//Send Employeed Bees
			Bees.Where(x => x.Status == Bee.StatusType.EMPLOYED).ToList().ForEach(bee => {
				var primaryFoodSource = foodSources[bee.RandomSolution[0]];
				PerformBeePrimaryAndNeighborFitness(primaryFoodSource, bee, employedBeeSelection);
			});
			employedBeeSelection.Sort(new FoodSourceComparer());
			int topValue = (int)(employedBeeSelection.Count * .3d); //top 30% dances win for health.
			//Each onlooker watches the dance of employed bees and chooses one of their sources depending on the dances, 
			//and then goes to that source. After choosing a neighbour around that, she evaluates its nectar amount.
			Bees.Where(x => x.Status == Bee.StatusType.ONLOOKER).ToList().ForEach(bee => {
				int nextTest = Rand.Next(0, topValue);
				var primaryFoodSource = employedBeeSelection[nextTest];
				PerformBeePrimaryAndNeighborFitness(primaryFoodSource, bee, onlookerBeeSelection);
			});

			//Abandoned food sources are determined and are replaced with the new food sources discovered by scouts.
			foodSources.Sort(new FoodSourceComparer());
			//Reset the foodsources where the trial count has gone > MaxVisits.
			foodSources.Where(x => x.TrialsCount > MaxVisits).ToList().ForEach(x => { 
				x.TrialsCount = 0; 
				x.IsAbandoned = false; 
			});

			//have the scouts attempt a query against any new sites to seed them into the future state.

			return foodSources;
		}

		private void PerformBeePrimaryAndNeighborFitness(FoodSource primaryFoodSource, 
		                                                 Bee bee, List<FoodSource> foodSourceSelection) {
			var primaryFoodSourceCoordinate = primaryFoodSource.Location.GeoCoordinate;
			double distanceLocation = double.MaxValue;
			FoodSource neighbor = null;
			foreach(var source in foodSources) {
				//Ignore if the current source is the same
				if (source.ToString() == primaryFoodSource.ToString()) continue;
				//Locate the nearest neighbor that is not abandoned.
				if (source.IsAbandoned) continue;
				double distance = source.Location.GeoCoordinate.GetDistanceTo(primaryFoodSourceCoordinate);
				if (distance < distanceLocation) {
					neighbor = source;
				}
			}
			//Each employed bee goes to a food source in her memory and determines a neighbour source, 
			//then evaluates its nectar amount and dances in the hive
			primaryFoodSource.FitnessValue = this.fitnessGetFunction(primaryFoodSource, bee);
			if (primaryFoodSource.FitnessValue <= 0) primaryFoodSource.IsAbandoned = true;
			primaryFoodSource.TrialsCount++;
			foodSourceSelection.Add(primaryFoodSource);
			if (neighbor != null) {
				neighbor.FitnessValue = this.fitnessGetFunction(neighbor, bee);
				if (neighbor.FitnessValue <= 0) neighbor.IsAbandoned = true;
				neighbor.TrialsCount++;
				foodSourceSelection.Add(neighbor);
			}
		}

			/*
Initial food sources are produced for all employed bees
REPEAT
Each employed bee goes to a food source in her memory and determines a neighbour source, then evaluates its nectar amount and dances in the hive
Each onlooker watches the dance of employed bees and chooses one of their sources depending on the dances, and then goes to that source. After choosing a neighbour around that, she evaluates its nectar amount.
Abandoned food sources are determined and are replaced with the new food sources discovered by scouts.
The best food source found so far is registered.
UNTIL (requirements are met)
 */
		
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
			list.Sort((x, y) => random.Next(-1, 1)); //assign a random location of the unique value.
			//list.ForEach(y => Console.Write(string.Format("{0},",y)));
			//Console.WriteLine("");
			return list;
		}
	}
}
