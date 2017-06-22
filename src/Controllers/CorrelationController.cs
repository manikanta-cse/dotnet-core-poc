using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using quote_cloud_poc.Middleware.Correlation;

namespace quote_cloud_poc.Controllers
{
    // Using this controller to test correlation identifier middleware
    [Route("api/correlation")]
    public class CorrelationController : Controller
    {
        // GET api/opportunity/{id}
        [HttpGet("")]
        public IActionResult Get()
        {
            return Ok(new {CorrelationId = CorrelationManager.GetCorrelationId()});
        }
    }
}
