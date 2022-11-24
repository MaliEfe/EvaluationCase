using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using EvaluationCase.Application.Dtos;
using EvaluationCase.Application.Interfaces.Repositories;
using EvaluationCase.Application.Wrappers;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EvaluationCase.Application.Features.Queries.GetProductById
{
    public class GetCampaignByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ServiceResponse<GetProductByIdViewModel>>
    {
        IProductRepository productRepository;
        private readonly IMapper mapper;
        private readonly ICacheStore cacheStore;

        public GetCampaignByIdQueryHandler(IProductRepository productRepository, IMapper mapper, ICacheStore cacheStore)
        {
            this.productRepository = productRepository;
            this.mapper = mapper;
            this.cacheStore = cacheStore;
        }

        public async Task<ServiceResponse<GetProductByIdViewModel>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = $"PRODUCT_{request.Id}";
            var cachedData = await cacheStore.GetAsync<GetProductByIdViewModel>(cacheKey);
            if (cachedData != null)
                return new ServiceResponse<GetProductByIdViewModel>(cachedData);

            var product = await productRepository.GetByIdAsync(request.Id);
            var dto = mapper.Map<GetProductByIdViewModel>(product);

            await cacheStore.AddAsync(dto, cacheKey, TimeSpan.FromMinutes(5));

            return new ServiceResponse<GetProductByIdViewModel>(dto);
        }
    }
}
