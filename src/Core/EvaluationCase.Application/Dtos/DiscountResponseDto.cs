using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationCase.Application.Dtos
{
    public class DiscountResponseDto 
    {
        public decimal DiscountPrice { get; set; }
        public string CampaignId { get; set; }
        public decimal DiscountedPrice { get; set; }
    }
}
