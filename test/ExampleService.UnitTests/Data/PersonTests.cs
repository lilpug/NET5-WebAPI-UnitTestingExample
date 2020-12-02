using ExampleService.Data;
using NUnit.Framework;

namespace ExampleService.UnitTests.Data
{
    public class PersonTests
    {
        private Person CreateInstance()
        {
            return new Person();
        }

        [Test]
        public void GetSetName()
        {
            const string name = "test";
            var instance = CreateInstance();
            instance.Name = name;            

            Assert.AreEqual(instance?.Name, name);            
        }

        [Test]
        public void GetSetAge()
        {   
            var instance = CreateInstance();            
            instance.Age = int.MaxValue;

            Assert.AreEqual(instance?.Age, int.MaxValue);
        }
    }
}
