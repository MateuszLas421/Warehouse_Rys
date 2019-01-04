using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Security;

namespace Warehouse_Rys
{
    public class Login 
    {
        private string _login;
        private String _password;
        private bool ok;

        public Login()
        {
            Ok = false;
        }

        public string Login1
        {
            get => _login;
            set
            {
                if (_login != value) {_login = value; }
            }
        }
        public String Password_log
        {
            get => _password;
            set
            {
                if (_password != value) { _password = value; }
            }
        }
        public bool Ok
        {
            get => ok;
            set
            {
                if (ok != value) { ok = value;  }
            }
        }
    }
}
