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
    public class DailycallController : ControllerBase
    {
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
                var dt = new DailyCallManager().Dailycall("1", "1", condition);

                JSONString = JsonConvert.SerializeObject(dt);
                return Ok(JSONString);
            }
            catch
            {
                return null;
            }
        }

        // POST: api/DailyCall
        [HttpPost]
        public ActionResult<Dailycall> Post(Dailycall dailyCall)
        {
            try
            {
                string isSaved = new DailyCallManager().DailyCallManagement(dailyCall);

                if (isSaved.Trim() == "1")
                    return Ok("1");
                else
                    return Ok(isSaved.Trim());
            }
            catch (Exception ex)
            {
                return Ok(ex.ToString());
            }
        }
    }
}
