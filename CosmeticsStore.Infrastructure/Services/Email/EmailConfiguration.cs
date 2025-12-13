using CosmeticsStore.Domain.Interfaces.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace CosmeticsStore.Infrastructure.Services.Email
{
    public static class EmailConfiguration
    {
        public static IServiceCollection AddEmailInfrastructure(this IServiceCollection services)
        {
            // تسجيل Validator
            services.AddScoped<IValidator<EmailConfig>, EmailConfigValidator>();

            // تسجيل الخيارات
            services.AddOptions<EmailConfig>()
                .BindConfiguration(nameof(EmailConfig));

            // التحقق يدوياً من صلاحية الإعدادات عند بناء الـ service provider
            services.AddTransient<IEmailService>(sp =>
            {
                // جلب IOptions<EmailConfig>
                var options = sp.GetRequiredService<IOptions<EmailConfig>>();

                // التحقق من القيم الفعلية
                var validator = sp.GetRequiredService<IValidator<EmailConfig>>();
                var result = validator.Validate(options.Value);

                if (!result.IsValid)
                {
                    throw new ValidationException(result.Errors);
                }

                // تمرير IOptions<EmailConfig> إلى الخدمة
                return new EmailService(options);
            });

            return services;
        }
    }
}
