using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationCase.Application.Dtos
{
    public class BasketEvaluateViewDto
    {
        public string ProductCode { get; set; }
        public decimal Price { get; set; }
        public DiscountResponseDto Discount { get; set; }
    }
}
