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
    public class SalesForceController : ControllerBase
    {
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            DataTable dt = null;
            try
            {
                return getData(id, "1", null);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpGet("{id}/{choice}/{cond}")]
        public IActionResult Get(string id, string choice, string cond)
        {
            DataTable dt = null;
            try
            {
                return getData(id, choice, cond);
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
                    dt = new SalesForceManager().SalesForceData("1", choice, condition);
                    JSONString = JsonConvert.SerializeObject(dt);
                    return Ok(JSONString);
            }
            return null;
        }
    }
}