using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DocumentationLogicielle.App
{
    public interface IAsyncCommand : ICommand
    {
        Task ExecuteAsync(object parameter);
    }
    

    public class AsyncCommand : IAsyncCommand
    {
        private readonly Func<Task> _command;
        private readonly Func<bool> _canExecute;
        /// <summary>
        /// Command that can be executed asyncronously
        /// You can create a command here to the execute it later
        /// </summary>
        /// <param name="command"></param>
        /// <param name="canExecute"></param>
        public AsyncCommand(Func<Task> command, Func<bool> canExecute = null)
        {
            _command = command;
            _canExecute = canExecute;
        }

        public AsyncCommand(Func<Task> action) : this(action, null)
        {
            _command = action;
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public async void Execute(object parameter)
        {
            await ExecuteAsync(parameter);
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute.Invoke();
        }

        /// <summary>
        /// Execute a command asyncronously
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public Task ExecuteAsync(object parameter)
        {
            return _command();
        }
    }
}
