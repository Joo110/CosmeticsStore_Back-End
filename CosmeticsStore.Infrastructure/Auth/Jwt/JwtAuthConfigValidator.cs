using System;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.Infrastructure.Auth.Jwt
{
    public class JwtAuthConfigValidator : AbstractValidator<JwtAuthConfig>
    {
        public JwtAuthConfigValidator()
        {
            RuleFor(x => x.Key)
              .NotEmpty();

            RuleFor(x => x.Issuer)
              .NotEmpty();

            RuleFor(x => x.Audience)
              .NotEmpty();

            RuleFor(x => x.LifetimeMinutes)
              .NotEmpty()
              .GreaterThan(0);
        }
    }
}
