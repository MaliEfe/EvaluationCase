using AutoMapper;
using MediatR;
using EvaluationCase.Application.Dtos;
using EvaluationCase.Application.Interfaces.Repositories;
using EvaluationCase.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using StackExchange.Redis.Extensions.Core.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace EvaluationCase.Application.Features.Queries.GetAllProducts
{
    public class GetAllCampaignsQueryHandler : IRequestHandler<GetAllProductsQuery, ServiceResponse<List<ProductViewDto>>>
    {
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;
        private readonly ICacheStore cacheStore;
        const string cacheKey = "PRODUCTS";

        public GetAllCampaignsQueryHandler(IProductRepository productRepository, IMapper mapper, ICacheStore cacheStore)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
            this.cacheStore = cacheStore;
        }


        public async Task<ServiceResponse<List<ProductViewDto>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var cachedData = await cacheStore.GetAsync<List<ProductViewDto>>(cacheKey);
            if (cachedData != null)
                return new ServiceResponse<List<ProductViewDto>>(cachedData);

            var products = await productRepository.GetAllAsync();

            var dto = mapper.Map<List<ProductViewDto>>(products);
            await cacheStore.AddAsync(dto, cacheKey, TimeSpan.FromMinutes(5));

            return new ServiceResponse<List<ProductViewDto>>(dto);
        }
    }
}
