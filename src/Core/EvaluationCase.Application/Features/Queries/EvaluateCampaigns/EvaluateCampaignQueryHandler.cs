using AutoMapper;
using MediatR;
using EvaluationCase.Application.Dtos;
using EvaluationCase.Application.Interfaces.Repositories;
using EvaluationCase.Application.Wrappers;
using System.Threading.Tasks;
using System.Threading;
using EvaluationCase.Application.Services;

namespace EvaluationCase.Application.Features.Queries.EvaluateCampaigns
{
    public class EvaluateCampaignQueryHandler : IRequestHandler<EvaluateCampaignQuery, ServiceResponse<BasketEvaluateReponseViewDto>>
    {
        private readonly CampaignEvaluateService CampaignEvaluateService;
        private readonly IMapper mapper;

        public EvaluateCampaignQueryHandler(IMapper mapper, CampaignEvaluateService CampaignEvaluateService)
        {
            this.mapper = mapper;
            this.CampaignEvaluateService = CampaignEvaluateService;
        }


        public async Task<ServiceResponse<BasketEvaluateReponseViewDto>> Handle(EvaluateCampaignQuery request, CancellationToken cancellationToken)
        {
            var basketEvaluate = await CampaignEvaluateService.CampaignEvaluate(request.BasketId);
            var result = mapper.Map<BasketEvaluateReponseViewDto>(basketEvaluate);

            return new ServiceResponse<BasketEvaluateReponseViewDto>(result);
        }
    }
}
