using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using IMTSalesApi.BLL;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IMTSalesApi.Controllers
{
    [Route("ibm/kenya/[controller]")]
    [ApiController]
    public class DashattendanceController : ControllerBase
    {
        // GET: api/<DashattendanceController>
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

        private IActionResult getData(string analytic)
        {
            DataTable dt = null;
            string JSONString = string.Empty;
            try
            {                
                dt = new DashboardManager().AttendanceData(analytic);
                JSONString = JsonConvert.SerializeObject(dt);
                return Ok(JSONString);
            }
            catch (Exception ex){
                return null;
            }           
        }

        // POST api/<DashattendanceController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<DashattendanceController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DashattendanceController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
