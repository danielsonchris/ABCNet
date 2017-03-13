﻿using System.Collections.Generic;

namespace ABCNet
{
	public class Bee
	{
		public enum StatusType {
			SCOUT = 0,
			ONLOOKER,
			EMPLOYED
		}
		public List<FoodSource> MemorizedSolution = new List<FoodSource>();
		public StatusType Status { get; set; }
		private List<int> RandomSolution;
		public Bee(StatusType status)
		{
			this.Status = status;
		}
		public Bee(StatusType status, List<int> randomSolution) {
			RandomSolution = randomSolution;
		}
	}
}
