using CosmeticsStore.Domain.Interfaces.Persistence.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Linq;

namespace CosmeticsStore.Infrastructure.Persistence.Services.Images
{
    public static class ImageServiceConfiguration
    {
        public static IServiceCollection AddImageService(this IServiceCollection services)
        {
            services.AddScoped<IValidator<FirebaseConfig>, FireBaseConfigValidator>();

            services.AddOptions<FirebaseConfig>()
                .BindConfiguration(nameof(FirebaseConfig));

            services.AddSingleton<IValidateOptions<FirebaseConfig>, FluentValidateOptions<FirebaseConfig>>();

            services.AddScoped<IImageService, FirebaseImageService>();

            return services;
        }
    }

    public class FluentValidateOptions<TOptions> : IValidateOptions<TOptions>
        where TOptions : class
    {
        private readonly IServiceProvider _serviceProvider;

        public FluentValidateOptions(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ValidateOptionsResult Validate(string? name, TOptions options)
        {
            ArgumentNullException.ThrowIfNull(options);

            using var scope = _serviceProvider.CreateScope();
            var validator = scope.ServiceProvider.GetRequiredService<IValidator<TOptions>>();
            var result = validator.Validate(options);

            if (result.IsValid)
                return ValidateOptionsResult.Success;

            var errors = result.Errors.Select(e => $"Property {e.PropertyName}: {e.ErrorMessage}");
            return ValidateOptionsResult.Fail(errors);
        }
    }
}
