using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Manager.Manager;
using Parakeet.Model;

namespace Parakeet.ViewModel.PrimaryWindow
{
    public class DocDistViewModel : BaseNotifyPropertyChanged
    {
        public int Threshold
        {
            get { return Data.GetInstance().DocDistModel.Threshold; }
            set
            {
                if (value >= 0 && value <= 100)
                    Data.GetInstance().DocDistModel.Threshold = value;
                OnPropertyChanged("Threshold");
            }
        }

        public Target Target
        {
            get { return Data.GetInstance().DocDistModel.Target; }
            set
            {
                Data.GetInstance().DocDistModel.Target = value;
                OnPropertyChanged("Threshold");
            }
        }
    }
}
