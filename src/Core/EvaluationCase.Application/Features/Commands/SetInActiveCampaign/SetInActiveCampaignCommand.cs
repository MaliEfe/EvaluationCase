using AutoMapper;
using MediatR;
using EvaluationCase.Application.Dtos;
using EvaluationCase.Application.Features.Commands.CreateProduct;
using EvaluationCase.Application.Interfaces.Repositories;
using EvaluationCase.Application.Wrappers;
using EvaluationCase.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EvaluationCase.Application.Features.Commands.SetInActiveCampaign
{
    public class SetInActiveCampaignCommand : IRequest<ServiceResponse<Guid>>
    {
        public Guid Id { get; set; }
    }
}
