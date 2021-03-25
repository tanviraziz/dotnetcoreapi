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
    public class UserController : ControllerBase
    {
        //[HttpGet]
        //public IActionResult Get()
        //{
        //    DataTable dt = null;
        //    try
        //    {
        //        return null;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}

        [HttpGet("{id}")]
        public IActionResult get(string id)
        {
            DataTable dt = null;
            try
            {
                return getData("1", null, null, null);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        // GET: api/User/auth object
        [HttpPost]
        public IActionResult Post(Auth auth)
        {
            DataTable dt = null;
            try
            {
                return getData("1", "1",auth.ID,auth.Pass);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private IActionResult getData(string analytic, string choice, string condition1,string condition2)
        {
            DataTable dt = null;
            string JSONString = string.Empty;

            switch (analytic)
            {
                case "1":
                    dt = new UserManager().UserData("1", choice, condition1,condition2);

                    if(dt != null) JSONString = JsonConvert.SerializeObject(dt);
                   
                    return Ok(JSONString);
            }
            return null;
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        
    }
}
