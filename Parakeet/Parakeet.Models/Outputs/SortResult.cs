using Parakeet.Models.Enums;

namespace Parakeet.Models.Outputs
{
	public class SortResult
	{
		public Target Type { get; set; }
		public string OldPath { get; set; }
		public string NewPath { get; set; }

		public string Destination { get; set; }
	}
}
