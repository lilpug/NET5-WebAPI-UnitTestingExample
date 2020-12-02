using ExampleService.Data.Interfaces;
using ExampleService.Data.Responses;
using System.Collections.Generic;

namespace ExampleService.Data.Responses
{
    public class ValidationErrorResponse : BaseResponse, IResponse
    {
        public Dictionary<string, List<string>> ValidationErrors { get; set; }
    }
}