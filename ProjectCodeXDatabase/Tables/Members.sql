CREATE TABLE [dbo].[Members]
(
	UserID 	int Not Null foreign key REFERENCES [Users](UserID),
	Due     smallmoney Null,
	Balance 	smallmoney Null
)
