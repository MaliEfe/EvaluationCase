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

namespace EvaluationCase.Application.Features.Commands.CreateCampaign
{
    public class UpdateCampaignCommandHandler : IRequestHandler<CreateCampaignCommand, ServiceResponse<Guid>>
    {
        ICampaignRepository campaignRepository;
        private readonly IMapper mapper;
        private readonly ICacheStore cacheStore;
        const string cacheKey = "CAMPAIGNS";

        public UpdateCampaignCommandHandler(ICampaignRepository campaignRepository, IMapper mapper, ICacheStore cacheStore)
        {
            this.campaignRepository = campaignRepository ?? throw new ArgumentNullException(nameof(campaignRepository));
            this.mapper = mapper;
            this.cacheStore = cacheStore;
        }

        public async Task<ServiceResponse<Guid>> Handle(CreateCampaignCommand request, CancellationToken cancellationToken)
        {
            var campaign = mapper.Map<Domain.Entities.Campaign>(request);
            await campaignRepository.CreateAsync(campaign);

            await cacheStore.RemoveAsync(new[] { cacheKey });

            return new ServiceResponse<Guid>(campaign.Id);
        }
    }
}
