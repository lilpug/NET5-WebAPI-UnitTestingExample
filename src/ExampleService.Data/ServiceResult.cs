using System.Net;

namespace ExampleService.Data
{
    public class ServiceResult
    {
        public object Response { get; set; }
        public HttpStatusCode? StatusCode { get; set; }
    }
}