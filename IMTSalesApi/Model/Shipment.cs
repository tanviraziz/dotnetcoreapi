using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IMTCMS.DAL.DAO
{
    public class Shipment
    {
        //Field
        private string condition = "1";

        //Property
        public string ShipID { get; set; }
        public string ShipDate { get; set; }
        public string ReqDate { get; set; }
        public string ShipReqID { get; set; }
        public string PartnerID { get; set; }
        public string CustomerID { get; set; }
        public string ShipPort { get; set; }
        public string ShipDestination { get; set; }
        public string ProfInvoiceNo { get; set; }
        public string ComInvoiceNo { get; set; }
        public string CIFRouteID { get; set; }

        public string Remarks { get; set; }
        public string Condition
        {
            get { return condition; }
            set { condition = value; }
        }
        public virtual ICollection<ShipDetail> ShipDetail { get; set; }
    }

    public class ShipDetail
    {
        //Fields
        private string condition = "1";

        //Properties
        public string ShipDetID { get; set; }
        public string ProdID { get; set; }
        public string ProdCode { get; set; }
        public string PCTypeID { get; set; }
        public string CIF { get; set; }
        public string BD_CIF { get; set; }
        public string USD_CIF { get; set; }
        public string CountryRate { get; set; }
        [Required(ErrorMessage = "Quantity required")]
        public string Quantity { get; set; }
        public string Ordered { get; set; }
        public string BatchNo { get; set; }
        public string MFGDate { get; set; } 
        public string ExpiryDate { get; set; }

        [StringLength(500, ErrorMessage = "Remarks must be in 500 characters")]
        public string Remarks { get; set; }

        public string Condition
        {
            get { return condition; }
            set { condition = value; }
        }

        public virtual Shipment shipment { get; set; }
        //public virtual Sample sample { get; set; }
        //public virtual Promotion promotion { get; set; }
        public virtual SaleDetail saledetail { get; set; }
    }
}
