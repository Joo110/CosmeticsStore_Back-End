using CosmeticsStore.Dtos.User;
using FluentValidation;

namespace CosmeticsStore.Validators.User
{
    public class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
    {
        public UpdateUserRequestValidator()
        {
            RuleFor(x => x.FullName)
                .MaximumLength(250).WithMessage(UserValidationMessages.FullNameTooLong)
                .When(x => !string.IsNullOrWhiteSpace(x.FullName));

            RuleFor(x => x.PhoneNumber)
                .Matches(@"^\+?\d{7,15}$")
                .When(x => !string.IsNullOrWhiteSpace(x.PhoneNumber))
                .WithMessage(UserValidationMessages.PhoneInvalid);

            RuleFor(x => x.Password)
                .MinimumLength(8).WithMessage(UserValidationMessages.PasswordWeak)
                .Matches(@"(?=.*[A-Za-z])(?=.*\d)").WithMessage(UserValidationMessages.PasswordWeak)
                .When(x => !string.IsNullOrWhiteSpace(x.Password));

            RuleFor(x => x.Roles)
                .Must(roles => roles == null || roles.All(r => !string.IsNullOrWhiteSpace(r) && r.Length <= 100))
                .WithMessage(UserValidationMessages.RoleInvalid);
        }
    }
}