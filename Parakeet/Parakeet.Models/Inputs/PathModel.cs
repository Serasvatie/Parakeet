namespace Parakeet.Models.Inputs
{
	public class PathModel
	{
		private string _path;
		private bool _activated;

		public PathModel()
		{
			_path = "";
			_activated = true;
		}

		public PathModel(string path, bool activated)
		{
			_path = path;
			_activated = activated;
		}

		public string Path
		{
			get { return _path; }
			set { _path = value; }
		}

		public bool Activated
		{
			get { return _activated; }
			set { _activated = value; }
		}
	}
}
