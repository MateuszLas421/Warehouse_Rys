using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//maybe delete

namespace Warehouse_Rys.ViewModel
{
    class LoginVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
       /* private Model.Login _login_user;



        public Model.Login Login_user
        {
            get { return _login_user; }
            set
            {
                _login_user = value;
                OnPropertyChanged("Kursant");
            }
        }
        public LoginVM()
        {
            Login_user = new Model.Login();
            Login_user.Login1 = String.Empty;
        }
        */

    }
}
