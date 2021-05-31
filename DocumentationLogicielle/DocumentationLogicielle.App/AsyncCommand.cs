using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DocumentationLogicielle.App
{
    /// <summary>
    /// Interface for the async command
    /// </summary>
    public interface IAsyncCommand : ICommand
    {
        Task ExecuteAsync(object parameter);
    }
    
    /// <summary>
    /// Class for the async command
    /// <remarks>Based on the interface</remarks>
    /// </summary>
    public class AsyncCommand : IAsyncCommand
    {
        private readonly Func<Task> _command;
        private readonly Func<bool> _canExecute;
        
        /// <summary>
        /// Command that can be executed asyncronously
        /// <remarks>This constructor is for the command used with a "Can be execute"</remarks>
        /// </summary>
        /// <param name="command"></param>
        /// <param name="canExecute"></param>
        public AsyncCommand(Func<Task> command, Func<bool> canExecute = null)
        {
            _command = command;
            _canExecute = canExecute;
        }

        /// <summary>
        /// Command that can be executed asyncronously
        /// </summary>
        /// <param name="action"></param>
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

        /// <summary>
        /// Function for check if it can be executed
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
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
