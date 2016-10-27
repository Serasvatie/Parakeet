using System.Collections.ObjectModel;
using System.Windows.Input;
using Parakeet.Model;

namespace Parakeet.ViewModel.PrimaryWindow
{
    class ChangeFileNameViewModel : BaseNotifyPropertyChanged
    {
        private Data data;
        private ObservableCollection<ChangeRule> rules;
        private int selectedItem;

        private string changeName;
        private string byName;

        private ICommand addRules;
        private ICommand deleteRules;

        public ChangeFileNameViewModel(Data data)
        {
            this.data = data;
            rules = new ObservableCollection<ChangeRule>();
            selectedItem = 0;
        }

        public ObservableCollection<ChangeRule> ListChangeRules
        {
            get { return rules; }
        }

        public int SelectedIndex
        {
            get
            {
                return selectedItem;
            }
            set
            {
                selectedItem = value;
                OnPropertyChanged("SelectedItem");
            }
        }

        public string ChangeName
        {
            get
            {
                return changeName;

            }
            set
            {
                changeName = value;
                OnPropertyChanged("ChangeName");
            }
        }

        public string ByName
        {
            get { return byName; }
            set
            {
                byName = value;
                OnPropertyChanged("ByName");
            }
        }

        public ICommand AddRules
        {
            get { return this.addRules ?? (this.addRules = new RelayCommand(DoAddRules, CanAddRules)); }
        }

        private bool CanAddRules()
        {
            return ChangeName != "" && ByName != "";
        }

        private void DoAddRules()
        {
            var tmp = new ChangeRule(ChangeName, ByName, true);
            ChangeName = "";
            ByName = "";
            rules.Add(tmp);
        }

        public ICommand DeleteRules
        {
            get { return this.deleteRules ?? (this.deleteRules = new RelayCommand(DoDeleteRules, CanDeleteRules)); }
        }

        private bool CanDeleteRules()
        {
            return rules.Count > 0;
        }

        private void DoDeleteRules()
        {
            rules.RemoveAt(SelectedIndex);
            SelectedIndex = 0;
        }
    }
}