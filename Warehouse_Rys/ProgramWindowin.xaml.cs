using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private ObservableCollection<Produkt> QuantityProducts = null;
        public ProgramWindowin()
        {
            InitializeComponent();
            OpenWindow_Program();
        }

        private void OpenWindow_Program()
        {
            QuantityProducts = new ObservableCollection<Produkt>();
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
        }

        private void MenuItem_ListaStany(object sender, RoutedEventArgs e)
        {
            windowStartStackPanel.Visibility = Visibility.Hidden;
            DataStany.Visibility = Visibility.Visible;
        }

        private void MenuItem_Zamowienie(object sender, RoutedEventArgs e)
        {
            windowStartStackPanel.Visibility = Visibility.Hidden;
            DataStany.Visibility = Visibility.Hidden;
        }
    }
}
