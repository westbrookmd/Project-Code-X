CREATE TABLE [dbo].[inventory_t]
(
	InventoryID 	int Not Null primary key,
	ItemName 	varchar(30) Null,
	Quantity 	int Null,
	Description 	varchar(500) Null
)
