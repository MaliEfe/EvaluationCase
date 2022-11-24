using MediatR;
using EvaluationCase.Application.Dtos;
using EvaluationCase.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationCase.Application.Features.Queries.GetProductById
{
    public class GetProductByIdQuery : IRequest<ServiceResponse<GetProductByIdViewModel>>
    {
        public Guid Id { get; set; }




    }
}
