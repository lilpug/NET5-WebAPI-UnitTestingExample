using ExampleService.Data.Interfaces;

namespace ExampleService.Data.Responses.Person
{
    public class GetPersonResponse : BaseResponse, IResponse
    {
        public Data.Person Person { get; set; }
    }
}