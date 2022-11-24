using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationCase.Domain.Entities
{
    public class BasketItem
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
