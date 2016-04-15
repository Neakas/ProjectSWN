using System;
using System.Windows.Input;

namespace SWNAdmin.Forms.EncyclopediaManager.Interaction
{
    public class Command : ICommand
    {
        private readonly bool _canExecute;
        private readonly Action<object> _execute;

        private Command( bool canExecute, Action<object> execute )
        {
            _canExecute = canExecute;
            _execute = execute;
        }

        public Command( Action<object> execute ) : this(true, execute)
        {
        }

        /// <summary>
        ///     Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged;

        public bool CanExecute( object parameter )
        {
            return _canExecute;
        }

        public void Execute( object parameter )
        {
            if (!CanExecute(parameter))
            {
                throw new InvalidOperationException("Invalid command execution requested");
            }

            _execute(parameter);
        }
    }
}