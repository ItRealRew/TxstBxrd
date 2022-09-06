using Microsoft.AspNetCore.Mvc;

namespace EVENT_SERVICE.Controllers
{
    [Route("[controller]")]
    public class EventController : ControllerBase
    {
        [HttpPost("add")]
        public string AddToSystemLog() => "WAZAP!";
    }
}