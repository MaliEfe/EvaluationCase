using AutoMapper;
using MediatR;
using EvaluationCase.Application.Interfaces.Repositories;
using EvaluationCase.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using StackExchange.Redis.Extensions.Core.Abstractions;

namespace EvaluationCase.Application.Features.Commands.CreateProduct
{
    public class CreateCampaignCommandHandler : IRequestHandler<CreateProductCommand, ServiceResponse<Guid>>
    {
        IProductRepository productRepository;
        private readonly IMapper mapper;
        private readonly ICacheStore cacheStore;
        const string cacheKey = "PRODUCTS";

        public CreateCampaignCommandHandler(IProductRepository productRepository, IMapper mapper, ICacheStore cacheStore)
        {
            this.productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            this.mapper = mapper;
            this.cacheStore = cacheStore;
        }

        public async Task<ServiceResponse<Guid>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = mapper.Map<Domain.Entities.Product>(request);
            await productRepository.CreateAsync(product);

            await cacheStore.RemoveAsync(new[] { cacheKey });

            return new ServiceResponse<Guid>(product.Id);
        }
    }
}
