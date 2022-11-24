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

namespace EvaluationCase.Application.Features.Commands.UpdateCampaign
{
    public class UpdateCampaignCommandHandler : IRequestHandler<UpdateCampaignCommand, ServiceResponse<CampaignViewDto>>
    {
        ICampaignRepository campaignRepository;
        private readonly IMapper mapper;
        private readonly ICacheStore cacheStore;

        public UpdateCampaignCommandHandler(ICampaignRepository campaignRepository, IMapper mapper, ICacheStore cacheStore)
        {
            this.campaignRepository = campaignRepository ?? throw new ArgumentNullException(nameof(campaignRepository));
            this.mapper = mapper;
            this.cacheStore = cacheStore;
        }

        public async Task<ServiceResponse<CampaignViewDto>> Handle(UpdateCampaignCommand request, CancellationToken cancellationToken)
        {
            var campaign = mapper.Map<Domain.Entities.Campaign>(request);
            await campaignRepository.UpdateAsync(campaign);
            var result = mapper.Map<CampaignViewDto>(campaign);

            await cacheStore.RemoveAsync(new[] { "CAMPAIGNS", $"CAMPAIGN_{request.Id}" });

            return new ServiceResponse<CampaignViewDto>(result);
        }
    }
}
