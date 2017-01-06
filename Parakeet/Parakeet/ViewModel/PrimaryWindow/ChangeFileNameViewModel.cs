using System.Windows.Input;
using Manager.Manager;
using Parakeet.Model;

namespace Parakeet.ViewModel.PrimaryWindow
{
    class ChangeFileNameViewModel : BaseNotifyPropertyChanged
    {
        private int _selectedItem;

        private string _changeName;
        private string _byName;

        private ICommand _addRules;
        private ICommand _deleteRules;

        public ChangeFileNameViewModel()
        {
            _selectedItem = 0;
        }

        public static SerializableList<ChangeRule> ListChangeRules
        {
            get { return Data.GetInstance().RenameRules; }
        }

        public int SelectedIndex
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
                OnPropertyChanged("SelectedItem");
            }
        }

        public string ChangeName
        {
            get
            {
                return _changeName;

            }
            set
            {
                _changeName = value;
                OnPropertyChanged("ChangeName");
            }
        }

        public string ByName
        {
            get { return _byName; }
            set
            {
                _byName = value;
                OnPropertyChanged("ByName");
            }
        }

        public ICommand AddRules
        {
            get { return this._addRules ?? (this._addRules = new RelayCommand(DoAddRules, CanAddRules)); }
        }

        private bool CanAddRules()
        {
            return ChangeName != null && ByName != null;
        }

        private void DoAddRules()
        {
            var tmp = new ChangeRule(ChangeName, ByName, true, Target.All);
            ChangeName = null;
            ByName = null;
            ListChangeRules.Add(tmp);
        }

        public ICommand DeleteRules
        {
            get { return this._deleteRules ?? (this._deleteRules = new RelayCommand(DoDeleteRules, CanDeleteRules)); }
        }

        private bool CanDeleteRules()
        {
            return ListChangeRules.Count > 0;
        }

        private void DoDeleteRules()
        {
            ListChangeRules.RemoveAt(SelectedIndex);
            SelectedIndex = 0;
        }
    }
}