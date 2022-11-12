CREATE TABLE [dbo].[events_t]
(
	EventID 	int Not Null primary key IDENTITY,
	Date 		date Null,
	Time 		time Null,
	Location 	varchar(50) Null,
	EventType 	varchar(50) Null,
	[Attendees]  	int Null,
	AmountRaised 	smallmoney Null,
	Cost 		smallmoney Null,
	Notes 		varchar(500) Null
)
