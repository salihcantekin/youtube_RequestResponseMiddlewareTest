using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RequestResponseLibraryTest.Api.Models;
using System;
using System.Threading.Tasks;

namespace RequestResponseLibraryTest.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> logger;

        public UserController(ILogger<UserController> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpPost()]
        [Route("login")]
        public IActionResult Login([FromBody] UserLoginRequestModel model)
        {
            logger.LogInformation("Hello from Login method");
            
            var result = new UserLoginResponseModel()
            {
                FirstName = model.UserName
            };

            return Ok(result);
        }

        [HttpPost()]
        [Route("onlylogin")]
        public IActionResult OnlyLogin([FromBody] UserLoginRequestModel model)
        {
            logger.LogInformation("Hello from OnlyLogin method");
            return Ok();
        }

        [HttpGet()]
        [Route("{id}")]
        public IActionResult GetUser(int id)
        {
            logger.LogInformation("Hello from GetUser method");
            var result = new UserLoginResponseModel()
            {
                FirstName = "Salih",
                LastName = "Cantekin"
            };

            return Ok(result);
        }
    }
}
