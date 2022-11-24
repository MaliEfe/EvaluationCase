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

namespace EvaluationCase.Application.Features.Commands.UpdateCampaign
{
    public class UpdateCampaignCommand : IRequest<ServiceResponse<CampaignViewDto>>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public decimal MinimumBasketPrice { get; set; }
        public decimal Discount { get; set; }
        public CampaignType CampaignType { get; set; }
        public CampaignDiscountType DiscountType { get; set; }
        public int AffectedCount { get; set; } = 1;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
