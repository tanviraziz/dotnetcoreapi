using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMTSalesApi.Model
{
    public class Attendance
    {
        public String Condition { get; set; } = "1";
        public String ID { get; set; }
        public String UID { get; set; }
        public String StartTime { get; set; }
        public String EndTime { get; set; }
        public String Longitude { get; set; }
        public String Latitude { get; set; }
        
    }
}
