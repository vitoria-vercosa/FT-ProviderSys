using FluentValidation;

namespace FT_ProviderSys.DTOs.Validators
{
    public class IdRequestDTOValidator : AbstractValidator<IdRequestDTO>
    {
        public IdRequestDTOValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty()
                .NotNull()
                .GreaterThan(0);
        }
    }
}
