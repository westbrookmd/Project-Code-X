CREATE TABLE [dbo].[users_t]
(
	UserID		varchar(10) Not Null Primary Key,
	FirstName	varchar(20) Null,
	LastName	varchar(20) Null,
	Address 	varchar(100) Null,
	City		varchar(25) Null,
	State		varchar(2) Null
)
