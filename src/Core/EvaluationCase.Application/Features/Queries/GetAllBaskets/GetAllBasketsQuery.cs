using MediatR;
using EvaluationCase.Application.Dtos;
using EvaluationCase.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationCase.Application.Features.Queries.GetAllBaskets
{
    public class GetAllBasketsQuery : IRequest<ServiceResponse<List<BasketViewDto>>>
    {
    }
}
