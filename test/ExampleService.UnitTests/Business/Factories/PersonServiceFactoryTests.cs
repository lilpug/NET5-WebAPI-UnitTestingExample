using ExampleService.Business.Factories;
using ExampleService.Business.Services.Person;
using ExampleService.Data.Enums;
using NUnit.Framework;

namespace ExampleService.UnitTests.Business.Factories
{
    public class PersonServiceFactoryTests
    {
        private PersonServiceFactory PersonServiceFactory { get; set; }
        
        [SetUp]
        public void Setup()
        {
            PersonServiceFactory = new PersonServiceFactory(null, null, null);
        }
        
        [Test]
        public void GetPersonService()
        {
            var results = PersonServiceFactory.GetService(PersonServiceType.GetPerson, null);
            Assert.IsTrue(results != null && results is GetPersonService service);
        }
        
        [Test]
        public void EmptyService()
        {
            var results = PersonServiceFactory.GetService(0, null);
            Assert.IsTrue(results == null);
        }
    }
}