CREATE PROCEDURE spEvent_Get 
	@EventID int
AS
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
WHERE
	EventID = @EventID
