using EvaluationCase.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationCase.Application.Dtos
{
    public class BasketViewDto 
    {
        public Guid Id { get; set; }
        public List<BasketItemViewDto> BasketItems { get; set; }
    }
}
