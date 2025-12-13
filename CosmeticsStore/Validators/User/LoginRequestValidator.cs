using FluentValidation;

namespace CosmeticsStore.Validators.User
{
    public class LoginRequestValidator : AbstractValidator<CosmeticsStore.Dtos.User.LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage(UserValidationMessages.EmailRequired)
                .EmailAddress().WithMessage(UserValidationMessages.EmailInvalid);

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(UserValidationMessages.PasswordRequired);
            // no complexity check here — only presence required for login
        }
    }
}
