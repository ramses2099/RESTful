using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessEntities
{
    public class ProductEntity
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int ProductCategoryID { get; set; }
        public decimal UnitPrice { get; set; }
        public string Description { get; set; }
        public bool Discontinued { get; set; }
        public int Stocks { get; set; }
        public string CreationUser { get; set; }
        public System.DateTime CreationDateTime { get; set; }
        public string LastUpdateUser { get; set; }
        public Nullable<System.DateTime> LastUpdateDateTime { get; set; }
        public byte[] Timestamp { get; set; }

    }
}
