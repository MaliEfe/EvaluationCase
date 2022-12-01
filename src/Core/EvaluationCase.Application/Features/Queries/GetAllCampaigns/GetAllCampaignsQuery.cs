 using AutoMapper;
using MediatR;
using EvaluationCase.Application.Dtos;
using EvaluationCase.Application.Interfaces.Repositories;
using EvaluationCase.Application.Wrappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EvaluationCase.Application.Features.Queries.GetAllCampaigns
{
    public class GetAllCampaignsQuery: IRequest<ServiceResponse<List<CampaignViewDto>>>
    {
       
    }
}
