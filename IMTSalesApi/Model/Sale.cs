using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace IMTCMS.DAL.DAO
{
    public class Sale
    {
        //Field
        private string condition = "1";

        //Property
        public string InvoiceID { get; set; }
        public string SaleOrdID { get; set; }
        public string SFTerrID { get; set; }
        public string InvoiceNO { get; set; }
        public string SaleDate { get; set; }
        public string CustomerID { get; set; }
        public string PCTypeID { get; set; }
        public string Condition
        {
            get { return condition; }
            set { condition = value; }
        }
        public virtual ICollection<SaleDetail> SaleDetail { get; set; }
    }

    public class SaleDetail
    {
        //Fields
        private string condition = "1";

        //Properties
        public string InvoiceDetID { get; set; }
        public string SaleDetID { get; set; }
        public string ID { get; set; }
        public string ProdCode { get; set; }
        [Required(ErrorMessage = "CIF required")]
        public string CIF { get; set; }
        public string BD_CIF { get; set; }
        public string USD_CIF { get; set; }
        [Required(ErrorMessage = "Quantity required")]
        public string Quantity { get; set; }
        public string Ordered { get; set; }

        [StringLength(500, ErrorMessage = "Remarks must be in 500 characters")]
        public string Remarks { get; set; }

        public string Condition
        {
            get { return condition; }
            set { condition = value; }
        }


        public virtual ICollection<ShipDetail> ShipDetail { get; set; }
        public virtual Sale sale { get; set; }
    }
}