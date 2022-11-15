CREATE TABLE [dbo].[purchase_t]
(
	PurchID 	int Not Null primary key,
  	UserID 		int Not Null foreign key,
  	Price   	smallmoney Null,
	Notes   	varchar(500) Null
)
