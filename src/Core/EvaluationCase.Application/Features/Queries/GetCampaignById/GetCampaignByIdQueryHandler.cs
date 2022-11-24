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

namespace EvaluationCase.Application.Features.Queries.GetCampaignById
{
    public class GetCampaignByIdQueryHandler : IRequestHandler<GetCampaignByIdQuery, ServiceResponse<CampaignViewDto>>
    {
        ICampaignRepository campaignRepository;
        private readonly IMapper mapper;
        private readonly ICacheStore cacheStore;

        public GetCampaignByIdQueryHandler(ICampaignRepository campaignRepository, IMapper mapper, ICacheStore cacheStore)
        {
            this.campaignRepository = campaignRepository;
            this.mapper = mapper;
            this.cacheStore = cacheStore;
        }

        public async Task<ServiceResponse<CampaignViewDto>> Handle(GetCampaignByIdQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = $"CAMPAIGN_{request.Id}";

            var cachedData = await cacheStore.GetAsync<CampaignViewDto>(cacheKey);
            if (cachedData != null)
                return new ServiceResponse<CampaignViewDto>(cachedData);

            var campaign = await campaignRepository.GetByIdAsync(request.Id);
            var dto = mapper.Map<CampaignViewDto>(campaign);

            await cacheStore.AddAsync(dto, cacheKey, TimeSpan.FromMinutes(5));

            return new ServiceResponse<CampaignViewDto>(dto);
        }
    }
}
