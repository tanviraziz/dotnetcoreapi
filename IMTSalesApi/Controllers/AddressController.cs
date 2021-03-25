using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using IMTSalesApi.BLL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IMTSalesApi.Controllers
{
    [Route("ibm/kenya/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        // GET: api/Location/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            DataTable dt = null;
            try
            {
                return getData("1", id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private IActionResult getData(string analytic, string choice)
        {
            DataTable dt = null;
            string JSONString = string.Empty;
            switch (analytic)
            {
                case "1":
                    dt = new AddressManager().AddressData("1", choice, null);
                    dt = ConvertDT(dt);
                    JSONString = JsonConvert.SerializeObject(dt);
                    return Ok(JSONString);
            }
            return null;
        }

        private DataTable ConvertDT(DataTable dt)
        {
            string currAddress = null;
            DataColumn newColumn = new DataColumn("address", typeof(System.String));
            newColumn.DefaultValue = "";
            dt.Columns.Add(newColumn);

            if (dt != null && dt.Rows.Count> 0)
            {
                foreach(DataRow dr in dt.Rows)
                {
                    try
                    {
                        
                        currAddress = getAddress(Convert.ToDouble(dr[1].ToString()), Convert.ToDouble(dr[2].ToString()));
                    }
                    catch(Exception ex) { }

                    dr["address"] = currAddress;
                }
            }

            return dt;
        }

        public string getAddress(double lat, double lon)
        {
            WebClient webClient = new WebClient();
            webClient.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");
            webClient.Headers.Add("Referer", "http://www.microsoft.com");
            var jsonData = webClient.DownloadData("http://nominatim.openstreetmap.org/reverse?format=json&lat=" + lat + "&lon=" + lon);
            DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(RootObject));
            RootObject rootObject = (RootObject)ser.ReadObject(new MemoryStream(jsonData));
            return rootObject.display_name;
        }
    }

    
    public class RootObject
    {        
        public string place_id { get; set; }        
        public string licence { get; set; }        
        public string osm_type { get; set; }        
        public string osm_id { get; set; }        
        public string lat { get; set; }        
        public string lon { get; set; }        
        public string display_name { get; set; }        
        public Address address { get; set; }   
    }

    
    public class Address
    {        
        public string road { get; set; }        
        public string suburb { get; set; }        
        public string city { get; set; }        
        public string state_district { get; set; }        
        public string state { get; set; }        
        public string postcode { get; set; }        
        public string country { get; set; }     
        public string country_code { get; set; }
    }
}
