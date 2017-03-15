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
		public StatusType Status { get; set; }
		public List<int> RandomSolution;
		public Bee(StatusType status, List<int> randomSolution) {
			Status = status;
			RandomSolution = randomSolution;
		}
	}
}
