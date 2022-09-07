using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EVENT_SERVICE.Models;
using Microsoft.EntityFrameworkCore;

namespace EVENT_SERVICE.Services
{
    public class EventService
    {
        private readonly EventDBContext database;

        public EventService()
        {
            this.database = new EventDBContext();
        }

        public async Task<List<Event>> GetAllEvent()
        {
            return await database.Events.Select(
                s => new Event
                {
                    EventId = s.EventId,
                    Customer = s.Customer,
                    EventType = s.EventType,
                    Description = s.Description,
                    EventDate = s.EventDate
                }
            ).ToListAsync();
        }

        public async Task<int> InsertEvent(Event event_)
        {
            var entity = new Event()
            {
                EventId = event_.EventId,
                Customer = event_.Customer,
                EventType = event_.EventType,
                Description = event_.Description,
                EventDate = event_.EventDate
            };

            database.Events.Add(entity);
           return await database.SaveChangesAsync();
        }
    }
}