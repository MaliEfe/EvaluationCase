using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationCase.Application.Dtos
{
    public class BasketEvaluateReponseViewDto 
    {
        public List<BasketEvaluateViewDto> Items { get; set; }
        public decimal TotalDiscountedPrice { get; set; }
    }
}
