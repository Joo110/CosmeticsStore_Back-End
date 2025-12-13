using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CosmeticsStore.Infrastructure.Common;

namespace CosmeticsStore.Infrastructure.Persistence.Services.Images
{
    public class FireBaseConfigValidator : AbstractValidator<FirebaseConfig>
    {
        public FireBaseConfigValidator()
        {
            RuleFor(x => x.Bucket)
              .NotEmpty();

            RuleFor(x => x.CredentialsJson)
              .NotEmpty()
              .ValidJson();
        }
    }
}