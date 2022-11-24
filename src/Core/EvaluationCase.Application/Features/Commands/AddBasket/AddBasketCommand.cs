using MediatR;
using EvaluationCase.Application.Dtos;
using EvaluationCase.Application.Wrappers;
using EvaluationCase.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationCase.Application.Features.Commands.AddBasket
{
    public class AddBasketCommand : IRequest<ServiceResponse<Guid>>
    {
        public List<BasketItemRequestViewDto> BasketItems { get; set; }

    }
}
