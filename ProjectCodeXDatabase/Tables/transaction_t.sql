CREATE TABLE [dbo].[transaction_t]
(
	TransID 	int Not Null primary key,
	UserID		int Not Null,
	TransType	varchar(20) Null,
	Amount 		smallmoney Null,
	Notes 		varchar(500) Null
)
