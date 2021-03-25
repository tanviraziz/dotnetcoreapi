using System;
using System.Data;
using IMTSalesApi.BLL;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IMTSalesApi.Controllers
{
    [Route("ibm/kenya/[controller]")]
    [ApiController]
    public class TourplandataController : ControllerBase
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
                    Chemist = new TourplandataManager().TourplanData("1", "1", condition),
                    Doctor = new TourplandataManager().TourplanData("1", "2", condition),
                    MPO = new TourplandataManager().TourplanData("1", "3", condition)
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
