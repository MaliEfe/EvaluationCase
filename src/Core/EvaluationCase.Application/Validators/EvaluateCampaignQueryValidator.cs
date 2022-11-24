using FluentValidation;
using EvaluationCase.Application.Features.Commands.AddBasket;
using EvaluationCase.Application.Features.Commands.CreateCampaign;
using EvaluationCase.Application.Features.Commands.CreateProduct;
using EvaluationCase.Application.Features.Queries.EvaluateCampaigns;
using System;

namespace EvaluationCase.Application.Validators
{
    public class EvaluateCampaignQueryValidator : AbstractValidator<EvaluateCampaignQuery>
    {
        public EvaluateCampaignQueryValidator()
        {
            RuleFor(s => s.BasketId)
                .NotEmpty()
                .NotNull()
                .WithMessage("BasketId cannot be null or empty");

        }
    }
}
