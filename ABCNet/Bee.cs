using System;
namespace ABCNet
{
	public class Bee
	{
		public enum StatusType {
			SCOUT = 0,
			ONLOOKER,
			EMPLOYEE
		}

		public StatusType Status { get; set; }

		public Bee(StatusType status)
		{
			this.Status = status;
		}
	}
}
