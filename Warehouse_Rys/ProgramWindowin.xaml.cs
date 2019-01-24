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
    public partial class ProgramWindowin : Window
    {
        private ObservableCollection<Quantity> QuantityProducts;
        private bool BDQOPEN = false;

        public bool BDQOPEN1 { get => BDQOPEN; set => BDQOPEN = value; }

        public ProgramWindowin()
        {
            InitializeComponent();
            OpenWindow_Program();
        }
 
        private void OpenWindow_Program()
        {
            QuantityProducts = null;
            QuantityProducts = new ObservableCollection<Quantity>();
            DataStany.ItemsSource = QuantityProducts;
        }

        private void Exit_Click(object sender, EventArgs e)  //zamykanie okna 
        {

        }

        #region <<Open Menu>>
        private void UpdateBase_Click(object sender, RoutedEventArgs e)
        {
            BDQOPEN = false;
            OpenWindow_Program();
            BaseOpen();
        }
        private void SettingsMenu_Click(object sender, RoutedEventArgs e)
        {
            workinprogress();
        }

        private void AddNewProductMenu_Click(object sender, RoutedEventArgs e)
        {
            AddProduktWindow okno1 = new AddProduktWindow();
            okno1.ShowDialog();
        }
        private void Exit_Click_Menu(object sender, RoutedEventArgs e) // zamykanie z menu
        {
            this.Close();
        }
        #endregion

        #region <<Menu>>
        private void MenuItem_Start(object sender, RoutedEventArgs e)
        {
            windowStartStackPanel.Visibility = Visibility.Visible;
            DataStany.Visibility = Visibility.Hidden;
            DataOrder.Visibility = Visibility.Hidden;
            btnAdd.Visibility = Visibility.Hidden;
            btnsearch.Visibility = Visibility.Hidden;
            searchbox.Visibility = Visibility.Hidden;
        }

        private void MenuItem_ListaStany(object sender, RoutedEventArgs e)
        {
            windowStartStackPanel.Visibility = Visibility.Hidden;
            DataStany.Visibility = Visibility.Visible;
            DataOrder.Visibility = Visibility.Hidden;
            btnAdd.Visibility = Visibility.Hidden;
            btnsearch.Visibility = Visibility.Visible;
            searchbox.Visibility = Visibility.Visible;
            if (BDQOPEN == false)  BaseOpen();
        }

        private void MenuItem_Zamowienie(object sender, RoutedEventArgs e)
        {
            windowStartStackPanel.Visibility = Visibility.Hidden;
            DataStany.Visibility = Visibility.Hidden;
            DataOrder.Visibility = Visibility.Visible;
            btnAdd.Visibility = Visibility.Visible;
            btnsearch.Visibility = Visibility.Visible;
            searchbox.Visibility = Visibility.Visible;
        }
        #endregion

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
                        }
                    }
                }
                conn.Close();
            }
         BDQOPEN = true;
        }
         private void workinprogress()
        {
            MessageBox.Show("work in progress");
        }
        private void clicBtnAdd(object sender, RoutedEventArgs e)
        {
            workinprogress();
            //AddProduktWindow okno1 = new AddProduktWindow();     
            //okno1.ShowDialog();
        }
    }
}
