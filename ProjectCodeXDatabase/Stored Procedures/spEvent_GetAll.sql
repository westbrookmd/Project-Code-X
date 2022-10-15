CREATE PROCEDURE spEvent_GetAll AS
SET NOCOUNT ON;

SELECT 
	EventID,
	[Date],
	[Time],
	[Location],
	EventType,
	Attendees,
	AmountRaised,
	Cost,
	Notes
FROM 
	emailevents_t
