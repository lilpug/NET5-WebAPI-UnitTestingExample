using System.Threading.Tasks;
using ExampleService.Business.Repositories;
using ExampleService.Data.Interfaces;
using NUnit.Framework;

namespace ExampleService.UnitTests.Business.Repositories
{
    public class PersonRepositoryTests
    {   
        private IPersonRepository CreateInstance()
        {
            return new PersonRepository();
        }

        [Test]
        public async Task GetPerson()
        {
            var repo = CreateInstance();
            var results = await repo.GetPersonAsync(int.MaxValue);
            Assert.AreNotEqual(results, null);
        }      
    }
}