using EvaluationCase.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationCase.Domain.Entities
{
    public class Basket : BaseEntity
    {
        public List<BasketItem> BasketItems { get; set; }
    }
}
