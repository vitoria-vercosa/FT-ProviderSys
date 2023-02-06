using FluentValidation;

namespace FT_ProviderSys.Configs
{
    public interface IValidationHelper
    {
        Task<bool> ValidateAsync<TValidator, T>(T command, string exceptionMessage, CancellationToken cancellationToken)
            where TValidator : AbstractValidator<T>
            where T : class;

        Task<bool> ValidateAsync<TValidator, T>(T command, bool throwIfFailed, string exceptionMessage, CancellationToken cancellationToken)
            where TValidator : AbstractValidator<T>
            where T : class;
        Task<bool> ValidateAsync<TValidator, T>(T command, bool throwIfFailed, CancellationToken cancellationToken)
            where TValidator : AbstractValidator<T>
            where T : class;
        Task<bool> ValidateAsync<TValidator, T>(T command, CancellationToken cancellationToken)
            where TValidator : AbstractValidator<T>
            where T : class;
        Task<bool> ValidateAsync<TValidator, T>(T command)
            where TValidator : AbstractValidator<T>
            where T : class;
        bool Validate<TValidator, T>(T command, bool throwIfFailed)
            where TValidator : AbstractValidator<T>
            where T : class;
        bool Validate<TValidator, T>(T command, bool throwIfFailed, string exceptionMessage)
            where TValidator : AbstractValidator<T>
            where T : class;
        bool Validate<TValidator, T>(T command)
            where TValidator : AbstractValidator<T>
            where T : class;
    }
}
