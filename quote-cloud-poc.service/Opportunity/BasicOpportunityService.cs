using System;
using System.Collections.Generic;
using System.Text;
using quote_cloud_poc.service.Data.Opportunity;
using quote_cloud_poc.service.Data.Opportunity.Model;

namespace quote_cloud_poc.service.Opportunity
{
    internal class BasicOpportunityService : IOpportunityService
    {
        private readonly IOpportunityRepository _repository;

        public BasicOpportunityService(IOpportunityRepository repository)
        {
            _repository = repository;
        }

        public OpportunityEntity Get(string id)
        {
            return _repository.Get(id);
        }

        public IList<OpportunityEntity> GetAll()
        {
            return _repository.GetAll();
        }

        public void Remove(OpportunityEntity entity)
        {
            Remove(entity);
        }

        public void Remove(string id)
        {
            _repository.Remove(id);
        }

        public void Save(OpportunityEntity entity)
        {
            _repository.Save(entity);
        }
    }
}
