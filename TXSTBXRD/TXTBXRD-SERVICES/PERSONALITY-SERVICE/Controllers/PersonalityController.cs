using Microsoft.AspNetCore.Mvc;
using PERSONALITY_SERVICE.Models;
using PERSONALITY_SERVICE.Services;

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

        [HttpGet("started")]
        public async Task<List<User>> RegistrationUser()
        {
            return await service.GetAllUsers();
        }
    }
}