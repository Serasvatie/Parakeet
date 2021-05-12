using Parakeet.Models.Enums;
using Parakeet.Models.Inputs;

namespace Parakeet.Models.Outputs
{
	public class RenameResult
	{
		public int Id { get; set; }
		public RenameRule Rule { get; set; }
		public Target Type { get; set; }
		public string OldPath { get; set; }
		public string NewPath { get; set; }
	}
}
