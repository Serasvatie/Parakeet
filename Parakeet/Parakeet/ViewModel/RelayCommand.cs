using System;
using System.Windows.Input;

namespace Parakeet.ViewModel
{
    class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        event EventHandler ICommand.CanExecuteChanged
        {
            add
            {
                if (_canExecute != null)
                    CommandManager.RequerySuggested += value;
            }
            remove
            {
                if (_canExecute != null)
                    CommandManager.RequerySuggested += value;
            }
        }

        public RelayCommand(Action execute)
            : this(execute, null) { }

        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            if (execute == null)
                throw new ArgumentException("Action : _execute");
            this._execute = execute;
            this._canExecute = canExecute;
        }

        bool ICommand.CanExecute(object parameter)
        {
            return _canExecute?.Invoke() ?? true;
        }

        void ICommand.Execute(object parameter)
        {
            this._execute();
        }
    }
}