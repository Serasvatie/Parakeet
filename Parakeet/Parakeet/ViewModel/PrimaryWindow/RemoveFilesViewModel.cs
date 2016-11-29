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
    class RemoveFilesViewModel : BaseNotifyPropertyChanged
    {
        private Data _data;
        private static SerializableList<RemoveRule> rules;
        private int selectedIndex;

        private string strings;

        private ICommand addRules;
        private ICommand deleteRules;

        public RemoveFilesViewModel(Data data)
        {
            _data = data;
            selectedIndex = 0;
            rules = new SerializableList<RemoveRule>();
        }

        public static SerializableList<RemoveRule> ListRules
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
            var tmp = new RemoveRule(Strings, true, true);
            ListRules.Add(tmp);
            Strings = null;
        }

        public ICommand DeleteRules
        {
            get { return this.deleteRules ?? (this.deleteRules = new RelayCommand(DoDeleteRules, CanDeleteRules)); }
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
    }
}
