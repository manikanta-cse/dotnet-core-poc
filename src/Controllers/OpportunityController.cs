using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using quote_cloud_poc.Controllers.Models;
using quote_cloud_poc.service.Data.Opportunity.Model;
using quote_cloud_poc.service.Opportunity;

namespace quote_cloud_poc.Controllers
{
    [Route("api/opportunity")]
    public class OpportunityController : Controller
    {
        private readonly IOpportunityService _opportunityService;

        public OpportunityController(IOpportunityService opportunityService)
        {
            _opportunityService = opportunityService;
        }

        // GET api/opportunity/{id}
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var result = _opportunityService.Get(id);

            if (result != null)
            {
                return Ok(result);
            }

            return NotFound(new { message = $"Your ID '{id}' was NOT found." });
        }

        // GET api/opportunity/{id}
        [HttpGet]
        public IActionResult Get()
        {
            var result = _opportunityService.GetAll();

            if (result != null)
            {
                return Ok(result);
            }

            return NotFound(new { message = $"Couldn't get those opportunities." });
        }

        // POST api/opportunity

        // This model should be in the Web and mapped to the service model - but for now
        [HttpPost]
        public IActionResult Post([FromBody]OpportunityEntity opportunity)
        {
            _opportunityService.Save(opportunity);
            return Ok(new { message = $"Let's just pretend like I created that '{opportunity.Id}' thing for you..." });
        }

        // PUT api/opportunity/{id}
        [HttpPut("{id}")]
        public IActionResult Put(string id, [FromBody]OpportunityEntity opportunity)
        {
            _opportunityService.Save(opportunity);
            return Ok(new { message = $"Let's just pretend like I updated that '{id}' thing to '{opportunity.Id}' for you..." });
        }

        // DELETE api/opportunity/{id}
        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            _opportunityService.Remove(id);
            return Ok(new { message = $"Let's just pretend like I deleted that '{id}' thing for you..." });
        }
    }
}
