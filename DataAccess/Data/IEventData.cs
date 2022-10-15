using DataAccess.Models;

namespace DataAccess.Data
{
    public interface IEventData
    {
        Task DeleteEvent(int id);
        Task<EventModel?> GetEvent(int id);
        Task<IEnumerable<EventModel>> GetEvents();
        Task InsertEvent(EventModel eventModel);
        Task UpdateEvent(EventModel eventModel);
    }
}