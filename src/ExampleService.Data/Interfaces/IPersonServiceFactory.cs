using ExampleService.Data.Enums;

namespace ExampleService.Data.Interfaces
{
    public interface IPersonServiceFactory
    {
        IService GetService(PersonServiceType service, IRequest request);
    }
}
