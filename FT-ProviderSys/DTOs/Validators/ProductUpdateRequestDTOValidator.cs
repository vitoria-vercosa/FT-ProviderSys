using FluentValidation;

namespace FT_ProviderSys.DTOs.Validators
{
    public class ProductUpdateRequestDTOValidator : AbstractValidator<ProductUpdateRequestDTO>
    {
        public ProductUpdateRequestDTOValidator()
        {
            RuleFor(x => x.ProductId)
                .NotNull()
                .NotEmpty()
                .WithMessage("The 'Product Id' cannot be null or empty.")
                .GreaterThan(0)
                .WithMessage("The 'Product Id' must be valid.");

            RuleFor(x => x.Code)
                .NotEmpty()
                .NotEmpty()
                .WithMessage("The 'Product Code' cannot be null or empty.");

            RuleFor(x => x.ProductName)
                .NotEmpty()
                .NotNull()
                .WithMessage("The 'Product Name' cannot be null or empty.");

            RuleFor(x => x.Description)
                .MaximumLength(250)
                .WithMessage("The 'Product Description' cannot be greater than 250 characters.");

            RuleFor(x => x.Price)
                .NotNull()
                .NotEmpty()
                .WithMessage("The 'Product Price' cannot be null or empty.")
                .GreaterThan(0)
                .WithMessage("The 'Product Price' must be valid.");
        }
    }
}
