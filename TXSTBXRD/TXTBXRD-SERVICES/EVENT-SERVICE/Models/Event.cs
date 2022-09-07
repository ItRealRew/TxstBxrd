using System;
using System.Collections.Generic;

namespace EVENT_SERVICE.Models
{
    public partial class Event
    {
        public int EventId { get; set; }
        public string[] Customer { get; set; } = null!;
        public int EventType { get; set; }
        public string[]? Description { get; set; }
        public DateTime EventDate { get; set; }
    }
}
