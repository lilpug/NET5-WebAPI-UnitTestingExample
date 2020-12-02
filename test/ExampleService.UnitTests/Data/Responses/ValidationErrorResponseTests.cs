using ExampleService.Data.Responses;
using NUnit.Framework;
using System.Collections.Generic;

namespace ExampleService.UnitTests.Data.Responses
{
    public class ValidationErrorResponseTests
    {
        private ValidationErrorResponse CreateInstance()
        {
            return new ValidationErrorResponse();
        }

        [Test]
        public void GetSetValidationErrors()
        {
            Dictionary<string, List<string>> validationErrors = new Dictionary<string, List<string>>()
            {
               { "testing", new List<string>() {"random validation testing"} }
            };
            var instance = CreateInstance();
            instance.ValidationErrors = validationErrors;

            Assert.AreEqual(instance?.ValidationErrors, validationErrors);
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
