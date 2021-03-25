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
    public class HospitalController : ControllerBase
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
                    dt = new HospitalManager().HospitalData("1", choice, condition);
                    JSONString = JsonConvert.SerializeObject(dt);
                    return Ok(JSONString);
            }
            return null;
        }

        [HttpPost]
        public ActionResult<Customer> Post(Customer customer)
        {
            try
            {                
                if (customer.CustomerID != null && customer.CustomerID.Trim().Length > 0)
                {
                    customer.Condition = "2";
                }
                else
                {
                    customer.CTypeID = "6";
                }
                    

                bool isSaved = new HospitalManager().HospitalManagement(customer);

                if (isSaved) return Ok("1");
                //return Ok(JsonConvert.SerializeObject(customer));
            }
            catch (Exception ex)
            {
                return BadRequest();
            }

            return BadRequest();
        }
    }
}