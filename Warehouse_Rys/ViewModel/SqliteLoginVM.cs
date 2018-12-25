using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Warehouse_Rys.ViewModel
{
    class SqliteLoginVM : INotifyPropertyChanged
    {
        public string Password { get; set; }
        public System.Security.SecureString SecurePassword { get; }
        

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
        private Model.Login _login_user;

        public ICommand LoginCheck { get; set; }

        public Model.Login Login_user
        {
            get { return _login_user; }
            set
            {
                _login_user = value;
                OnPropertyChanged("Login_user");
            }
        }
        public SqliteLoginVM()
        {
            MessageBox.Show("create");
            System.Windows.Controls.PasswordBox passwordBox = new System.Windows.Controls.PasswordBox();
            Login_user = new Model.Login();
            Login_user.Login1 = String.Empty;
            Login_user.Password_log = passwordBox.Password;
            Login_user.Ok = false;
            try
            {
                LoginCheck = new RelayCommand(Login_Check);
            }
            catch
            {
                MessageBox.Show("error wiązania");
            }
        }
        private void Login_Check()
        {
            MessageBox.Show("Interfejs Icommand click");
            LoadData();
        }

        public void LoadData()
        {
            try
            {
                MessageBox.Show("try");
                using (var conn = new SQLiteConnection(@"Data Source=Base.s3db;Version=3"))
                {
                    try
                    {
                        conn.Open();
                    }
                    catch(Exception ex)
            {
                        MessageBox.Show(ex.Message);
                    }
                    using (var cmd = new SQLiteCommand("SELECT NickName,Password FROM Users WHERE NickName='@username' AND Password = '@password'", conn))
                    {
                        cmd.Parameters.AddWithValue("@username", Login_user.Login1);
                        cmd.Parameters.AddWithValue("@password", Login_user.Password_log);
                        using (var reader = cmd.ExecuteReader())
                        {
                            var count = 0;
                            while (reader.Read())
                            {
                                count = count + 1;
                            }
                            if (count == 1)
                            {
                                Login_user.Ok = true;
                            }
                            else if (count == 0)
                            {
                                Login_user.Login1 = null;
                                Login_user.Password_log = null;

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
