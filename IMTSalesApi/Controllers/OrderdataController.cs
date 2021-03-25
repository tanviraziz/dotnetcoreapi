using System;
using System.Data;
using IMTSalesApi.BLL;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IMTSalesApi.Controllers
{
    [Route("ibm/kenya/[controller]")]
    [ApiController]
    public class OrderdataController : ControllerBase
    {
        // GET: ibm/kenya/Orderdata/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            DataTable dt = null;
            try
            {
                return getData(id);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private IActionResult getData(string choice)
        {
            //DataTable dt1, dt2 = null;
            string JSONString = string.Empty;

            try
            {
                var data = new
                {
                    Customer = new CustomerManager().CustomerData("1", choice, null),
                    Product = new ProductManager().ProductData("1",choice, null)
                };

                //dt = new CustomerManager().CustomerData("1", choice, condition);
                JSONString = JsonConvert.SerializeObject(data, Formatting.Indented);
                return Ok(JSONString);
            }
            catch
            {
                return null;
            }   
        }
    }
}
