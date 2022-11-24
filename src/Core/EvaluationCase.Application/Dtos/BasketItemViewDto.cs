using EvaluationCase.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationCase.Application.Dtos
{
    public class BasketItemViewDto 
    {
        public ProductViewDto Product { get; set; }
        public int Quantity { get; set; }
    }
}
