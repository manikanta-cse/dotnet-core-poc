using System;
using System.Collections.Generic;
using System.Text;

namespace quote_cloud_poc.service.Opportunity
{
    public static class OpportunityIdGenerator
    {
        public static string GenerateId()
        {
            return $"O-{Guid.NewGuid().ToString()}";
        }
    }
}
