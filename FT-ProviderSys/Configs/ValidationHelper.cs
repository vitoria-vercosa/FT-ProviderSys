using FluentValidation;
using FluentValidation.Results;

namespace FT_ProviderSys.Configs
{
    public class ValidationHelper : IValidationHelper
    {
        public Task<bool> ValidateAsync<TValidator, T>(T command, string exceptionMessage, CancellationToken cancellationToken)
            where TValidator : AbstractValidator<T>
            where T : class
        {
            return ValidateAsync<TValidator, T>(command, true, exceptionMessage, cancellationToken);
        }

        public Task<bool> ValidateAsync<TValidator, T>(T command, bool throwIfFailed, CancellationToken cancellationToken)
            where TValidator : AbstractValidator<T>
            where T : class
        {
            return ValidateAsync<TValidator, T>(command, throwIfFailed, "Validation Failed", cancellationToken);
        }

        public Task<bool> ValidateAsync<TValidator, T>(T command, CancellationToken cancellationToken)
            where TValidator : AbstractValidator<T>
            where T : class
        {
            return ValidateAsync<TValidator, T>(command, true, "Validation Failed", cancellationToken);
        }
        public Task<bool> ValidateAsync<TValidator, T>(T command)
            where TValidator : AbstractValidator<T>
            where T : class
        {
            return ValidateAsync<TValidator, T>(command, true, "Validation Failed", CancellationToken.None);
        }

        public bool Validate<TValidator, T>(T command, bool throwIfFailed)
            where TValidator : AbstractValidator<T>
            where T : class
        {
            return Validate<TValidator, T>(command, throwIfFailed, "Validation Failed");
        }

        public bool Validate<TValidator, T>(T command)
            where TValidator : AbstractValidator<T>
            where T : class
        {
            return Validate<TValidator, T>(command, true, "Validation Failed");
        }

        public async Task<bool> ValidateAsync<TValidator, T>(T command, bool throwIfFailed, string exceptionMessage, CancellationToken cancellationToken)
            where TValidator : AbstractValidator<T>
            where T : class
        {
            if (command is null)
                throw new ArgumentNullException(nameof(command));

            if (exceptionMessage is null)
                throw new ArgumentNullException(nameof(exceptionMessage));

            AbstractValidator<T> validator = Activator.CreateInstance<TValidator>();
            ValidationResult validationResult = await validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid && throwIfFailed)
                throw new ValidationException(exceptionMessage, validationResult.Errors);

            return validationResult.IsValid;
        }

        public bool Validate<TValidator, T>(T command, bool throwIfFailed, string exceptionMessage)
            where TValidator : AbstractValidator<T>
            where T : class
        {
            if (command is null)
                throw new ArgumentNullException(nameof(command));

            if (exceptionMessage is null)
                throw new ArgumentNullException(nameof(exceptionMessage));

            AbstractValidator<T> validator = Activator.CreateInstance<TValidator>();
            ValidationResult validationResult = validator.Validate(command);
            if (!validationResult.IsValid && throwIfFailed)
                throw new ValidationException(exceptionMessage, validationResult.Errors);

            return validationResult.IsValid;
        }
    }
}
