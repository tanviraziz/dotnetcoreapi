using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IMTSalesApi.Model
{
    public class Customer
    {
		public String Condition { get; set; } = "1";
		public String CustomerID { get; set; } 
		public String CTypeID { get; set; } 
		public String Code { get; set; } 
		public String Name { get; set; } 
		public String Address { get; set; } 
		public String ContactPerson { get; set; } 
		public String PhoneNo { get; set; } 
		public String AltPhoneNo { get; set; } 
		public String WhatsappNo { get; set; } 
		public String Email { get; set; } 
		public String Qualification { get; set; } 
		public String Speciality { get; set; } 
		public String BaseTerrID { get; set; } 
		public String SalePerMonth { get; set; } 
		public String PatientPerDay { get; set; } 
		public String ProdPrescribed { get; set; } 
		public String Latitude { get; set; } 
		public String Longitude { get; set; }
		public String UID { get; set; } = null;
		
	}
}
