CREATE TABLE [dbo].[purchaseLineItems_t]
(
  	PLineID 	int Not Null primary key,
	PurchID 	int Not Null foreign key,
  	InventoryID 	int Not Null foreign key,
  	Quantity   	int Null,
	Price   	smallmoney Null
)
