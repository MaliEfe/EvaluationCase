using AutoMapper;
using MediatR;
using EvaluationCase.Application.Interfaces.Repositories;
using EvaluationCase.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using EvaluationCase.Application.Dtos;
using StackExchange.Redis.Extensions.Core.Abstractions;

namespace EvaluationCase.Application.Features.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, ServiceResponse<ProductViewDto>>
    {
        IProductRepository productRepository;
        private readonly IMapper mapper;
        private readonly ICacheStore cacheStore;

        public UpdateProductCommandHandler(IProductRepository productRepository, IMapper mapper, ICacheStore cacheStore)
        {
            this.productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            this.mapper = mapper;
            this.cacheStore = cacheStore;
        }

        public async Task<ServiceResponse<ProductViewDto>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = mapper.Map<Domain.Entities.Product>(request);
            await productRepository.UpdateAsync(product);
            var result = mapper.Map<ProductViewDto>(product);

            await cacheStore.RemoveAsync(new[] { "PRODUCTS", $"PRODUCT_{request.Id}" });

            return new ServiceResponse<ProductViewDto>(result);
        }
    }
}
