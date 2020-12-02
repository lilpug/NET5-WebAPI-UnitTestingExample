using ExampleService.Data.Requests.Person;
using FluentValidation;

namespace ExampleService.Business.Validators.Person
{
    public class GetPersonRequestValidator : BaseValidator<GetPersonRequest>
    {
        public GetPersonRequestValidator()
        {
            RuleFor(x => x.PersonID)
                .Cascade(CascadeMode.Stop)                
                .NotEmpty().WithMessage("PersonID is a required field.")
                .GreaterThan(0).WithMessage("PersonID cannot be zero or below.");
        }
    }
}