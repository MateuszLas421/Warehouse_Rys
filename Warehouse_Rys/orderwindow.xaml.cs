using System;
using System.Collections.Generic;
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
using System.Collections.ObjectModel;
using static Warehouse_Rys.ProgramWindowin;

namespace Warehouse_Rys
{
    /// <summary>
    /// Interaction logic for orderwindow.xaml
    /// </summary>
    public partial class orderwindow : Window
    {
        private List<string> listname =new List<string>();
        Window_result a = null;
        internal orderwindow(ObservableCollection<Quantity> quantityProducts1,Window_result _Result)
        {
            a = _Result;
            InitializeComponent();
            for (int i = 0; i < quantityProducts1.Count(); i++)
            {
                listname.Add(quantityProducts1[i].Name);
            }
            openprogram();
        }

        public List<string> Listname { get => listname; set => listname = value; }

        private void openprogram()
        {
            CBox.ItemsSource = Listname;
        }
        private void PtwBT_Click(object sender, RoutedEventArgs e)
        {
            a.Result = CBox.Text.ToString();
            try
            {
                a.Order = int.Parse(ordervalue.Text);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.ToString());
                a.Result = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                a.Result = null;
            }
            this.DialogResult = true;
        }
    }
}
