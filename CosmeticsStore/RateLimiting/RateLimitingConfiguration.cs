using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using FluentValidation;

namespace CosmeticsStore.RateLimiting;

public static class RateLimitingConfiguration
{
    public static IServiceCollection AddRateLimitingConfiguration(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddOptions<FixedWindowRateLimiterConfig>()
            .BindConfiguration("RateLimiting:FixedWindow")
            .Validate(config =>
            {
                var validator = new FixedWindowRateLimiterConfigValidator();
                var result = validator.Validate(config);
                return result.IsValid;
            }, "Invalid Fixed Window Rate Limiter Configuration");

        services.AddSingleton<IValidator<FixedWindowRateLimiterConfig>,
                              FixedWindowRateLimiterConfigValidator>();

        return services;
    }
}