using CosmeticsStore.Dtos.User;
using FluentValidation;

namespace CosmeticsStore.Validators.User
{
    public class AddUserRequestValidator : AbstractValidator<AddUserRequest>
    {
        public AddUserRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(UserValidationMessages.EmailRequired)
                .EmailAddress().WithMessage(UserValidationMessages.EmailInvalid);

            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage(UserValidationMessages.FullNameRequired)
                .MaximumLength(250).WithMessage(UserValidationMessages.FullNameTooLong);

            // Phone optional but must be valid if provided
            RuleFor(x => x.PhoneNumber)
                .Matches(@"^\+?\d{7,15}$")
                .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber))
                .WithMessage(UserValidationMessages.PhoneInvalid);

            // Password optional (admin-created user), but if provided must be strong enough
            RuleFor(x => x.Password)
                .MinimumLength(8).WithMessage(UserValidationMessages.PasswordWeak)
                .Matches(@"(?=.*[A-Za-z])(?=.*\d)").WithMessage(UserValidationMessages.PasswordWeak)
                .When(x => !string.IsNullOrWhiteSpace(x.Password));

            // Roles optional: if provided each role should be non-empty and reasonably short
            RuleFor(x => x.Roles)
                .Must(roles => roles == null || roles.All(r => !string.IsNullOrWhiteSpace(r) && r.Length <= 100))
                .WithMessage(UserValidationMessages.RoleInvalid);
        }
    }
}
