using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace quote_cloud_poc.Middleware.Correlation
{
    public class CorrelationMiddleware
    {
        private const string CorrelationHeader = "Logging-CorrelationId";
        private readonly RequestDelegate _next;

        public CorrelationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context)
        {
            string correlationId = context.Request.Headers[CorrelationHeader];

            correlationId = string.IsNullOrEmpty(correlationId) ? CorrelationGenerator.Generate() : correlationId;

            CorrelationManager.SetCorrelationId(correlationId);
            
            // Call the next delegate/middleware in the pipeline
            return this._next(context);
        }
    }
}
