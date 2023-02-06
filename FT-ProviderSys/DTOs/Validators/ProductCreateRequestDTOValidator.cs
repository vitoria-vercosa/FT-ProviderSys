using FluentValidation;
using FT_ProviderSys.DTOs;

namespace FT_ProductSys.DTOs.Validators
{
    public class ProductCreateRequestDTOValidator : AbstractValidator<ProductCreateRequestDTO>
    {
        public ProductCreateRequestDTOValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty()
                .NotNull()
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
