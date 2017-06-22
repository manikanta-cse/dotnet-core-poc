using System;
using System.Collections.Generic;
using System.Text;
using quote_cloud_poc.service.Data.Opportunity.Model;

namespace quote_cloud_poc.service.Data.Opportunity
{
    internal interface IOpportunityRepository
    {
        OpportunityEntity Get(string id);

        IList<OpportunityEntity> GetAll();

        void Remove(OpportunityEntity entity);

        void Save(OpportunityEntity entity);

        void Remove(string id);
    }
}
