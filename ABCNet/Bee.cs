using System;
namespace ABCNet
{
	public class Bee
	{
		public enum Status {
			SCOUT = 0,
			ONLOOKER,
			EMPLOYEE
		}

		public Status Status { get; set; }

		public Bee(Status status)
		{
			this.Status = status;
		}
	}
}
