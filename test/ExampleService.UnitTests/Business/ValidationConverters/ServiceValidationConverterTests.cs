using ExampleService.Business;
using ExampleService.Data.Interfaces;
using FluentValidation.Results;
using NUnit.Framework;
using System.Collections.Generic;

namespace ExampleService.UnitTests.Business.ValidationConverters
{
    public class ServiceValidationConverterTests
    {   
        private IServiceValidationConverter CreateInstance()
        {
            return new ServiceValidationConverter();
        }

        [Test]
        public void EmptyResults()
        {
            ValidationResult validationResult = new ValidationResult();

            var instance = CreateInstance();
            var results = instance.ConvertToDictionaryFormat(validationResult);

            Assert.AreEqual(results, null);            
        }

        [Test]
        public void NullResults()
        {
            var instance = CreateInstance();
            var results = instance.ConvertToDictionaryFormat(null);

            Assert.AreEqual(results, null);            
        }

        [Test]
        public void PopulatedResults()
        {
            ValidationResult validationResult = new ValidationResult(new List<ValidationFailure>()
            {
                new ValidationFailure("TestProperty", "TestProperty is required."),
                new ValidationFailure("TestProperty2", "TestProperty2 is required."),
            });
            
            var instance = CreateInstance();
            var results = instance.ConvertToDictionaryFormat(validationResult);

            Assert.IsNotNull(results, null);
            Assert.IsTrue(results.Count == 2);
            Assert.IsTrue(results.ContainsKey("TestProperty"));
            Assert.IsTrue(results.ContainsKey("TestProperty2"));
        }
    }
}