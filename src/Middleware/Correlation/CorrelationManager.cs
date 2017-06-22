using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace quote_cloud_poc.Middleware.Correlation
{
    /// <summary>
    /// Responsible for managing the CorrelationId for the Logical Execution Thread
    /// </summary>
    public static class CorrelationManager
    {
        private const string CorrelationKey = "Thread.Current.CorrelationId";
        private static readonly AsyncLocal<string> _currentCorrelationIdentifier = new AsyncLocal<string>();

        /// <summary>
        /// Sets the correlationId for the current logical execution thread
        /// </summary>
        /// <param name="id">CorrelationId</param>
        public static void SetCorrelationId(string id)
        {
            _currentCorrelationIdentifier.Value = id;
        }

        /// <summary>
        /// Gets the correlationId for the current logical execution thread
        /// </summary>
        public static string GetCorrelationId()
        {
            return _currentCorrelationIdentifier.Value;
        }
        /// <summary>
        /// Clears the CorrelationId from the internal ContextStore
        /// </summary>
        public static void ClearCorrelationId()
        {
            _currentCorrelationIdentifier.Value = null;
        }
    }
}
