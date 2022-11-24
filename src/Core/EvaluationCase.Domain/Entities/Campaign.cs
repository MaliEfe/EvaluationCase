using EvaluationCase.Domain.Common;
using EvaluationCase.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationCase.Domain.Entities
{
    public class Campaign : BaseEntity
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public decimal MinimumBasketPrice { get; set; } = 0;
        public decimal Discount { get; set; }
        public CampaignType CampaignType { get; set; }
        public CampaignDiscountType DiscountType { get; set; }
        public List<string> CampaignTypeValue { get; set; }
        public int AffectedCount { get; set; } = 1;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}
