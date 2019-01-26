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
        private bool BDQOPEN;
        public string result;

        public bool BDQOPEN1 { get => BDQOPEN; set => BDQOPEN = value; }
        internal ObservableCollection<Quantity> QuantityProducts1 { get; set; }
        internal ObservableCollection<Quantity> OrderProducts { get; set; }
        Window_result _Result = new Window_result();
        public ProgramWindowin()
        {
            InitializeComponent();
            OpenWindow_Program();
            BDQOPEN1 = false;
        }

        private void OpenWindow_Program()
        {
            QuantityProducts1 = null;
            QuantityProducts1 = new ObservableCollection<Quantity>();
            OrderProducts = null;
            OrderProducts=new ObservableCollection<Quantity>();
            DataStany.ItemsSource = QuantityProducts1;
            DataOrder.ItemsSource = OrderProducts;
        }

        private void update()
        {
            BDQOPEN1 = false;
            OpenWindow_Program();
            BaseOpen();
        }

        #region <<Open Menu>>
        private void UpdateBase_Click(object sender, RoutedEventArgs e)
        {
            update();
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
            btnZamow.Visibility = Visibility.Hidden;
        }

        private void MenuItem_ListaStany(object sender, RoutedEventArgs e)
        {
            windowStartStackPanel.Visibility = Visibility.Hidden;
            DataStany.Visibility = Visibility.Visible;
            DataOrder.Visibility = Visibility.Hidden;
            btnAdd.Visibility = Visibility.Hidden;
            btnsearch.Visibility = Visibility.Visible;
            searchbox.Visibility = Visibility.Visible;
            btnZamow.Visibility = Visibility.Hidden;
            if (BDQOPEN1 == false) BaseOpen();
        }

        private void MenuItem_Zamowienie(object sender, RoutedEventArgs e)
        {
            windowStartStackPanel.Visibility = Visibility.Hidden;
            DataStany.Visibility = Visibility.Hidden;
            DataOrder.Visibility = Visibility.Visible;
            btnAdd.Visibility = Visibility.Visible;
            btnsearch.Visibility = Visibility.Visible;
            searchbox.Visibility = Visibility.Visible;
            btnZamow.Visibility = Visibility.Hidden;
        }
        #endregion

        #region <<Base>>
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
                string strsql = "Select Products.ID, Name, EAN, Quantity_Product, Products.Quantity_ID from Products join QUANTITY on Products.Quantity_ID=QUANTITY.ID";
                using (SQLiteCommand cmd = new SQLiteCommand(strsql, conn))
                {
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            Quantity a = new Quantity(rdr.GetInt32(0), rdr.GetString(1), rdr.GetString(2), rdr.GetInt32(3), rdr.GetInt32(4));
                            QuantityProducts1.Add(a);
                        }
                    }
                }
                conn.Close();
            }
            BDQOPEN1 = true;
        }
        #endregion

        private void workinprogress()
        {
            MessageBox.Show("work in progress");
        }
        /*private bool FiltrUzytkownika(object item)
        {
            if (String.IsNullOrEmpty(txtFilter.Text))
                return true;
            else
                return ((item as Produkt).Nazwa.IndexOf(txtFilter.Text,
                    StringComparison.OrdinalIgnoreCase) >= 0);
        }*/

        private void clicBtnAdd(object sender, RoutedEventArgs e)
        {
            var orderWindow = new orderwindow(QuantityProducts1,_Result);
            orderWindow.ShowDialog();
            for (int i = 0; i < QuantityProducts1.Count(); i++)
            {
                if (QuantityProducts1[i].Name == _Result.Result)
                {
                    OrderProducts.Add(QuantityProducts1[i]);
                    int a = OrderProducts.Count();
                    OrderProducts[a-1].QuantityProdukt += _Result.Order;
                }
            }
            if(btnZamow.Visibility==Visibility.Hidden) btnZamow.Visibility = Visibility.Visible;
        }

        private void clicBtnOrder(object sender, RoutedEventArgs e)
        {
            if (BDQOPEN1 == false) BaseOpen();
            Order order = new Order();
            order.Orderload(OrderProducts);
            update();
        }
    }
}
