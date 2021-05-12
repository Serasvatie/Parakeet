using Parakeet.Models.Inputs;
using Parakeet.Models.Outputs;
using System.Collections.Generic;

namespace Parakeet.Manager
{
	public class ManagerData
	{
		public List<PathData> Paths { get; set; }

		public LauncherParameter Parameters { get; set; }

		public ManagerData(LauncherParameter parameters)
		{
			Parameters = parameters;
			Paths = new List<PathData>();
		}
	}
}
