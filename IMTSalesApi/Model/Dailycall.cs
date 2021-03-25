using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMTSalesApi.Model
{
    public class Dailycall
    {
        public String DailyCallID { get; set; }
        public String CustomerID { get; set; }
        public String VisitTime { get; set; }
        public String FeedBackID { get; set; }
        public String Feedback { get; set; }
        public String Remarks { get; set; }
        public virtual ICollection<Product> PromotedProduct { get; set; }
        public String Condition { get; set; } = "1";

    }
}
