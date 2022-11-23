using DataAccess.DbAccess;
using DataAccess.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data;

public class EventData : IEventData
{
	private readonly ISqlDataAccess _db;

	public EventData(ISqlDataAccess db)
	{
		_db = db;
	}

	public Task<IEnumerable<EventModel>> GetEvents() =>
		_db.LoadData<EventModel, dynamic>("dbo.spEvent_GetAll", new { });

	public async Task<EventModel?> GetEvent(int id)
	{
		var results = await _db.LoadData<EventModel, dynamic>(
			"dbo.spEvent_Get",
			new { EventID = id });
		return results.FirstOrDefault();
	}

	public Task InsertEvent(EventModel eventModel) =>
		_db.SaveData("dbo.spEvent_Insert", new
		{
			eventModel.Date,
			eventModel.Time,
			eventModel.Location,
			eventModel.EventType,
			eventModel.Attendees,
			eventModel.AmountRaised,
			eventModel.Cost,
			eventModel.Notes
		});

	public Task UpdateEvent(EventModel eventModel) =>
		_db.SaveData("dbo.spEvent_Save", eventModel);

	public Task DeleteEvent(int id) =>
		_db.SaveData("dbo.spEvent_Delete", new { EventID = id });

}
