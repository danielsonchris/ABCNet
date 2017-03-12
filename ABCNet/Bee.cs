
using System.Collections.Generic;

namespace ABCNet
{
	public class Bee
	{
		public enum StatusType {
			SCOUT = 0,
			ONLOOKER,
			EMPLOYED
		}

		public List<IFoodSource> MemorizedSolution = new List<IFoodSource>();

		public StatusType Status { get; set; }

		public Bee(StatusType status)
		{
			this.Status = status;
		}
		private List<int> RandomSolution;
		public Bee(StatusType status, List<int> randomSolution) {
			RandomSolution = randomSolution;
		}
	}
}
