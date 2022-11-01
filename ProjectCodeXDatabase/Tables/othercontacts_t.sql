CREATE TABLE [dbo].[othercontacts_t]
(
	ContactID 	int Not Null primary key,
	FirstName 		varchar(50) Null,
	LastName 		varchar(50) Null,
	Company 	varchar(50) Null,
	Address 	varchar(50) Null,
	City 	varchar(50) Null,
	State 	varchar(2) Null,
	Phone 		varchar(20) Null,
	Email 	varchar(100) Null
)
