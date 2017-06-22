namespace quote_cloud_poc.service.Data.Opportunity.Model
{
    /// <summary>
    /// Immutable
    /// </summary>
    public class CustomerRecord
    {
        public CustomerRecord(string id, string source)
        {
            Id = id;
            Source = source;
        }

        public string Id { get; }
        public string Source { get; }
    }
}
