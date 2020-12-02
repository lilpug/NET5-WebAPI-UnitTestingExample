using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using ExampleService.Business.Validators.Person;
using ExampleService.Data.Interfaces;
using ExampleService.Data.Requests.Person;
using ExampleService.Data.Responses;
using ExampleService.Data.Responses.Person;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using NUnit.Framework;
using IValidatorFactory = ExampleService.Data.Interfaces.IValidatorFactory;
using ValidationException = FluentValidation.ValidationException;
using PersonResult = ExampleService.Data.Person;
using ExampleService.Business.Services.Person;

namespace ExampleService.UnitTests.Business.Services.Person
{
    public class GetPersonServiceTests
    {
        private Mock<IServiceValidationConverter> MockServiceValidationConverter { get; set; }
        private Mock<IPersonRepository> MockPersonRepository { get; set; }
        private Mock<IValidatorFactory> MockValidatorFactory { get; set; }        
        private GetPersonRequest Request { get; set; }

        [SetUp]
        public void Setup()
        {
            MockServiceValidationConverter = new Mock<IServiceValidationConverter>();
            MockValidatorFactory = new Mock<IValidatorFactory>();
            MockPersonRepository = new Mock<IPersonRepository>();
            Request = new GetPersonRequest
            {
                PersonID = int.MaxValue
            };
        }

        private GetPersonService CreateService()
        {
            return new GetPersonService(MockValidatorFactory.Object, MockPersonRepository.Object, MockServiceValidationConverter.Object, Request);
        }

        [Test]
        public async Task SuccessfulFlow()
        {
            const string name = "example";
            const int age = int.MaxValue;
            var repositoryResults = new PersonResult()
            {
                Name = name,
                Age = age
            };

            MockValidatorFactory.Setup(v => v.GetValidator(Request)).Returns(new GetPersonRequestValidator());
            MockPersonRepository.Setup(r => r.GetPersonAsync(Request.PersonID.Value)).ReturnsAsync(repositoryResults);

            var service = CreateService();
            var result = await service.ProcessAsync();

            MockValidatorFactory.Verify(v => v.GetValidator(Request), Times.Once);
            MockPersonRepository.Verify(r => r.GetPersonAsync(Request.PersonID.Value), Times.Once);

            Assert.IsTrue(result != null);
            Assert.IsTrue(result.StatusCode == HttpStatusCode.OK);
            Assert.IsTrue(result.Response is GetPersonResponse response && response.Person != null && response.Successful);
        }

        [Test]
        public async Task ExceptionFlow()
        {
            MockValidatorFactory.Setup(v => v.GetValidator(Request)).Throws(new Exception("testing error."));

            var service = CreateService();
            var result = await service.ProcessAsync();

            MockValidatorFactory.Verify(v => v.GetValidator(Request), Times.Once);
            MockPersonRepository.Verify(r => r.GetPersonAsync(Request.PersonID.Value), Times.Never);

            Assert.IsTrue(result != null);
            Assert.IsTrue(result.StatusCode == HttpStatusCode.InternalServerError);
            Assert.IsTrue(result.Response is ErrorResponse response && !response.Successful);
        }

        [Test]
        public async Task ValidationFlow()
        {
            MockValidatorFactory.Setup(v => v.GetValidator(Request)).Throws(new ValidationException("testing error."));

            var service = CreateService();
            var result = await service.ProcessAsync();

            MockValidatorFactory.Verify(v => v.GetValidator(Request), Times.Once);
            MockPersonRepository.Verify(r => r.GetPersonAsync(Request.PersonID.Value), Times.Never);

            Assert.IsTrue(result != null);
            Assert.IsTrue(result.StatusCode == HttpStatusCode.BadRequest);
            Assert.IsTrue(result.Response is ValidationErrorResponse response && !response.Successful);
        }

        [Test]
        public async Task EmptyValidator()
        {
            MockValidatorFactory.Setup(v => v.GetValidator(Request)).Returns<IValidator>(null);

            var service = CreateService();
            var result = await service.ProcessAsync();

            MockValidatorFactory.Verify(v => v.GetValidator(Request), Times.Once);
            MockPersonRepository.Verify(r => r.GetPersonAsync(Request.PersonID.Value), Times.Never);

            Assert.IsTrue(result != null);
            Assert.IsTrue(result.StatusCode == HttpStatusCode.InternalServerError);
            Assert.IsTrue(result.Response is ErrorResponse response && !response.Successful);
        }
        
        [Test]
        public async Task ValidatorFailure()
        {
            var mockValidationResult = new Mock<ValidationResult>();
            mockValidationResult.SetupGet(v => v.IsValid).Returns(false);

            MockServiceValidationConverter.Setup(v => v.ConvertToDictionaryFormat(It.IsAny<ValidationResult>()))
                .Returns(new Dictionary<string, List<string>> { { "testing", new List<string> { "testing message." } } });

            var mockValidator = new Mock<GetPersonRequestValidator>();
            mockValidator.Setup(v => v.ValidateAsync(It.IsAny<ValidationContext<GetPersonRequest>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockValidationResult.Object);

            MockValidatorFactory.Setup(v => v.GetValidator(Request)).Returns(mockValidator.Object);

            var service = CreateService();
            var result = await service.ProcessAsync();

            MockValidatorFactory.Verify(v => v.GetValidator(Request), Times.Once);
            mockValidator.Verify(v => v.ValidateAsync(It.IsAny<ValidationContext<GetPersonRequest>>(), It.IsAny<CancellationToken>()), Times.Once);
            mockValidationResult.VerifyGet(v => v.IsValid, Times.Once);
            MockServiceValidationConverter.Verify(v => v.ConvertToDictionaryFormat(It.IsAny<ValidationResult>()), Times.Once);

            MockPersonRepository.Verify(r => r.GetPersonAsync(Request.PersonID.Value), Times.Never);

            Assert.IsTrue(result != null);
            Assert.IsTrue(result.StatusCode == HttpStatusCode.BadRequest);
            Assert.IsTrue(result.Response is ValidationErrorResponse response && 
                          !response.Successful &&
                          response.ValidationErrors.ContainsKey("testing") &&
                          response.ValidationErrors["testing"][0] == "testing message.");
            
        }
    }
}