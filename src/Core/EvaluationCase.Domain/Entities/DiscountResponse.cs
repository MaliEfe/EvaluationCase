using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationCase.Domain.Entities
{
    public class DiscountResponse
    {
        public decimal DiscountPrice { get; set; }
        public string CampaignId { get; set; }
        public decimal DiscountedPrice { get; set; }
    }
}
