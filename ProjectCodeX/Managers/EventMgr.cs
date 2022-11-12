using DataAccess.Data;
using DataAccess.Models;
using Microsoft.Build.Execution;
using Microsoft.EntityFrameworkCore.Storage;
using ProjectCodeX.Models;
using System.Security.Cryptography.X509Certificates;

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

        public async Task<bool> InsertEventAsync(Event eventData)
        {
            EventModel em = MapEventToEventModel(eventData);
            await _eventData.InsertEvent(em);

            return true;
        }

        private EventModel MapEventToEventModel(Event eventData)
        {
            return new EventModel
            {
                EventID = eventData.EventID,
                Date = eventData.Date,
                Time = eventData.Time.TimeOfDay,
                Location = eventData.Location,
                EventType = eventData.Type,
                Attendees = eventData.NumberOfAttendees,
                AmountRaised = eventData.AmountRaised,
                Cost = eventData.Cost,
                Notes = eventData.Notes
            };
        }

        private static Event MapEventModelToEvent(EventModel em)
        {

            return new()
            {
                EventID = em.EventID,
                Date = em.Date,
                Time = em.Date + em.Time,
                Location = em.Location,
                Type = em.EventType,
                NumberOfAttendees = em.Attendees,
                AmountRaised = em.AmountRaised,
                Cost = em.Cost,
                Notes = em.Notes
            };
        }
    }
}
