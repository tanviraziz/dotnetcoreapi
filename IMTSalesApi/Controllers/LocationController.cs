using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using IMTSalesApi.BLL;
using IMTSalesApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IMTSalesApi.Controllers
{
    [Route("ibm/kenya/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {  
        // GET: api/Location/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            DataTable dt = null;
            try
            {
                return getData("1",id);
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
                    dt = new LocationManager().LocationData("1", choice,null);
                    JSONString = JsonConvert.SerializeObject(dt);
                    return Ok(JSONString);
            }
            return null;
        }

        // POST: api/Location
        [HttpPost]        
        public ActionResult<Location> Post(Location location)
        {
            try
            {
                //return Ok(location);

                string isSaved = new LocationManager().LocationManagement(location);

                if (isSaved.Trim() == "1")
                    return Ok("1");
                else
                    return Ok(isSaved.Trim());
            }
            catch (Exception ex)
            {
                return Ok(ex.ToString());
                //return BadRequest();
            }

            //return BadRequest();
        }

        // PUT: api/Location/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
