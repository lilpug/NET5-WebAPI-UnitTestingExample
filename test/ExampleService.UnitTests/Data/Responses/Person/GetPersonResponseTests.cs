using ExampleService.Data.Responses.Person;
using NUnit.Framework;
using personResult = ExampleService.Data.Person;

namespace ExampleService.UnitTests.Data.Responses.Person
{
    public class GetPersonResponseTests
    {
        private GetPersonResponse CreateInstance()
        {
            return new GetPersonResponse();
        }

        [Test]
        public void GetSetPerson()
        {
            personResult person = new personResult()
            {
                Name = "test",
                Age = int.MaxValue
            };            
            var instance = CreateInstance();
            instance.Person = person;

            Assert.AreEqual(instance?.Person, person);
        }

        [Test]
        public void GetSetSuccessful()
        {
            var instance = CreateInstance();
            instance.Successful = false;

            Assert.AreEqual(instance?.Successful, false);
        }
    }
}
