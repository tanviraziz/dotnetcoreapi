using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IMTCMS.DAL.DAO;
using IMTSalesApi.BLL;
using IMTSalesApi.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace IMTSalesApi.Controllers
{
    [Route("ibm/kenya/[controller]")]
    [ApiController]
    public class SalesorderController : ControllerBase
    {
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
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
                var dt = new SalesOrderManager().Orders("1", "1", condition);

                JSONString = JsonConvert.SerializeObject(dt);
                return Ok(JSONString);
            }
            catch
            {
                return null;
            }
        }

        // POST: api/Attendance
        [HttpPost]
        public ActionResult Post(SalesOrder sale)
        {
            try
            {
                string isSaved = new SalesOrderManager().SalesOrderManagement(sale);

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
