using EvaluationCase.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationCase.Application.Dtos
{
    public class CampaignViewDto 
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public decimal MinimumBasketPrice { get; set; }
        public decimal Discount { get; set; }
        public CampaignType CampaignType { get; set; }
        public CampaignDiscountType DiscountType { get; set; }
        public List<string> CampaignTypeValue { get; set; }
        public int AffectedCount { get; set; } = 1;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
