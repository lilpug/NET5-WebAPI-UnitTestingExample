using ExampleService.Data.Interfaces;

namespace ExampleService.Data.Requests.Person
{
    public class GetPersonRequest : IRequest
    {
        public int? PersonID { get; set; }
    }
}
