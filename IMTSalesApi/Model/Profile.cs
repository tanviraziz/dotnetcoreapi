using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMTSalesApi.Model
{
    public class Profile
    {
        public String Condition { get; set; } = "1";
        public String SFID { get; set; }
        public String Empid { get; set; }
        public String Name { get; set; }
        public String Gender { get; set; }
        public String Qualification { get; set; }
        public String PresentAddress { get; set; }
        public String PermanentAddress { get; set; }
        public String Email { get; set; }
        public String PhoneNo { get; set; }
        public String Designation { get; set; }
        public String MPOCode { get; set; }
        public String Territory { get; set; }
        public String TrCode { get; set; }
        public String JoinDate { get; set; }
    }
}
