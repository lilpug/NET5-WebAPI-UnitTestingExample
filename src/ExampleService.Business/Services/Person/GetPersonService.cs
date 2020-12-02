using ExampleService.Business.Validators.Person;
using ExampleService.Data;
using ExampleService.Data.Interfaces;
using ExampleService.Data.Requests.Person;
using ExampleService.Data.Responses;
using ExampleService.Data.Responses.Person;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using IValidatorFactory = ExampleService.Data.Interfaces.IValidatorFactory;

namespace ExampleService.Business.Services.Person
{
    public class GetPersonService : IService
    {   
        private readonly IValidatorFactory _validatorFactory;
        private readonly IPersonRepository _personRepository;
        private readonly IServiceValidationConverter _serviceValidationConverter;
        private readonly GetPersonRequest _request;        

        public GetPersonService(IValidatorFactory validatorFactory, 
                                IPersonRepository personRepository, 
                                IServiceValidationConverter serviceValidationConverter,
                                IRequest request)            
        {   
            _validatorFactory = validatorFactory;
            _personRepository = personRepository;
            _serviceValidationConverter = serviceValidationConverter;
            _request = request as GetPersonRequest;
        }

        protected async Task<Dictionary<string, List<string>>> ServiceValidation()
        {
            //Attempts to get the validator from the factory using the request type
            var tempValidator = _validatorFactory.GetValidator(_request);

            //Checks the validator is the correct version
            if (tempValidator is GetPersonRequestValidator validator)
            {
                //Runs our validation
                var results = await validator.ValidateAsync(_request);

                //Checks if it was invalid
                if (!results.IsValid)
                {
                    return _serviceValidationConverter.ConvertToDictionaryFormat(results);
                }
                return null;
            }
            else
            {
                throw new Exception("The validator could not be found for the GetPersonService.");
            }
        }

        public async Task<ServiceResult> ProcessAsync()
        {
            ServiceResult serviceResult = new ServiceResult();
            Dictionary<string, List<string>> validationResults = null;

            try
            {
                validationResults = await ServiceValidation();
                if(validationResults != null && validationResults.Count > 0)
                {
                    throw new ValidationException("There has been a validation error");
                }

                //Pulls out the person from the repository
                Data.Person person = await _personRepository.GetPersonAsync(_request.PersonID.Value);

                //builds our response and status code output
                serviceResult.Response = new GetPersonResponse() { Successful = true, Person = person };
                serviceResult.StatusCode = HttpStatusCode.OK;
            }
            catch(ValidationException)
            {
                serviceResult.Response = new ValidationErrorResponse() { Successful = false, ValidationErrors = validationResults };
                serviceResult.StatusCode = HttpStatusCode.BadRequest;
            }
            catch(Exception)
            {
                serviceResult.Response = new ErrorResponse() { Successful = false, Message = "There has been an error." };
                serviceResult.StatusCode = HttpStatusCode.InternalServerError;
            }
            finally
            {
                validationResults = null;
            }

            return serviceResult;
        }
    }
}