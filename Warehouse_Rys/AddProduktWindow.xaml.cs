using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

    public partial class AddProduktWindow : Window
    {
        private ObservableCollection<string> ProductsSupplier = null;
        public AddProduktWindow()
        {
            InitializeComponent();
            ProductsSupplier = new ObservableCollection<string>();
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
                string strsql = "Select Name from SupplierTable";
                using (SQLiteCommand cmd = new SQLiteCommand(strsql, conn))
                {
                    using (var rdr = cmd.ExecuteReader())
                    {
                        
                        while (rdr.Read())
                        {
                            string a = null;
                            a = rdr.GetString(0);
                            ProductsSupplier.Add(a); 
                        }
                    }
                }
                conn.Close();
            }
            SupplierComboBox.ItemsSource = ProductsSupplier;
        }

        private void btnPotwierdz_Click(object sender, RoutedEventArgs e)
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
                string strsql = "Select Products.Name,EAN from Products join SupplierTable on SupplierTable.Id=Products.Supplier_ID where Products.Name = '" + 
                    AddNameTextB.Text.ToString() + "' or EAN = '"+ AddEANTextB.Text.ToString()+"'";
                using (SQLiteCommand cmd = new SQLiteCommand(strsql, conn))
                {
                    using (var rdr = cmd.ExecuteReader())
                    {

                        if (rdr.HasRows)
                        {
                            AddNameTextB.BorderBrush = Brushes.Red;
                            AddEANTextB.BorderBrush = Brushes.Red;
                        }
                        else
                        {
                            string AddEANTextB_String=AddEANTextB.Text.ToString();
                            if (13 == AddEANTextB_String.Length || AddEANTextB_String.Length==9)
                            {

                            }
                        }
                    }
                }
                conn.Close();
            }
        }
    }
}
