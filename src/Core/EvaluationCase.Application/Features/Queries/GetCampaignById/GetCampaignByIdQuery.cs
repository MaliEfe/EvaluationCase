using MediatR;
using EvaluationCase.Application.Dtos;
using EvaluationCase.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Text;

namespace EvaluationCase.Application.Features.Queries.GetCampaignById
{
    public class GetCampaignByIdQuery : IRequest<ServiceResponse<CampaignViewDto>>
    {
        public Guid Id { get; set; }

    }
}
