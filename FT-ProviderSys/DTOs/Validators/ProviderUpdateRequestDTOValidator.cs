using FluentValidation;

namespace FT_ProviderSys.DTOs.Validators
{
    public class ProviderUpdateRequestDTOValidator : AbstractValidator<ProviderUpdateRequestDTO>
    {
        public ProviderUpdateRequestDTOValidator()
        {
            RuleFor(x => x.ProviderId)
                .NotNull()
                .NotEmpty()
                .WithMessage("The 'Provider Id' cannot be null or empty.");

            RuleFor(x => x.CorporateName)
                .NotNull()
                .NotEmpty()
                .WithMessage("The 'Corporate Name' cannot be null or empty.");

            RuleFor(x => x.ContactEmail)
                .NotEmpty()
                .NotNull()
                .WithMessage("The 'Contact Email' cannot be null or empty.")
                .EmailAddress()
                .WithMessage("The 'Contact Email' is invalid.");

            RuleFor(x => x.State)
                .NotNull()
                .NotEmpty()
                .WithMessage("The 'State' cannot be null or empty.")
                .MaximumLength(2)
                .WithMessage("The 'State' must be less than or equal to 2 characters.");

            RuleFor(x => x.ContactName)
                .NotNull()
                .NotEmpty()
                .WithMessage("The 'Contact Name' cannot be null or empty.")
                .MaximumLength(50)
                .WithMessage("The 'Contact Name' cannot be greater than 50 characters.");

            RuleFor(x => x.LegalEntityIdentifier)
                .NotNull()
                .NotEmpty()
                .MaximumLength(50)
                .WithMessage("The 'Legal Entity Identifier' cannot be greater than 50 characters.");
        }
    }
}
