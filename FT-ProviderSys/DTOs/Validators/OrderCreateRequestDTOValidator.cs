using FluentValidation;

namespace FT_ProviderSys.DTOs.Validators
{
    public class OrderCreateRequestDTOValidator : AbstractValidator<OrderCreateRequestDTO>
    {
        public OrderCreateRequestDTOValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty()
                .NotNull()
                .WithMessage("The 'Order Code' cannot be null or empty.");

            RuleFor(x => x.ProductIds)
                .NotEmpty()
                .NotNull()
                .WithMessage("The 'Order Products' cannot be null or empty.")
                .ForEach(item => item.GreaterThan(0))
                .WithMessage("The 'Product Ids' must be valids.");

            RuleFor(x => x.ProviderId)
                .NotNull()
                .NotEmpty()
                .WithMessage("The 'Provider Id' cannot be null or empty.")
                .GreaterThan(0)
                .WithMessage("The 'Provider Id' must be valid.");
            /*
            RuleFor(x => x.Amount)
                .NotNull()
                .NotEmpty()
                .WithMessage("The Order Amount cannot be null or empty.")
                .GreaterThan(0)
                .WithMessage("The Order Amount must be valid.");
            */

                
        }
    }
}
