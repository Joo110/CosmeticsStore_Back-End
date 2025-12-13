using FluentValidation;
using CosmeticsStore.Domain.Interfaces.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CosmeticsStore.Infrastructure.Auth.Jwt
{
    public static class AuthConfiguration
    {
        public static IServiceCollection AddAuthInfrastructure(
            this IServiceCollection services)
        {
            // Register Validator
            services.AddScoped<IValidator<JwtAuthConfig>, JwtAuthConfigValidator>();

            // Configure JWT Options
            services.AddOptions<JwtAuthConfig>()
                .BindConfiguration(nameof(JwtAuthConfig))
                .ValidateDataAnnotations()
                .ValidateOnStart();

            // Register JWT Bearer Options Configurator
            services.AddSingleton<IConfigureOptions<JwtBearerOptions>, ConfigureJwtBearerOptions>();

            // Configure Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer();

            // Register Token Generator
            services.AddTransient<IJwtTokenGenerator, JwtTokenGenerator>();

            return services;
        }
    }

    // Class منفصل لتكوين JwtBearer
    internal class ConfigureJwtBearerOptions : IConfigureNamedOptions<JwtBearerOptions>
    {
        private readonly JwtAuthConfig _config;

        public ConfigureJwtBearerOptions(IOptions<JwtAuthConfig> config)
        {
            _config = config.Value;
        }

        public void Configure(string? name, JwtBearerOptions options)
        {
            if (name == JwtBearerDefaults.AuthenticationScheme)
            {
                Configure(options);
            }
        }

        public void Configure(JwtBearerOptions options)
        {
            var key = Encoding.UTF8.GetBytes(_config.Key);

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _config.Issuer,
                ValidAudience = _config.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ClockSkew = TimeSpan.Zero
            };

            options.SaveToken = true;
            options.RequireHttpsMetadata = false; // للتطوير فقط - اجعلها true في Production
        }
    }
}