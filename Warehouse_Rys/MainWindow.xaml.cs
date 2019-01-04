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
        }

        public void LoadData()
        {
            try
            {
                //MessageBox.Show("try");
                using (var conn = new SQLiteConnection(@"Data Source=Base.s3db;Version=3;New=False"))
                {
                    try
                    {
                        //MessageBox.Show("próba otwarcia ?");
                        conn.Open();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                        MessageBox.Show("problem z połączeniem?");
                    }
                    using (var cmd = new SQLiteCommand("SELECT NickName,Password,Permissions FROM Users WHERE NickName = '@username' AND Password = '@password'", conn))
                    {
                        try
                        {
                            cmd.Parameters.AddWithValue("@username", Login_user.Login1);
                            cmd.Parameters.AddWithValue("@password", Login_user.Password_log);
                            // MessageBox.Show(Login_user.Login1);
                            // MessageBox.Show(Login_user.Password_log);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("niepowodzenie połączenia");
                            MessageBox.Show(ex.Message);
                        }
                        using (var reader = cmd.ExecuteReader())     /// coś tu nie działa bo nie idzie się zalogować   do naprawienia
                        {

                            /* var count = 0;
                             while (reader.Read())
                             {
                                 count += 1;
                             }*/
                            if (reader.HasRows)
                            {

                                Login_user.Ok = true;
                            }
                            else
                            {
                                Login_user.Login1 = null;
                                Login_user.Password_log = null;
                            }
                            MessageBox.Show(Login_user.Ok.ToString());
                            var ProgramWindow = new ProgramWindowin();
                            this.Close();
                            ProgramWindow.Show();

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("plik problem?");
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }
    }
}
