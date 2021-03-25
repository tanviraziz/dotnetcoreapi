using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using IMTSalesApi.BLL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IMTSalesApi.Controllers
{
    [Route("ibm/kenya/[controller]")]
    [ApiController]
    public class DailycalldataController : ControllerBase
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

        private IActionResult getData(string condition)
        {
            string JSONString = string.Empty;
            try
            {
                var data = new
                {
                    Doctor = new DailycalldataManager().DailycallData("1", "1", condition),
                    Product = new DailycalldataManager().DailycallData("1", "2", condition),
                    Feedback = new DailycalldataManager().DailycallData("1", "3", condition)
                };

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
