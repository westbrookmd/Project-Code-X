CREATE TABLE [dbo].[emailevents_t]
(
	EventID 	int Not Null primary key,
	Date 		date Null,
	Time 		time Null,
	Location 	varchar(50) Null,
	EventType 	varchar(50) Null,
	Attendees#  	int Null,
	AmountRaised 	smallmoney Null,
	Cost 		smallmoney Null,
	Notes 		varchar(500) Null
)
