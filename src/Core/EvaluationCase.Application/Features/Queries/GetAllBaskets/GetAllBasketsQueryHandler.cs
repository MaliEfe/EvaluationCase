using AutoMapper;
using MediatR;
using EvaluationCase.Application.Dtos;
using EvaluationCase.Application.Features.Queries.GetAllCampaigns;
using EvaluationCase.Application.Interfaces.Repositories;
using EvaluationCase.Application.Wrappers;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace EvaluationCase.Application.Features.Queries.GetAllBaskets
{
    public class GetAllBasketsQueryHandler : IRequestHandler<GetAllBasketsQuery, ServiceResponse<List<BasketViewDto>>>
    {
        private readonly IBasketRepository basketRepository;
        private readonly IMapper mapper;
        private readonly ICacheStore cacheStore;
        const string cacheKey = "BASKETS";

        public GetAllBasketsQueryHandler(IBasketRepository basketRepository, IMapper mapper, ICacheStore cacheStore)
        {
            this.basketRepository = basketRepository;
            this.mapper = mapper;
            this.cacheStore = cacheStore;
        }


        public async Task<ServiceResponse<List<BasketViewDto>>> Handle(GetAllBasketsQuery request, CancellationToken cancellationToken)
        {
            var cachedData = await cacheStore.GetAsync<List<BasketViewDto>>(cacheKey);
            if (cachedData != null)
                return new ServiceResponse<List<BasketViewDto>>(cachedData);

            var baskets = await basketRepository.GetAllAsync();

            var dto = mapper.Map<List<BasketViewDto>>(baskets);
            await cacheStore.AddAsync(dto, cacheKey, TimeSpan.FromMinutes(5));

            return new ServiceResponse<List<BasketViewDto>>(dto);
        }
    }
}
