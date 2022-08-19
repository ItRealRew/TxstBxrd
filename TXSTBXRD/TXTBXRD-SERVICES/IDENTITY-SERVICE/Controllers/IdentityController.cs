using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using IDENTITY_SERVICE.Services;
using IDENTITY_SERVICE.Models;

namespace IDENTITY_SERVICE.Controllers
{
    [Route("[controller]")]
    public class IdentityController : Controller
    {
        private readonly ILogger<IdentityController> logger;
        private readonly IdentityService service;

        public IdentityController(ILogger<IdentityController> logger, IdentityService service)
        {
            this.logger = logger;
            this.service = service;
        }

        [HttpPost]
        public string IdentificationUser(Personal unknownUser) => service.Identification(unknownUser);

    }
}