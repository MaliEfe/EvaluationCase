using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationCase.Domain.Entities
{
    public class BasketEvaluateReponse
    {
        public List<BasketEvaluate> Items { get; set; }
        public decimal TotalDiscountedPrice { get; set; } = 0;
    }
}
