using ExampleService.Data.Responses;
using NUnit.Framework;

namespace ExampleService.UnitTests.Data.Responses
{
    public class ErrorResponseTests
    {
        private ErrorResponse CreateInstance()
        {
            return new ErrorResponse();
        }

        [Test]
        public void GetSetMessage()
        {
            const string message = "test";
            var instance = CreateInstance();
            instance.Message = message;

            Assert.AreEqual(instance?.Message, message);
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
