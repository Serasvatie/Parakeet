using Parakeet.Models.Enums;

namespace Parakeet.Models.Inputs
{
	public class DocDistModel
	{
		private int _threshold;
		private Target _target;
		private bool _caseSensitive;
		private bool _percentage;

		public DocDistModel()
		{
			_threshold = 50;
			_target = Target.Folder;
			_caseSensitive = true;
		}

		public DocDistModel(int _thres, Target target)
		{
			_threshold = _thres;
			_target = target;
		}

		public int Threshold
		{
			get { return _threshold; }
			set { _threshold = value; }
		}

		public Target Target
		{
			get { return _target; }
			set { _target = value; }
		}

		public bool CaseSensitive
		{
			get => _caseSensitive;
			set => _caseSensitive = value;
		}

		public bool Percentage
		{
			get { return _percentage; }
			set => _percentage = value;
		}
	}
}
