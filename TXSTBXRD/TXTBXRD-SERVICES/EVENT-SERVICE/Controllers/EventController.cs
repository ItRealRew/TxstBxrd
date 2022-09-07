using Microsoft.AspNetCore.Mvc;
using EVENT_SERVICE.Models;
using EVENT_SERVICE.Services;

namespace EVENT_SERVICE.Controllers
{
    [Route("[controller]")]
    public class EventController : ControllerBase
    {

        private readonly EventService service;

        public EventController(EventService service)
        {
            this.service = service;
        }

        [HttpPost("add")]
        public async Task<bool> AddToSystemLog(Event event_) => await service.InsertEvent(event_) > 0 ? true : false;
    }
}