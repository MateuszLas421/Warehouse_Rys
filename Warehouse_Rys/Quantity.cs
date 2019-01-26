using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse_Rys
{
    class Quantity : Produkt
    {
        public Quantity(int iD, string name, string eAN, int v, int id) : base(iD, name, eAN)
        {
            QuantityProdukt = v;
            QuantityID = id;
        }
        public  int QuantityID { get; set; }
        public int QuantityProdukt { get; set; }
    }
}
