using System.ComponentModel;
using System.IO;
using System.Windows.Input;
using Parakeet.Model;
using Parakeet.Properties;

namespace Parakeet.ViewModel.PrimaryWindow
{
    public class StatusBarViewModel : BaseNotifyPropertyChanged
    {
        private static StatusBarViewModel _instance;
        static readonly object Instancelock = new object();

        private ICommand _cancel;

        public StatusBarViewModel()
        {
            Data.GetInstance().FFManager.IsBwStarted += (sender, args) =>
            {
                RefreshAll();
            };
            Data.GetInstance().FFManager.BwTask.RunWorkerCompleted += (sender, args) =>
            {
                RefreshAll();
            };
            Data.GetInstance().SManager.BwTask.RunWorkerCompleted += (sender, args) =>
            {
                RefreshAll();
            };
            Data.GetInstance().SManager.IsBwStarted += (sender, args) =>
            {
                RefreshAll();
            };
        }

        public static StatusBarViewModel GetInstance()
        {
            if (_instance == null)
            {
                lock (Instancelock)
                    if (_instance == null)
                        _instance = new StatusBarViewModel();
            }
            return _instance;
        }

        public void RunRefresh()
        {
            RefreshAll();
        }

        public string FileName => Path.GetFileNameWithoutExtension(Data.GetInstance().FileTitle);

        public string StatusTask
        {
            get { return Data.GetInstance().FFManager.BwTask.IsBusy || Data.GetInstance().SManager.BwTask.IsBusy ? Resources.StatusBarViewModel_StatusTask_Waiting : Resources.StatusBarViewModel_StatusTask_Running; }
        }

        public bool StatusValue
        {
            get { return Data.GetInstance().FFManager.BwTask.IsBusy || Data.GetInstance().SManager.BwTask.IsBusy; }
        }

        public ICommand Cancel => this._cancel ?? (_cancel = new RelayCommand(DoCancel, CanCancel));

        private void DoCancel()
        {
            if (Data.GetInstance().FFManager.BwTask.IsBusy)
                Data.GetInstance().FFManager.BwTask.CancelAsync();
            if (Data.GetInstance().SManager.BwTask.IsBusy)
                Data.GetInstance().SManager.BwTask.CancelAsync();
        }

        private bool CanCancel()
        {
            return Data.GetInstance().FFManager.BwTask.IsBusy || Data.GetInstance().SManager.BwTask.IsBusy;
        }
    }
}