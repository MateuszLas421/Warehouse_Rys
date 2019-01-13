using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVM_First_code
{
    public class MyCommand : ICommand
    {
         Action _execute;
       /* public MyCommand(Action executeMethod)
        {
            _execute = executeMethod;
        } */
        public void Execute(object parameter)
        {
            if (_execute != null)
            {
                _execute();
            }
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }
        public event EventHandler CanExecuteChanged;
    }
}
