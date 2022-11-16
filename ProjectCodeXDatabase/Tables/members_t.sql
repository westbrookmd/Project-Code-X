CREATE TABLE [dbo].[members_t]
(
	[UserID] 	INT NOT NULL,
	[Due]     smallmoney NULL,
	[Balance] 	smallmoney NULL
	FOREIGN KEY ([UserID]) REFERENCES [dbo].[Users] ([UserID])
)
