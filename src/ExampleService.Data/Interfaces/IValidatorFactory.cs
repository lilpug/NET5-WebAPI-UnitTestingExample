using FluentValidation;

namespace ExampleService.Data.Interfaces
{
    public interface IValidatorFactory
    {
        IValidator GetValidator(IRequest request);
    }
}