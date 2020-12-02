using ExampleService.Data.Requests.Person;
using NUnit.Framework;

namespace ExampleService.UnitTests.Data.Requests.Person
{
    public class GetPersonRequestTests
    {
        private GetPersonRequest CreateInstance()
        {
            return new GetPersonRequest();
        }

        [Test]
        public void GetSetPersonID()
        {   
            var instance = CreateInstance();
            instance.PersonID = int.MaxValue;

            Assert.AreEqual(instance?.PersonID, int.MaxValue);
        }
    }
}
