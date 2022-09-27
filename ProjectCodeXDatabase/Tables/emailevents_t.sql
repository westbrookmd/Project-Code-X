CREATE TABLE [dbo].[emailevents_t]
(
	EventID int primary key,
	Date date,
	Time time,
	Location varchar(50),
	EventType varchar(50),
	Attendees#  int,
	AmountRaised smallmoney,
	Cost smallmoney,
	Notes varchar(500)
)
