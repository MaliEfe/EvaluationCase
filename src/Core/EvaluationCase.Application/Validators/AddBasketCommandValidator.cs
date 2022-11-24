using FluentValidation;
using EvaluationCase.Application.Dtos;
using EvaluationCase.Application.Features.Commands.AddBasket;
using EvaluationCase.Application.Features.Commands.CreateCampaign;
using EvaluationCase.Application.Features.Commands.CreateProduct;
using System;
using System.Linq;

namespace EvaluationCase.Application.Validators
{
    public class AddBasketCommandValidator : AbstractValidator<AddBasketCommand>
    {
        public AddBasketCommandValidator()
        {
            RuleFor(s => s.BasketItems)
                .NotEmpty()
                .NotNull()
                .WithMessage("BasketItems cannot be null or empty");

            RuleForEach(x => x.BasketItems).SetValidator(new BasketItemValidator());
        }
        public class BasketItemValidator : AbstractValidator<BasketItemRequestViewDto>
        {
            public BasketItemValidator()
            {
                RuleFor(k => k.Quantity)
                    .NotEmpty()
                    .WithMessage("Quantity can not empty.")
                    .GreaterThan(0)
                    .WithMessage("Quantity must greater than 0");

                RuleFor(k => k.ProductId)
                    .NotEmpty()
                    .NotNull()
                    .WithMessage("ProductId can not empty.");
            }
        }
    }
}
