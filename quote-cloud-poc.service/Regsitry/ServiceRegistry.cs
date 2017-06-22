using System;
using System.Collections.Generic;
using System.Text;
using quote_cloud_poc.service.Data.Opportunity;
using quote_cloud_poc.service.Health;
using quote_cloud_poc.service.Opportunity;
using StructureMap;

namespace quote_cloud_poc.service.Regsitry
{
    public class ServiceRegistry : Registry
    {
        public ServiceRegistry()
        {
            ForSingletonOf<IHealthCheckService>().Use<BasicHealthCheckService>();
            ForSingletonOf<IOpportunityRepository>().Use<InMemoryOpportunityRepository>();
            For<IOpportunityService>().Use<BasicOpportunityService>();
        }
    }
}
