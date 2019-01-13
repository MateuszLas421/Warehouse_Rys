using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Warehouse_Rys.ViewModel
{
    class SqliteLoginVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
        private Model.Login _login_user;



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
            Login_user = new Model.Login();
            Login_user.Login1 = String.Empty;
        }
        public void LoadData()
        {
            try
            {
                using (var conn = new SQLiteConnection(@"Data Source=Base.db;Version=3"))
                {
                    conn.Open();
                    using (var cmd = new SQLiteCommand("SELECT NickName,Password FROM Users WHERE NickName='@username' AND Password = '@password'", conn))
                    {
                        cmd.Parameters.AddWithValue("@username", Login_user.Login1);
                        cmd.Parameters.AddWithValue("@password", Pas);
                        using (var reader = cmd.ExecuteReader())
                        {
                            var count = 0;
                            while (reader.Read())
                            {
                                count = count + 1;
                            }
                            if (count == 1)
                            {
                                Base bs = new Base();
                                bs.Show();
                                Hide();
                            }
                            else if (count == 0)
                            {
                                flatAlertBox1.kind = FlatUI.FlatAlertBox._Kind.Error;
                                flatAlertBox1.Text = "data not right";
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
