using System;
using System.Collections.Generic;

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

		public Colony(int scouts, int size, List<IFoodSource> foodSources, Fitness.Get fitnessGetFunction)
		{
			this.scouts = scouts;
			this.foodSources = foodSources;
			//Initialization
			//  generate random solution array
			List<int> offsetSolution = GetUniqueRandoms(rand, foodSources.Count);

			//Generate the scout bees.
			if (scouts == DEFAULT_SCOUT_COUNT) {
				scouts = (int)(.2 * size);
			}
			for (int i = 0; i < scouts; i++) {
				Bees.Add(new Bee(Bee.StatusType.SCOUT));
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

		public void Run()
		{
			
		}

		private List<int> GetUniqueRandoms(Random random, int count)
		{
			List<int> result = new List<int>(count);
			HashSet<int> set = new HashSet<int>();
			int i = 0;
			while (i++ < count)
  			{
    			int num;
				do {
					num = random.Next();
				} while (!set.Add(num));
				result.Add(num);
			}
			return result;
		}
	}
}
