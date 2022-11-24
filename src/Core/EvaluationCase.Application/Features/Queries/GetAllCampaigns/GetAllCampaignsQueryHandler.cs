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
using EvaluationCase.Application.Features.Queries.GetAllCampaigns;
using StackExchange.Redis.Extensions.Core.Abstractions;

namespace EvaluationCase.Application.Features.Queries.GetAllCampaigns
{
    public class GetAllCampaignsQueryHandler : IRequestHandler<GetAllCampaignsQuery, ServiceResponse<List<CampaignViewDto>>>
    {
        private readonly ICampaignRepository campaignRepository;
        private readonly IMapper mapper;
        private readonly ICacheStore cacheStore;
        const string cacheKey = "CAMPAIGNS";

        public GetAllCampaignsQueryHandler(ICampaignRepository campaignRepository, IMapper mapper, ICacheStore cacheStore)
        {
            this.campaignRepository = campaignRepository;
            this.mapper = mapper;
            this.cacheStore = cacheStore;
        }


        public async Task<ServiceResponse<List<CampaignViewDto>>> Handle(GetAllCampaignsQuery request, CancellationToken cancellationToken)
        {
            var cachedData = await cacheStore.GetAsync<List<CampaignViewDto>>(cacheKey);
            if (cachedData != null)
                return new ServiceResponse<List<CampaignViewDto>>(cachedData);

            var campaigns = await campaignRepository.GetAllAsync();

            var dto = mapper.Map<List<CampaignViewDto>>(campaigns);
            await cacheStore.AddAsync(dto, cacheKey, TimeSpan.FromMinutes(5));

            return new ServiceResponse<List<CampaignViewDto>>(dto);
        }
    }
}
