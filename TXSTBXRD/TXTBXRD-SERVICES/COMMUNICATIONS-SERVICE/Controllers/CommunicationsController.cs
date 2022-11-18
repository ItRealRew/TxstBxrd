using Microsoft.AspNetCore.Mvc;
using TXSTBXRD_MIDDLEWARE.COMMUNICATIONS;
using COMMUNICATIONS_SERVICE.Services;

namespace COMMUNICATIONS_SERVICE.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CommunicationsController : ControllerBase
    {
        private readonly EmailService service;

        public CommunicationsController(EmailService service)
        {
            this.service = service;
        }

        [HttpPost("resetpswd")]
        public async Task<bool> ResetUserPassword([FromBody] ResetPassword dateReset) => await service.ResetUserPassword(dateReset);
    }
}