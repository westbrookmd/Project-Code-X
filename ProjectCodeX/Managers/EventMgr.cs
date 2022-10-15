using DataAccess.Data;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore.Storage;
using ProjectCodeX.Models;

namespace ProjectCodeX.Managers
{
    public class EventMgr
    {
        private readonly IEventData _eventData;

        public EventMgr(IEventData eventData)
        {
            _eventData = eventData;
        }

        public Event? GetEvent(int id)
        {
            EventModel e = _eventData.GetEvent(id).Result;
            if (e != null)
            {
                return MapEventModelToEvent(e!);
            }
            else
            {
                return null;
            }
        }

        private static Event MapEventModelToEvent(EventModel e)
        {
            return new()
            {
                EventID = e.EventID,
                Date = new DateTime(),
                Location = e.Location,
                Type = e.EventType,
                NumberOfAttendees = e.Attendees,
                AmountRaised = e.AmountRaised,
                Cost = e.Cost,
                Notes = e.Notes
            };
        }
    }
}
