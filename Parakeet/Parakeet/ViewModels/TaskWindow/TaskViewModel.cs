using Parakeet.Properties;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;

namespace Parakeet.ViewModels.TaskWindow
{
	public class TaskViewModel : BindableBase, IDialogAware
	{
		private bool _isRecursive;
		private bool _isRemove;
		private bool _isRename;
		private bool _isSort;
		private bool _isChecking;

		private DelegateCommand<string> _closeDialogCommand;
		public DelegateCommand<string> CloseDialogCommand =>
			_closeDialogCommand ?? (_closeDialogCommand = new DelegateCommand<string>(CloseDialog, CanCloseDialog)
				.ObservesProperty(() => IsRemove)
				.ObservesProperty(() => IsRename)
				.ObservesProperty(() => IsSort)
				.ObservesProperty(() => IsChecking));

		public event Action<IDialogResult> RequestClose;

		public bool IsRecursive
		{
			get { return _isRecursive; }
			set { SetProperty(ref _isRecursive, value); }
		}

		public bool IsRemove
		{
			get { return _isRemove; }
			set { SetProperty(ref _isRemove, value); }
		}

		public bool IsRename
		{
			get { return _isRename; }
			set { SetProperty(ref _isRename, value); }
		}

		public bool IsSort
		{
			get { return _isSort; }
			set { SetProperty(ref _isSort, value); }
		}

		public bool IsChecking
		{
			get { return _isChecking; }
			set { SetProperty(ref _isChecking, value); }
		}

		public string Title { get; set; }

		public TaskViewModel()
		{
		}

		private void CloseDialog(string obj)
		{
			ButtonResult result = ButtonResult.None;

			if (obj?.ToLower() == "true")
			{
				result = ButtonResult.OK;
			}
			else if (obj?.ToLower() == "false")
				result = ButtonResult.Cancel;

			RaiseRequestClose(new DialogResult(result));
		}

		public virtual void RaiseRequestClose(IDialogResult dialogResult)
		{
			RequestClose?.Invoke(dialogResult);
		}

		public bool CanCloseDialog()
		{
			return true;
		}

		private bool CanCloseDialog(string arg)
		{
			if (arg?.ToLower() == "true")
			{
				return IsRename || IsRemove || IsSort || IsChecking;
			}
			return true;
		}

		public void OnDialogClosed()
		{
			Settings.Default.TaskSort = _isSort;
			Settings.Default.TaskRename = _isRename;
			Settings.Default.TaskRecursive = _isRecursive;
			Settings.Default.TaskRemove = _isRemove;
			Settings.Default.TaskCheck = _isChecking;
		}

		public void OnDialogOpened(IDialogParameters parameters)
		{
			this.IsRecursive = Settings.Default.TaskRecursive;
			this.IsRemove = Settings.Default.TaskRemove;
			this.IsSort = Settings.Default.TaskSort;
			this.IsRename = Settings.Default.TaskRename;
			this.IsChecking = Settings.Default.TaskCheck;
			Title = Strings.TaskView_TitleWindow;
		}
	}
}