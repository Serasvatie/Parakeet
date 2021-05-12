using System.Collections.Generic;

namespace Parakeet.Models.Outputs
{
	public class ResultOutput
	{
		public List<RemoveResult> RemoveResults { get; private set; } = new List<RemoveResult>();

		public List<RenameResult> RenameResults { get; private set; } = new List<RenameResult>();

		public List<SortResult> SortingResults { get; private set; } = new List<SortResult>();

		public List<FolderCreationResult> FolderCreationResults { get; private set; } = new List<FolderCreationResult>();

		public List<DocDistResultModel> DocDistResults { get; private set; } = new List<DocDistResultModel>();
	}
}
