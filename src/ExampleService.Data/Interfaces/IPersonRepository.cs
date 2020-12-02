using System.Threading.Tasks;

namespace ExampleService.Data.Interfaces
{
    public interface IPersonRepository
    {
        Task<Person> GetPersonAsync(int personID);
    }
}
