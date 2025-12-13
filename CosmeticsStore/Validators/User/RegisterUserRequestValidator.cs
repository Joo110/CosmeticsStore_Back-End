using CosmeticsStore.Dtos.User;
using FluentValidation;

namespace CosmeticsStore.Validators.User
{
    public class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
    {
        public RegisterUserRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(UserValidationMessages.EmailRequired)
                .EmailAddress().WithMessage(UserValidationMessages.EmailInvalid);

            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage(UserValidationMessages.FullNameRequired)
                .MaximumLength(250).WithMessage(UserValidationMessages.FullNameTooLong);

            RuleFor(x => x.PhoneNumber)
                .Matches(@"^\+?\d{7,15}$")
                .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber))
                .WithMessage(UserValidationMessages.PhoneInvalid);

            // Password required and must be strong
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(UserValidationMessages.PasswordRequired)
                .MinimumLength(8).WithMessage(UserValidationMessages.PasswordWeak)
                .Matches(@"(?=.*[A-Za-z])(?=.*\d)").WithMessage(UserValidationMessages.PasswordWeak);

            RuleFor(x => x.Roles)
                .Must(roles => roles == null || roles.All(r => !string.IsNullOrWhiteSpace(r) && r.Length <= 100))
                .WithMessage(UserValidationMessages.RoleInvalid);
        }
    }
}
