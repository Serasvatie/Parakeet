using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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
            Data.getInstance().manager.bwTask.RunWorkerCompleted += Finish;
            Data.getInstance().manager.bwTask.DoWork += Start;
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

        private void Start(object sender, DoWorkEventArgs e)
        {
            RefreshAll();
        }

        private void Finish(object sender, RunWorkerCompletedEventArgs e)
        {
            RefreshAll();
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