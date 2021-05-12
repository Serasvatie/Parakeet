namespace Parakeet.Models.Outputs
{
	public class DocDistResultModel
	{
		private PathData _first;
		private PathData _second;
		private double _dist;
		private double _pourcent;

		public DocDistResultModel()
		{
			_first = null;
			_second = null;
			_dist = 0;
			_pourcent = 0;
		}

		public DocDistResultModel(PathData first, PathData second, double dist, double pourcent)
		{
			_first = first;
			_second = second;
			_dist = dist;
			_pourcent = pourcent;
		}

		public PathData First
		{
			get { return _first; }
			set { _first = value; }
		}

		public PathData Second
		{
			get { return _second; }
			set { _second = value; }
		}

		public double Distance
		{
			get { return _dist; }
			set { _dist = value; }
		}

		public double Percentage
		{
			get { return _pourcent; }
			set { _pourcent = value; }
		}

		public override bool Equals(object other)
		{
			DocDistResultModel item = other as DocDistResultModel;
			if (item == null)
				return false;
			if (_dist != item._dist)
				return false;
			if (_first.CurrentPath != item._first.CurrentPath && _first.CurrentPath != item._second.CurrentPath)
				return false;
			if (_second.CurrentPath != item._first.CurrentPath && _first.CurrentPath != item._second.CurrentPath)
				return false;
			return true;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
