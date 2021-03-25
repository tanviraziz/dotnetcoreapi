using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMTSalesApi.Model
{
    public class TourPlan
    {
        public String ID { get; set; }
        public String SFID { get; set; }

        public String TourPlanID { get; set; }
        public String TourDate { get; set; }
        public virtual ICollection<TourTask> Tours { get; set; }

        public String Condition { get; set; } = "1";
    }

    public class TourTask
    {
        public String ID { get; set; }
        public String Name { get; set; }
        public String CustomerID { get; set; }
        public String VisitTime { get; set; }
        public bool DoctorTask { get; set; }
        public String Condition { get; set; } = "1";
    }
}
