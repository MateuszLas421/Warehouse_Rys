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
using System.Windows.Shapes;

namespace Warehouse_Rys
{
    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        private List<string> permisionslist = new List<string>();
        private Login _login_user;
        public Login Login_user
        {
            get { return _login_user; }
            set
            {
                _login_user = value;
            }
        }
        public RegistrationWindow()
        {
            InitializeComponent();
            Login_user = new Login();
            Login_user.Login1 = String.Empty;
            Login_user.Password_log = String.Empty;
            Login_user.Ok = false;
            string a;
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
                using (var cmd = new SQLiteCommand("SELECT Name FROM Permissions", conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            a = reader.GetString(0);
                            permisionslist.Add(a);
                        }
                    }
                }
            }
            CBpermissions.ItemsSource = permisionslist;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Login_user.Password_log = passwordBox.Password;
            Login_user.Login1 = loginBox.Text;
            Login_user.Name = nameBox.Text;
            Login_user.Surname = surnameBox.Text;
            int plCBpermissions = 0;
            for(int i=0;i<permisionslist.Count;i++)
            {
                if (permisionslist[i] == CBpermissions.Text.ToString())
                {
                    plCBpermissions = i;
                    break;
                }
            }
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
                               loginBox.BorderBrush = Brushes.Red;
                               passwordBox.BorderBrush = Brushes.Red;
                               passwordBoxweryfi.BorderBrush = Brushes.Red;
                               nameBox.BorderBrush = Brushes.Red;
                               surnameBox.BorderBrush = Brushes.Red;
                               MessageBox.Show("Takie konto już istnieje");
                        }
                        else
                        {
                               if (passwordBox.Password != passwordBoxweryfi.Password)
                               {
                                   passwordBox.BorderBrush = Brushes.Red;
                                   passwordBoxweryfi.BorderBrush = Brushes.Red;
                                   MessageBox.Show("Hasła nie są takie same");
                               }
                               if (passwordBox.Password == passwordBoxweryfi.Password)
                               {
                                   var strsql = "INSERT INTO Users (Name,SurName,NickName,Permissions,Password) VALUES (@Name,@SurName,@NickName,@Permissions,@Password)";
                                   using (var insertSQL = new SQLiteCommand(strsql, conn))
                                   {
                                       insertSQL.Parameters.AddWithValue("@Name", Login_user.Name);
                                       insertSQL.Parameters.AddWithValue("@SurName", Login_user.Surname);
                                       insertSQL.Parameters.AddWithValue("@NickName", Login_user.Login1);
                                       insertSQL.Parameters.AddWithValue("@Permissions", plCBpermissions);
                                       insertSQL.Parameters.AddWithValue("@Password", Login_user.Password_log);
                                       try
                                       {
                                          insertSQL.ExecuteNonQuery();
                                       }
                                       catch (Exception ex)
                                       {
                                           throw new Exception(ex.Message);
                                       }
                                    MessageBox.Show("zarejestrowano");
                                    }
                               }
                        }
                    }
                }
                conn.Close();
            }
        this.DialogResult = true;
        }

    }
}
