using System.Collections.ObjectModel;
using System.Windows.Input;
using Manager.Manager;

namespace Parakeet.ViewModel.PrimaryWindow
{
    public class SortByViewModel : BaseNotifyPropertyChanged
    {
        private static ObservableCollection<SortByRule> _rules;
        private int _selectedIndex;

        private string _strings;

        private ICommand _addRules;
        private ICommand _deleteRules;

        public SortByViewModel()
        {
            _rules = new ObservableCollection<SortByRule>();
            _selectedIndex = 0;
            _strings = null;
        }

        public static ObservableCollection<SortByRule> ListRules
        {
            get { return _rules; }
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
            var tmp = new SortByRule(Strings, true);
            _rules.Add(tmp);
            Strings = null;
        }

        public ICommand DeleteRules
        {
            get { return this._deleteRules ?? (this._deleteRules = new RelayCommand(DoDeleteRules, CanDeleteRules)); }
        }

        private bool CanDeleteRules()
        {
            return _rules.Count > 0;
        }

        private void DoDeleteRules()
        {
            _rules.RemoveAt(SelectedIndex);
            SelectedIndex = 0;
        }
    }
}