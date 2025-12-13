using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CosmeticsStore.Infrastructure.Common
{
    public static class ValidationExtensions
    {
        public static IRuleBuilderOptions<T, string> ValidJson<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder
              .Must(IsValidJson)
              .WithMessage(ValidationMessages.InvalidJson);
        }

        private static bool IsValidJson(string json)
        {
            try
            {
                _ = JsonDocument.Parse(json);

                return true;
            }
            catch (JsonException)
            {
                return false;
            }
        }
    }
}
