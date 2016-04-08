using System;
using System.Windows.Input;

namespace SWNAdmin.Forms.EncyclopediaManager.Interaction
{
    public class Command : ICommand
    {
        private readonly Action<object> _execute;

        private bool _canExecute;

        private Command(bool canExecute, Action<object> execute)
        {
            _canExecute = canExecute;
            _execute = execute;
        }

        public Command(Action<object> execute)
            : this(true, execute)
        {
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        public void Execute(object parameter)
        {
            if (!CanExecute(parameter))
                throw new InvalidOperationException("Invalid command execution requested");

            _execute(parameter);
        }
    }
}