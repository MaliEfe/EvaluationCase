using FluentValidation;
using EvaluationCase.Application.Features.Commands.AddBasket;
using EvaluationCase.Application.Features.Commands.CreateProduct;

namespace EvaluationCase.Application.Validators
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(s => s.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("Product Name cannot be null or empty");
            RuleFor(s => s.Code)
                .NotEmpty()
                .NotNull()
                .WithMessage("Code cannot be null or empty");
            RuleFor(s => s.Price)
               .NotEmpty()
               .NotNull()
               .WithMessage("Code cannot be null or empty")
               .GreaterThan(0).WithMessage("Price must greater than 0.");

        }
    }
}
