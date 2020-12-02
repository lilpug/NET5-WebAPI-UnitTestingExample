using ExampleService.Business.Services.Person;
using ExampleService.Data.Enums;
using ExampleService.Data.Interfaces;


namespace ExampleService.Business.Factories
{
    public class PersonServiceFactory : IPersonServiceFactory
    {   
        private readonly IValidatorFactory _validatorFactory;
        private readonly IPersonRepository _personRepository;
        private readonly IServiceValidationConverter _serviceValidationConverter;

        public PersonServiceFactory(IValidatorFactory validatorFactory, 
                                    IPersonRepository personRepository,
                                    IServiceValidationConverter serviceValidationConverter)
        {
            _validatorFactory = validatorFactory;
            _personRepository = personRepository;
            _serviceValidationConverter = serviceValidationConverter;
        }

        public IService GetService(PersonServiceType service, IRequest request)
        {
            return service switch
            {
                PersonServiceType.GetPerson => new GetPersonService(_validatorFactory, _personRepository, _serviceValidationConverter, request),
                _ => null
            };
        }
    }
}
