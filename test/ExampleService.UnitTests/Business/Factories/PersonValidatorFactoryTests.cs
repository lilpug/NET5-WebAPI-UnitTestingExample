using ExampleService.Data.Interfaces;
using ExampleService.Data.Requests.Person;
using ExampleService.Business.Factories;
using ExampleService.Business.Validators.Person;
using Moq;
using NUnit.Framework;

namespace ExampleService.UnitTests.Business.Factories
{
    public class PersonValidatorFactoryTests
    {
        private PersonValidatorFactory PersonValidatorFactory { get; set; }

        [SetUp]
        public void Setup()
        {
            PersonValidatorFactory = new PersonValidatorFactory();
        }

        [Test]
        public void GetPersonRequestValidator()
        {
            IRequest request = new GetPersonRequest();

            var result = PersonValidatorFactory.GetValidator(request);

            Assert.IsTrue(result is GetPersonRequestValidator);
        }
        
        [Test]
        public void EmptyValidator()
        {
            var request = new Mock<IRequest>().Object;

            var result = PersonValidatorFactory.GetValidator(request);

            Assert.IsTrue(result == null);
        }
    }
}