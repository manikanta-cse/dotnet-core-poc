using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using quote_cloud_poc.service.Data.Opportunity.Model;

namespace quote_cloud_poc.service.Data.Opportunity
{
    internal class InMemoryOpportunityRepository : IOpportunityRepository
    {
        private readonly IList<OpportunityEntity> _internalRepository = new List<OpportunityEntity>();
        
        public OpportunityEntity Get(string id)
        {
            return _internalRepository.SingleOrDefault(o => o.Id.Equals(id));
        }

        public IList<OpportunityEntity> GetAll()
        {
            return _internalRepository.ToList();
        }

        public void Remove(OpportunityEntity entity)
        {
            if (_internalRepository.Any(o => o.Id == entity.Id))
            { _internalRepository.Remove(_internalRepository.First(o => o.Id.Equals(entity.Id))); }
        }

        public void Save(OpportunityEntity entity)
        {
            Remove(entity);
            _internalRepository.Add(entity);
        }

        public void Remove(string id)
        {
            var entity = Get(id);
            _internalRepository.Remove(entity);
        }
    }
}
