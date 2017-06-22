using System;
using System.Collections.Generic;
using System.Text;

namespace quote_cloud_poc.service.Health
{
    public interface IHealthCheckService
    {
        HealthCheckResponse PerformHealthCheck();
    }
}
