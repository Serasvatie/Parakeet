using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parakeet.Model;

namespace Parakeet.ViewModel.PrimaryWindow
{
    public class StatusBarViewModel : BaseNotifyPropertyChanged
    {
        public StatusBarViewModel()
        {
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

        public void Refresh()
        {
            RefreshAll();
        }
    }
}