using IMTSalesApi.BLL;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace IMTSalesApi.Controllers
{
    [Route("ibm/kenya/[controller]")]
    [ApiController]
    public class ReportController : Controller
    {
        // GET: ibm/kenya/Report/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            try
            {
                return getData(id, "1", null); // return all product by list
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // GET: ibm/kenya/Report/5/5
        [HttpGet("{id}/{cond}")]
        public IActionResult Get(string id, string cond)
        {
            try
            {
                return getData(id, "2", cond); // return the product list on specified condition
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private IActionResult getData(string analytic, string choice, string condition)
        {
            DataTable dt = null;
            string JSONString = string.Empty;
            switch (analytic)
            {
                case "1":
                    dt = new ProductManager().ProductData("1", choice, condition);
                    JSONString = JsonConvert.SerializeObject(dt);
                    return Ok(JSONString);
            }
            return null;
        }
    }
}
