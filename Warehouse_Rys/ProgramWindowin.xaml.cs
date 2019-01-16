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
    /// <summary>
    /// Interaction logic for ProgramWindowin.xaml
    /// </summary>
    public partial class ProgramWindowin : Window
    {
        private ObservableCollection<Quantity> QuantityProducts = null;
        private bool BDQOPEN = false;

        public bool BDQOPEN1 { get => BDQOPEN; set => BDQOPEN = value; }

        public ProgramWindowin()
        {
            InitializeComponent();
            OpenWindow_Program();
        }

        private void OpenWindow_Program()
        {
            QuantityProducts = new ObservableCollection<Quantity>();
            DataStany.ItemsSource = QuantityProducts;
        }

        private void Exit_Click(object sender, RoutedEventArgs e) // zamykanie z menu
        {
            this.Close();
        }

        private void Exit_Click(object sender, EventArgs e)  //zamykanie okna 
        {

        }

        private void MenuItem_Start(object sender, RoutedEventArgs e)
        {
            windowStartStackPanel.Visibility = Visibility.Visible;
            DataStany.Visibility = Visibility.Hidden;
            DataOrder.Visibility = Visibility.Hidden;
        }

        private void MenuItem_ListaStany(object sender, RoutedEventArgs e)
        {
            windowStartStackPanel.Visibility = Visibility.Hidden;
            DataStany.Visibility = Visibility.Visible;
            DataOrder.Visibility = Visibility.Hidden;
            if (BDQOPEN == false) { BaseOpen(); BDQOPEN = true; }
        }

        private void MenuItem_Zamowienie(object sender, RoutedEventArgs e)
        {
            windowStartStackPanel.Visibility = Visibility.Hidden;
            DataStany.Visibility = Visibility.Hidden;
            DataOrder.Visibility = Visibility.Visible;
        }


        private void BaseOpen()
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
                string strsql = "Select Products.ID, Name, EAN, Quantity_Product from Products join QUANTITY on Products.Quantity_ID=QUANTITY.ID";
                using (SQLiteCommand cmd = new SQLiteCommand(strsql, conn))
                {
                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Quantity a = new Quantity(rdr.GetInt32(0),rdr.GetString(1), rdr.GetString(2), rdr.GetInt32(3));
                            QuantityProducts.Add(a);
                            // MessageBox.Show(a.Name.ToString()+" "+a.EAN.ToString()+" "+a.QuantityProdukt.ToString());
                        }
                        //DataStany.ItemsSource = QuantityProducts;
                    }
                }
                conn.Close();
            }
        }
    }
}
