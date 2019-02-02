using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WhiteSolution.Utils.WeakEvent;
using Xamarin.Forms;

namespace WhiteSolution.Utils
{
    public class TaskCommand : ICommand
    {
        #region Fields
        private volatile bool _inProgress = false;
        private Func<object, Task> _execute;
        private bool _continueOnCapturedContext;
        private Func<object, bool> _canExecute;
        readonly WeakEventManager _weakEventManager = new WeakEventManager();
        #endregion

        public TaskCommand(Func<object, Task> execute, Func<object, bool> canExecute = null, bool continueOnCapturedContext = true)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
            _continueOnCapturedContext = continueOnCapturedContext;
        }
        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter)
        {
            if (_inProgress)
            {
                return false;
            }
            if (_canExecute != null)
            {
                return _canExecute(parameter);
            }
            return true;
        }
        public void Execute(object parameter)
        {
            _inProgress = true;
            RaiseCanExecuteChanged();
            _execute(parameter).ContinueWith((task) =>
            {
                _inProgress = false;
                RaiseCanExecuteChanged();
            }).ConfigureAwait(_continueOnCapturedContext);
        }
        public void RaiseCanExecuteChanged()
        {
            _weakEventManager.HandleEvent(this, EventArgs.Empty, nameof(CanExecuteChanged));
        }
    }
}
