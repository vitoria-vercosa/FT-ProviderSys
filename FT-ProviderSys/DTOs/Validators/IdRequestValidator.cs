using FluentValidation;

namespace FT_ProviderSys.DTOs.Validators
{
    public class IdRequestValidator : AbstractValidator<int>
    {
        public IdRequestValidator()
        {
            RuleFor(x => x).NotNull().NotEmpty().GreaterThan(0);
        }
    }
}
