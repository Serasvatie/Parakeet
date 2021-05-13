namespace Parakeet.Models.Outputs
{
	public class RemoveResult
	{
		public PathData Path { get; private set; }

		public RemoveResult(PathData path)
		{
			Path = path;
		}
	}
}