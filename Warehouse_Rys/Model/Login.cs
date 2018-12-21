using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Warehouse_Rys.Model
{
    public class Login : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
        private string _login;
        private Boolean ok;

        public Login()
        {
            Ok = false;
        }

        public string Login1
        {
            get => _login;
            set
            {
                if (_login != value) {_login = value; OnPropertyChanged("Login1");}
            }
        }
        public Boolean Ok
        {
            get => ok;
            set
            {
                if (ok != value) { ok = value; OnPropertyChanged("Ok"); }
            }
        }
    }
}
