using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;

namespace quote_cloud_poc.Middleware.Correlation
{
    public static class CorrelationExtensions
    {
        public static void UseCorrelationIdentifier(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<CorrelationMiddleware>();
        }
    }
}
