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
        private ICommand _doUp;
        private ICommand _doDown;

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

        public ICommand DeleteEntry
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

        public ICommand DoUp
        {
            get { return this._doUp ?? (this._doUp = new RelayCommand(DoUpC, CanUp)); }
        }

        private bool CanUp()
        {
            return SelectedIndex >= 1;
        }

        private void DoUpC()
        {
            ListChangeRules.Move(SelectedIndex, SelectedIndex - 1);
        }

        public ICommand DoDown
        {
            get { return this._doDown ?? (this._doDown = new RelayCommand(DoDownC, CanDown)); }
        }

        private bool CanDown()
        {
            return SelectedIndex < ListChangeRules.Count - 1;
        }

        private void DoDownC()
        {
            ListChangeRules.Move(SelectedIndex, SelectedIndex + 1);
        }
    }
}