using ExampleService.Data;
using NUnit.Framework;
using System.Net;

namespace ExampleService.UnitTests.Data
{
    public class ServiceResultTests
    {
        private ServiceResult CreateInstance()
        {
            return new ServiceResult();
        }

        [Test]
        public void GetSetResponse()
        {
            const string response = "random response";
            var instance = CreateInstance();
            instance.Response = response;

            Assert.AreEqual(instance?.Response, response);
        }

        [Test]
        public void GetSetStatusCode()
        {   
            var instance = CreateInstance();            
            instance.StatusCode = HttpStatusCode.Unauthorized;

            Assert.AreEqual(instance?.StatusCode, HttpStatusCode.Unauthorized);
        }
    }
}
