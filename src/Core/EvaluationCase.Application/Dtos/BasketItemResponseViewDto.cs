using EvaluationCase.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationCase.Application.Dtos
{
    public class BasketItemResponseViewDto 
    {
        public List<BasketItem> BasketItems { get; set; }
    }
}
