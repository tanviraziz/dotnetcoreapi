using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMTSalesApi.Model
{
    public class Product
    {
        public string ID { get; set; }
        public int Position { get; set; }
        public string Name { get; set; }
        public string Generic { get; set; }
        public int Quantity { get; set; }
        public string DailyCallPromotionID { get; set; }
        public string Condition { get; set; } = "1";
    }
}
