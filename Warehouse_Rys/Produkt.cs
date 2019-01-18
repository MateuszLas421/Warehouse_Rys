using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse_Rys
{
    class Produkt
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string EAN { get; set; }
        public string Supplier_ID { get; set; }
        public string Quantity_ID { get; set; }

        public Produkt(int iD, string name, string eAN, string supplier_ID, string quantity_ID)
        {
            ID = iD;
            Name = name;
            EAN = eAN;
            Supplier_ID = supplier_ID;
            Quantity_ID = quantity_ID;
        }
        public Produkt(int iD, string name, string eAN)
        {
            ID = iD;
            Name = name;
            EAN = eAN;
        }
        public Produkt( string name, string eAN)
        {
            Name = name;
            EAN = eAN;
        }
    }
}
