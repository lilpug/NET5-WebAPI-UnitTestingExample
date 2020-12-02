using System.Threading.Tasks;

namespace ExampleService.Data.Interfaces
{
    public interface IService
    {
        public Task<ServiceResult> ProcessAsync();
    }
}
