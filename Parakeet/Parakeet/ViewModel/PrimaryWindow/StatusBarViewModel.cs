using System.ComponentModel;
using System.IO;
using System.Windows.Input;
using Parakeet.Model;

namespace Parakeet.ViewModel.PrimaryWindow
{
    public class StatusBarViewModel : BaseNotifyPropertyChanged
    {
        private static StatusBarViewModel _instance;
        static readonly object instancelock = new object();

        private ICommand cancel;

        public StatusBarViewModel()
        {
            Data.getInstance().manager.IsBwStarted += (sender, args) =>
            {
                RefreshAll();
            };
            Data.getInstance().manager.bwTask.RunWorkerCompleted += (sender, args) =>
            {
                RefreshAll();
            };
        }

        public static StatusBarViewModel getInstance()
        {
            if (_instance == null)
            {
                lock (instancelock)
                    if (_instance == null)
                        _instance = new StatusBarViewModel();
            }
            return _instance;
        }

        public void RunRefresh()
        {
            RefreshAll();
        }

        public string FileName => Path.GetFileNameWithoutExtension(Data.getInstance().FileTitle);

        public string StatusTask
        {
            get { return Data.getInstance().manager.bwTask.IsBusy ? "En cours." : "En attente."; }
        }

        public bool StatusValue
        {
            get { return Data.getInstance().manager.bwTask.IsBusy; }
        }

        public ICommand Cancel => this.cancel ?? (cancel = new RelayCommand(DoCancel, CanCancel));

        private void DoCancel()
        {
            Data.getInstance().manager.bwTask.CancelAsync();
        }

        private bool CanCancel()
        {
            return Data.getInstance().manager.bwTask.IsBusy;
        }
    }
}