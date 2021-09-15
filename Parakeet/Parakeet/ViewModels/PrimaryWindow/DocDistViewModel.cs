using Parakeet.Models;
using Parakeet.Models.Enums;
using Parakeet.Services;
using Prism.Events;
using Prism.Mvvm;

namespace Parakeet.ViewModels.PrimaryWindow
{
	public class DocDistViewModel : BindableBase
	{
		private readonly IProjectHelper _projectHelper;

		private int _treshold;
		private Target _target;
		private bool _isSensitiveCase;
		private bool _percentage;

		public DocDistViewModel(IProjectHelper projectHelper, IEventAggregator ea)
		{
			_projectHelper = projectHelper;

			ea.GetEvent<ProjectChangedEvent>().Subscribe(ProjectChanged);
		}

		private void ProjectChanged()
		{
			SetProperty(ref _treshold, _projectHelper.Project.DocDist.Threshold, nameof(Threshold));
			SetProperty(ref _target, _projectHelper.Project.DocDist.Target, nameof(Target));
		}

		public int Threshold
		{
			get { return _treshold; }
			set
			{

				if (value >= 0 && value <= 100)
				{
					_projectHelper.Project.DocDist.Threshold = value;
					SetProperty(ref _treshold, value);
				}
			}
		}

		public Target Target
		{
			get { return _target; }
			set
			{
				_projectHelper.Project.DocDist.Target = value;
				SetProperty(ref _target, value);
			}
		}

		public bool IsSensitiveCase
		{
			get { return _isSensitiveCase; }
			set
			{
				_projectHelper.Project.DocDist.CaseSensitive = value;
				SetProperty(ref _isSensitiveCase, value);
			}
		}

		public bool Percentage
		{
			get { return _percentage; }
			set
			{
				_projectHelper.Project.DocDist.Percentage = value;
				SetProperty(ref _percentage, value);
			}
		}
	}
}
