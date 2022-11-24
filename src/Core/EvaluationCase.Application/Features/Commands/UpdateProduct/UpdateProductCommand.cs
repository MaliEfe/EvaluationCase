using AutoMapper;
using MediatR;
using EvaluationCase.Application.Dtos;
using EvaluationCase.Application.Features.Commands.CreateProduct;
using EvaluationCase.Application.Interfaces.Repositories;
using EvaluationCase.Application.Wrappers;
using EvaluationCase.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EvaluationCase.Application.Features.Commands.UpdateProduct
{
    public class UpdateProductCommand : IRequest<ServiceResponse<ProductViewDto>>
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public string BrandCode { get; set; }
        public decimal Price { get; set; }

        public List<string> ProductGroupCodes = new List<string>();
    }
}
