using FluentValidation.Results;
using System.Collections.Generic;

namespace ExampleService.Data.Interfaces
{
    public interface IServiceValidationConverter
    {
        Dictionary<string, List<string>> ConvertToDictionaryFormat(ValidationResult results);
    }
}
