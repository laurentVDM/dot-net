using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace WpfEmployee.ViewModels
{
    public class DelegateCommand : ICommand
    {
        private Action _executeMethod;
        public event EventHandler CanExecuteChanged;
        public DelegateCommand(Action executeMethod)
        {
            _executeMethod = executeMethod;
        }
        public bool CanExecute(object parameter) { return true; }
        public void Execute(object parameter)
        {
            _executeMethod.Invoke();
        }
    }

}
