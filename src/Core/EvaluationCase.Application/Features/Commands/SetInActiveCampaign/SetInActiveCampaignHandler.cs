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

namespace EvaluationCase.Application.Features.Commands.SetInActiveCampaign
{
    public class SetInActiveCampaignHandler : IRequestHandler<SetInActiveCampaignCommand, ServiceResponse<Guid>>
    {
        ICampaignRepository campaignRepository;
        private readonly IMapper mapper;
        private readonly ICacheStore cacheStore;

        public SetInActiveCampaignHandler(ICampaignRepository campaignRepository, IMapper mapper, ICacheStore cacheStore)
        {
            this.campaignRepository = campaignRepository ?? throw new ArgumentNullException(nameof(campaignRepository));
            this.mapper = mapper;
            this.cacheStore = cacheStore;
        }

        public async Task<ServiceResponse<Guid>> Handle(SetInActiveCampaignCommand request, CancellationToken cancellationToken)
        {
            var campaign = await campaignRepository.SetInactiveCampaignAsync(request.Id);
            await cacheStore.RemoveAsync(new[] { "CAMPAIGNS", $"CAMPAIGN_{request.Id}" });

            return new ServiceResponse<Guid>(campaign.Id);
        }
    }
}
