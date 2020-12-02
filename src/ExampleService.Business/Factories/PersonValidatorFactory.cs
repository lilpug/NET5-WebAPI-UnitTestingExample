using ExampleService.Business.Validators.Person;
using ExampleService.Data.Interfaces;
using ExampleService.Data.Requests.Person;
using FluentValidation;
using IValidatorFactory = ExampleService.Data.Interfaces.IValidatorFactory;

namespace ExampleService.Business.Factories
{
    public class PersonValidatorFactory : IValidatorFactory
    {
        public IValidator GetValidator(IRequest request)
        {
            return request switch
            {
                GetPersonRequest _ => new GetPersonRequestValidator(),                
                _ => null
            };
        }
    }
}