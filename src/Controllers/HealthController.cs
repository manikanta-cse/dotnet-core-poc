using System;
using Microsoft.AspNetCore.Mvc;
using quote_cloud_poc.service.Health;

namespace quote_cloud_poc.Controllers
{
    [Route("health")]
    public class HealthController : Controller
    {
        private readonly IHealthCheckService _healthService;

        public HealthController(IHealthCheckService healthService)
        {
            _healthService = healthService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_healthService.PerformHealthCheck());
        }
    }
}
