using System.Linq;
using System.Threading.Tasks;
using ExampleService.Business.Validators.Person;
using ExampleService.Data.Requests.Person;
using Moq;
using NUnit.Framework;

namespace ExampleService.UnitTests.Business.Validators.Person
{
    public class GetPersonRequestValidatorTests
    {
        [Test]
        public async Task NullRequest()
        {
            GetPersonRequest request = null;
            var validator = new GetPersonRequestValidator();
            var validatorResults = await validator.ValidateAsync(request);

            Assert.IsTrue(!validatorResults.IsValid);
            Assert.IsTrue(validatorResults?.Errors?.Count == 1);
            Assert.IsTrue(validatorResults.Errors.FirstOrDefault(e => e.PropertyName == "Request" && e.ErrorMessage == "The request has not been populated.") != null);
            Assert.IsTrue(validatorResults?.Errors[0].PropertyName == "Request");
        }

        [Test]
        public async Task EmptyRequest()
        {
            var mockRequest = new Mock<GetPersonRequest>();
            var validator = new GetPersonRequestValidator();
            var validatorResults = await validator.ValidateAsync(mockRequest.Object);

            Assert.IsTrue(!validatorResults.IsValid);
            Assert.IsTrue(validatorResults?.Errors?.Count == 1);
            Assert.IsTrue(validatorResults.Errors.FirstOrDefault(e => e.PropertyName == "PersonID" && e.ErrorMessage == "PersonID is a required field.") != null);
        }

        [Test]
        public async Task EmptyPersonIDRequest()
        {
            var request = new GetPersonRequest
            {
                PersonID = null
            };
            var validator = new GetPersonRequestValidator();
            var validatorResults = await validator.ValidateAsync(request);

            Assert.IsTrue(!validatorResults.IsValid);
            Assert.IsTrue(validatorResults?.Errors?.Count == 1);
            Assert.IsTrue(validatorResults.Errors.FirstOrDefault(e => e.PropertyName == "PersonID" && e.ErrorMessage == "PersonID is a required field.") != null);
        }

        [Test]
        public async Task ZeroPersonIDRequest()
        {
            var request = new GetPersonRequest
            {
                PersonID = 0
            };
            var validator = new GetPersonRequestValidator();
            var validatorResults = await validator.ValidateAsync(request);

            Assert.IsTrue(!validatorResults.IsValid);
            Assert.IsTrue(validatorResults?.Errors?.Count == 1);
            Assert.IsTrue(validatorResults.Errors.FirstOrDefault(e => e.PropertyName == "PersonID" && e.ErrorMessage == "PersonID cannot be zero or below.") != null);
        }

        [Test]
        public async Task BelowZeroPersonIDRequest()
        {
            var request = new GetPersonRequest
            {
                PersonID = int.MinValue
            };
            var validator = new GetPersonRequestValidator();
            var validatorResults = await validator.ValidateAsync(request);

            Assert.IsTrue(!validatorResults.IsValid);
            Assert.IsTrue(validatorResults?.Errors?.Count == 1);
            Assert.IsTrue(validatorResults.Errors.FirstOrDefault(e => e.PropertyName == "PersonID" && e.ErrorMessage == "PersonID cannot be zero or below.") != null);
        }

        [Test]
        public async Task SuccessfulRequest()
        {
            var request = new GetPersonRequest
            {
                PersonID = int.MaxValue
            };
            var validator = new GetPersonRequestValidator();
            var validatorResults = await validator.ValidateAsync(request);

            Assert.IsTrue(validatorResults.IsValid);
            Assert.IsTrue(validatorResults?.Errors?.Count == 0);
        }
    }
}