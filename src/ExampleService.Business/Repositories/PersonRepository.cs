using ExampleService.Data;
using ExampleService.Data.Interfaces;
using System.Threading.Tasks;

namespace ExampleService.Business.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        //This is an example function only!        
        public Task<Person> GetPersonAsync(int personID)
        {
            return Task.FromResult(new Person() { Name = "bob", Age = 99 });
        }
    }
}
