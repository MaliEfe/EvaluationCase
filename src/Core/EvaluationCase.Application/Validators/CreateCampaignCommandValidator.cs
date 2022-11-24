using FluentValidation;
using EvaluationCase.Application.Features.Commands.AddBasket;
using EvaluationCase.Application.Features.Commands.CreateCampaign;
using EvaluationCase.Application.Features.Commands.CreateProduct;
using System;

namespace EvaluationCase.Application.Validators
{
    public class CreateCampaignCommandValidator : AbstractValidator<CreateCampaignCommand>
    {
        public CreateCampaignCommandValidator()
        {
            RuleFor(s => s.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("Campaign Name cannot be null or empty");
            RuleFor(s => s.Discount)
                .NotEmpty()
                .NotNull()
                .WithMessage("Discount cannot be null or empty")
                .GreaterThan(0).WithMessage("Discount must greater than 0.");
            RuleFor(s => s.AffectedCount)
               .NotEmpty()
               .NotNull()
               .WithMessage("AffectedCount cannot be null or empty")
               .GreaterThan(0).WithMessage("AffectedCount must greater than 0.");
            RuleFor(s => (int)s.CampaignType)
              .NotNull()
              .WithMessage("CampaignType cannot be null or empty");
            RuleFor(s => (int)s.DiscountType)
              .NotNull()
              .WithMessage("DiscountType cannot be null or empty");
            RuleFor(s => s.EndDate)
               .NotEmpty().WithMessage("End Date can not empty.")
               .GreaterThan(DateTime.Now).WithMessage("End Date can not be before now");

        }
    }
}
