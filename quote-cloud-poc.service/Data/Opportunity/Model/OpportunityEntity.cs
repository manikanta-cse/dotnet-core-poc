using System;
using System.Collections.Generic;

namespace quote_cloud_poc.service.Data.Opportunity.Model
{
    public class OpportunityEntity
    {
        // The immutable fields should be passed in via constructor

            // OpportunityID needs to be more defined than an GUID
        public OpportunityEntity(string id, DateTime createdDate)
        {
            CreatedDate = createdDate;
            Id = id;
        }

        /// <summary>
        /// Unique identifier for this instance of an Opportunity
        /// </summary>
        public string Id { get; } // The id should never be updatable via the model

        /// <summary>
        /// The CDK storeId that this opportunity belongs to
        /// </summary>
        public string StoreId { get; set; }

        /// <summary>
        /// The source system for lead information
        /// </summary>
        public string LeadSourceSystem { get; set; }
        /// <summary>
        /// The identifier relating to a lead in the <see cref="LeadSourceSystem"/>
        /// </summary>
        public string LeadIdentifier { get; set; }
        /// <summary>
        /// The application which crated the Opportunity
        /// </summary>
        public string SourceApplication { get; set; }

        #region Audit information
        /// <summary>
        /// The date the Opportunity started
        /// </summary>
        public DateTime CreatedDate { get; } // The created date should never be updatable via the model 
        public DateTime LastModifiedDate { get; set; }
        public string LastModifiedBy { get; set; }

        #endregion

        #region Related Entities

        public IList<CustomerRecord> Customers { get; set; }

        public IList<string> QuoteIds { get; set; }

        public IList<string> VehicleIds { get; set; }

        public IList<ExternalIdentifier> ExternalIdentifiers { get; set; }

        #endregion
    }
}
