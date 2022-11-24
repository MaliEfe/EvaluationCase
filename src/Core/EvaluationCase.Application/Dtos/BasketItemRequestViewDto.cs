using EvaluationCase.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationCase.Application.Dtos
{
    public class BasketItemRequestViewDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
