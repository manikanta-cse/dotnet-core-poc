using System;
using System.Collections.Generic;
using System.Text;

namespace quote_cloud_poc.service.Health
{
    internal class BasicHealthCheckService : IHealthCheckService
    {
        public HealthCheckResponse PerformHealthCheck()
        {
            return new HealthCheckResponse() {Message = "UP"};
        }
    }
}
