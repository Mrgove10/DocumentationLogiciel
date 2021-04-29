using System;
using System.Windows.Input;

namespace DocumentationLogicielle.App
{
    public class CommandHandler : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<bool> _canExecute;

        /// <summary>
        /// Creates instance of the command handler
        /// </summary>
        /// <param name="action">Action to be executed by the command</param>
        /// <param name="canExecute">A boolean property to containing current permissions to execute the command</param>
        public CommandHandler(Action<object> action, Func<bool> canExecute)
        {
            _execute = action;
            _canExecute = canExecute;
        }


        public CommandHandler(Action<object> action) : this(action, null)
        {
            _execute = action;
        }

        /// <summary>
        /// Wires CanExecuteChanged event 
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        /// <summary>
        /// Forcess checking if execute is allowed
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return _canExecute.Invoke();
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

    }
}
