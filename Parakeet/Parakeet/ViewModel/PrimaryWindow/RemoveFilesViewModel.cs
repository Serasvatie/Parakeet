using System.Windows.Input;
using Manager.Manager;
using Parakeet.Model;

namespace Parakeet.ViewModel.PrimaryWindow
{
    class RemoveFilesViewModel : BaseNotifyPropertyChanged
    {
        private int _selectedIndex;

        private string _strings;

        private ICommand _addRules;
        private ICommand _deleteRules;
        private ICommand _doUp;
        private ICommand _doDown;

        public RemoveFilesViewModel()
        {
            _selectedIndex = 0;
        }

        public static SerializableList<RemoveRule> ListRules
        {
            get { return Data.GetInstance().RemoveRules; }
        }

        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                _selectedIndex = value;
                OnPropertyChanged("SelectedIndex");
            }
        }

        public string Strings
        {
            get { return _strings; }
            set
            {
                _strings = value;
                OnPropertyChanged("Strings");
            }
        }

        public ICommand AddRules
        {
            get { return this._addRules ?? (this._addRules = new RelayCommand(DoAddRules, CanAddRules)); }
        }

        private bool CanAddRules()
        {
            return Strings != null;
        }

        private void DoAddRules()
        {
            var tmp = new RemoveRule(Strings, true, true);
            ListRules.Add(tmp);
            Strings = null;
        }

        public ICommand DeleteEntry
        {
            get { return this._deleteRules ?? (this._deleteRules = new RelayCommand(DoDeleteRules, CanDeleteRules)); }
        }

        private bool CanDeleteRules()
        {
            return ListRules.Count > 0;
        }

        private void DoDeleteRules()
        {
            ListRules.RemoveAt(SelectedIndex);
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
            ListRules.Move(SelectedIndex, SelectedIndex - 1);
        }

        public ICommand DoDown
        {
            get { return this._doDown ?? (this._doDown = new RelayCommand(DoDownC, CanDown)); }
        }

        private bool CanDown()
        {
            return SelectedIndex < ListRules.Count - 1;
        }

        private void DoDownC()
        {
            ListRules.Move(SelectedIndex, SelectedIndex + 1);
        }
    }
}
