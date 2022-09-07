namespace EVENT_SERVICE.Models
{
    public class Event
    {
        public string Customer { get; set; }
        public string EventType { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}