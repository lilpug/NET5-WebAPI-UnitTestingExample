using ExampleService.Data.Enums;
using ExampleService.Data.Interfaces;
using ExampleService.Data.Requests.Person;
using ExampleService.Data.Responses;
using ExampleService.Data.Responses.Person;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;
using System.Threading.Tasks;

namespace ExampleService.Controllers
{
    [SwaggerResponse(500, "Outputs the error details.", typeof(ErrorResponse))]
    [SwaggerResponse(400, "Outputs the validation error details.", typeof(ValidationErrorResponse))]    
    [Produces("application/json")]
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private IPersonServiceFactory _personServiceFactory;

        public PersonController(IPersonServiceFactory personServiceFactory)
        {
            _personServiceFactory = personServiceFactory;
        }

        [SwaggerResponse(200, "Obtains the person details from a person ID.", typeof(GetPersonResponse))]   
        [Route("{PersonID}")]
        [HttpGet]
        public async Task<IActionResult> Get([FromHeader]GetPersonRequest request)
        {
            IService service = _personServiceFactory.GetService(PersonServiceType.GetPerson, request);
            var result = await service.ProcessAsync();
            return StatusCode((int)(result?.StatusCode ?? HttpStatusCode.InternalServerError), result?.Response);
        }
    }
}