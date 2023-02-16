using Microsoft.AspNetCore.Mvc;
using PERSONALITY_SERVICE.Models;
using PERSONALITY_SERVICE.Services;
using TXSTBXRD_MIDDLEWARE.PERSONALITY;

namespace PERSONALITY_SERVICE.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PersonalityController : ControllerBase
    {
        private readonly Personality service;

        public PersonalityController(Personality service)
        {
            this.service = service;
        }

        [HttpPost("registration")]
        public async Task<bool> RegistrationUser([FromBody] Registration newUser) => await service.AddNewUser(newUser);
    }
}