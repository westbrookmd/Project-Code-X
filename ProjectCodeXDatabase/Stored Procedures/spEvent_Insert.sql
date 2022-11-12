CREATE PROCEDURE spEvent_Insert
	@Date 			date,
	@Time 			time,
	@Location 		varchar(50),
	@EventType 		varchar(50),
	@Attendees  	int,
	@AmountRaised 	smallmoney,
	@Cost 			smallmoney,
	@Notes 			varchar(500)
AS
SET NOCOUNT ON;

INSERT INTO emailevents_t
	(Date,
	Time,
	Location,
	EventType,
	Attendees,
	AmountRaised,
	Cost,
	[Notes])
VALUES
(
	@Date,
	@Time,
	@Location,
	@EventType,
	@Attendees,
	@AmountRaised,
	@Cost,
	@Notes
)
return @@ROWCOUNT;