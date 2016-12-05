using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Parakeet.Model;

namespace Parakeet.ViewModel.PrimaryWindow
{
    public class SortByViewModel : BaseNotifyPropertyChanged
    {
        private static ObservableCollection<SortByRule> rules;
        private int selectedIndex;

        private string strings;

        private ICommand addRules;
        private ICommand deleteRules;

        public SortByViewModel()
        {
            rules = new ObservableCollection<SortByRule>();
            selectedIndex = 0;
            strings = null;
        }

        public static ObservableCollection<SortByRule> ListRules
        {
            get { return rules; }
        }

        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                selectedIndex = value;
                OnPropertyChanged("SelectedIndex");
            }
        }

        public string Strings
        {
            get { return strings; }
            set
            {
                strings = value;
                OnPropertyChanged("Strings");
            }
        }

        public ICommand AddRules
        {
            get { return this.addRules ?? (this.addRules = new RelayCommand(DoAddRules, CanAddRules)); }
        }

        private bool CanAddRules()
        {
            return Strings != null;
        }

        private void DoAddRules()
        {
            var tmp = new SortByRule(Strings, true);
            rules.Add(tmp);
            Strings = null;
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