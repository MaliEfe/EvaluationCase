using MediatR;
using EvaluationCase.Application.Dtos;
using EvaluationCase.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationCase.Application.Features.Queries.EvaluateCampaigns
{
    public class EvaluateCampaignQuery : IRequest<ServiceResponse<BasketEvaluateReponseViewDto>>
    {
        public Guid BasketId { get; set; }
    }
}
