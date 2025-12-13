using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CosmeticsStore.SharedKernel.OptionsValidation
{
    public static class OptionsBuilderExtensions
    {
        public static OptionsBuilder<TOptions> ValidateFluentValidation<TOptions>(
          this OptionsBuilder<TOptions> builder)
          where TOptions : class
        {
            builder.Services.AddSingleton<IValidateOptions<TOptions>, FluentValidateOptions<TOptions>>();

            return builder;
        }
    }
}
