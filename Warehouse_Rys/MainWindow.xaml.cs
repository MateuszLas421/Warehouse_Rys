using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Warehouse_Rys
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Login _login_user;
        public Login Login_user
        {
            get { return _login_user; }
            set
            {
                _login_user = value;
            }
        }
        public MainWindow()
        {
            InitializeComponent();
            Login_user = new Login();
            Login_user.Login1 = String.Empty;
            Login_user.Password_log = String.Empty;
            Login_user.Ok = false;

                                                                                            ///tymczasowe obejście
            var ProgramWindow = new ProgramWindowin();
            this.Close();
            ProgramWindow.Show();

        }

        public void LoadData()
        {
            Login_user.Password_log = passwordBox.Text;
            Login_user.Login1 = loginBox.Text;
            try
            {
                using (var conn = new SQLiteConnection(@"Data Source=Base.s3db;Version=3;New=False"))
                {
                    try
                    {
                        conn.Open();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message + "problem z połączeniem?");
                    }
                    using (var cmd = new SQLiteCommand("SELECT NickName,Permissions FROM Users WHERE NickName = '" + Login_user.Login1 + "' AND Password = '" + Login_user.Password_log + "'   ", conn))
                    {
                        using (var reader = cmd.ExecuteReader()) 
                        {
                            if (reader.HasRows)
                            {

                                Login_user.Ok = true;
                                var ProgramWindow = new ProgramWindowin();
                                this.Close();
                                ProgramWindow.Show();
                                conn.Close();
                            }
                            else
                            {
                                Login_user.Login1 = null;
                                Login_user.Password_log = null;
                                passwordBox.Clear();
                                loginBox.Clear();
                                MessageBox.Show("Nie poprawne hasło lub login");
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
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
    }
}
