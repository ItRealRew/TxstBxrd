using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using IDENTITY_SERVICE.Services;
using IDENTITY_SERVICE.Models;
using System;
using System.Threading.Tasks;
using TXSTBXRD_MIDDLEWARE.IDENTITY;

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

        [HttpPost("identification")]
        public async Task<Guid> IdentificationUser(LogIn unknownUser) => await service.Identification(unknownUser);

        [HttpPost("registration")]
        public async Task<bool> RegistrationUser(Registration newUser) => await service.Registration(newUser);

        [HttpPost("logout")]
        public bool LogOutUser(LogOut loginOut) => service.loginOut(loginOut);

        [HttpPost("verification")] 
        public bool VerificationUserPermission(VerificationPermission userPermission) => service.Verification(userPermission);

        [HttpPost("changepermission")]
        public async Task<string> changeUserPermission(СhangingPermissions user) => await  service.ChangePermission(user);
    }
}