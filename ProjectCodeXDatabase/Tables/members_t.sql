CREATE TABLE [dbo].[members_t]
(
	UserID 	int Not Null foreign key REFERENCES Users(UserID),
	Due     smallmoney Null,
	Balance 	smallmoney Null
)
