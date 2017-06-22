using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace quote_cloud_poc.Controllers.Models
{
    public class Opportunity
    {
        public string Id { get; set; }
        public string StoreId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}