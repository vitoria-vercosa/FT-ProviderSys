using FluentValidation;

namespace FT_ProviderSys.DTOs.Validators
{
    public class IEnumerableIdsRequestDTOValidator : AbstractValidator<IEnumerableIdsRequestDTO>
    {
        public IEnumerableIdsRequestDTOValidator()
        {
            RuleFor(x => x.Ids)
                .NotNull()
                .NotEmpty()
                .ForEach(item => item.GreaterThan(0));
        }
    }
}
