using Parakeet.Models.Enums;
using System.Collections.Generic;

namespace Parakeet.Models.Outputs
{
	public class PathData
	{
		public string CurrentPath { get; private set; }

		public string ParentPath { get; private set; }

		public List<string> History { get; set; }

		public Target Target { get; private set; }

		public bool IsFromMainDirectories { get; private set; }

		public PathData(string path, string parent, Target target, bool isFromMainDirectories)
		{
			CurrentPath = path;
			ParentPath = parent;
			Target = target;
			IsFromMainDirectories = isFromMainDirectories;
			History = new List<string>();
		}

		public void SetNewPath(string newPath)
		{
			History.Add(CurrentPath);
			CurrentPath = newPath;
		}

		public bool IsPathValid()
		{
			return !string.IsNullOrEmpty(CurrentPath);
		}

		public void MovePath(string destinationFolder, string newPath, bool isFromMainDirectories)
		{
			History.Add(CurrentPath);
			CurrentPath = newPath;
			ParentPath = destinationFolder;
			IsFromMainDirectories = isFromMainDirectories;
		}

		public void UpdateParent(string oldPath, string newPath)
		{
			var newParentPath = newPath + ParentPath.Substring(oldPath.Length);
			var newCurrentPath = newPath + CurrentPath.Substring(oldPath.Length);

			ParentPath = newParentPath;
			CurrentPath = newCurrentPath;
		}
	}
}
