using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmeticsStore.SharedKernel.OptionsValidation
{
    public static class ValidationMessages
    {
        public static string GenerateValidationFailureMessages(string optionsType, string propertyName, string errorMessages)
        {
            return $"Fluent validation failed for '{optionsType}.{propertyName}' with the error: '{errorMessages}'.";
        }
    }
}
