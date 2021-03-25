using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMTSalesApi.Model
{
    public class SalesOrder
    {
        public string OrderID { get; set; }
        public string Type { get; set; }
        public string CustomerID { get; set; }
        public string Customer { get; set; }
        public string TenderNo { get; set; }
        public string MPOID { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public string Condition { get; set; } = "1";
    }
}
