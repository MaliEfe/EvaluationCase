using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationCase.Domain.Entities
{
    public class BasketEvaluate
    {
        public string ProductCode { get; set; }
        public decimal Price { get; set; }
        public DiscountResponse Discount { get; set; }
    }
}
