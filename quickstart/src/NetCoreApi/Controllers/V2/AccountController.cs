using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NetCoreApi.Controllers.V2
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
   // [Route("api/[controller]")]
    
    public class AccountController : ControllerBase
    {
        [HttpGet]
        public ActionResult Get()
        {
            return Ok("2.0");
        }
    }
}