using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace quote_cloud_poc.Middleware.Correlation
{
    public static class CorrelationGenerator
    {
        /// <summary>
        /// Generates an identifier
        /// </summary>
        public static string Generate()
        {
            return Guid.NewGuid().ToString("N");
        }
        /// <summary>
        /// Generates an identifier using the seed value
        /// </summary>
        public static string Generate(string seed)
        {
            return string.Format("{0}_{1:N}", seed, Guid.NewGuid());
        }
    }
}
