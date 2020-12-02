using System.Net;
using System.Threading.Tasks;
using ExampleService.Controllers;
using ExampleService.Data;
using ExampleService.Data.Enums;
using ExampleService.Data.Interfaces;
using ExampleService.Data.Requests.Person;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace ExampleService.UnitTests.Service.Controllers
{
    public class PersonControllerTests
    {
        private Mock<IService> MockService { get; set; }
        private Mock<IPersonServiceFactory> MockPersonServiceFactory { get; set; }
        
        [SetUp]
        public void Setup()
        {
            MockService = new Mock<IService>();
            MockPersonServiceFactory = new Mock<IPersonServiceFactory>();
        }
        
        [Test]
        public async Task Get()
        {
            MockService.Setup(s => s.ProcessAsync()).ReturnsAsync(new ServiceResult() {Response = true, StatusCode = HttpStatusCode.OK});
            MockPersonServiceFactory.Setup(s => s.GetService(PersonServiceType.GetPerson, It.IsAny<IRequest>())).Returns(MockService.Object);
            
            PersonController controller = new PersonController(MockPersonServiceFactory.Object);
            var result = await controller.Get(new GetPersonRequest());
            
            MockPersonServiceFactory.Verify(s => s.GetService(PersonServiceType.GetPerson, It.IsAny<IRequest>()), Times.Once);
            MockService.Verify(s => s.ProcessAsync(), Times.Once);
            
            Assert.IsTrue(result != null && result is ObjectResult response && response.StatusCode == 200);
        }      
    }
}