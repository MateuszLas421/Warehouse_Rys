using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static Warehouse_Rys.ProgramWindowin;

namespace Warehouse_Rys
{
    class Order
    {
        ObservableCollection<Quantity> a = null;
        internal void Orderload(ObservableCollection<Quantity> OrderProducts)
        {
            a = OrderProducts;
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
                string strsql = "update Quantity set Quantity_Product=:quantity where Id=:id";
                for (int i=0;i<a.Count() ;i++)
                {
                    using (var updatesQLite = new SQLiteCommand(strsql, conn))
                    {
                        updatesQLite.Parameters.Add("quantity", System.Data.DbType.Int32).Value = a[i].QuantityProdukt;
                        updatesQLite.Parameters.Add("id", System.Data.DbType.Int32).Value = a[i].QuantityID;
                        try
                        {
                            updatesQLite.ExecuteNonQuery();
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message);
                        }
                    }
                }
            }
            a.Clear();
        }
    }
}
