using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMTSalesApi.Model
{
    public class Location
    {
        public String Condition { get; set; } = "1";
        public String ID { get; set; }
        public String Longitude { get; set; }
        public String Latitude { get; set; }
        public String MoveTime { get; set; }
    }
}
