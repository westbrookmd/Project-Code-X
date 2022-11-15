CREATE TABLE [dbo].[Users] (
    [UserID]  INT          IDENTITY (1, 1) NOT NULL,
    [FName]   VARCHAR (12) NULL,
    [LName]   VARCHAR (12) NULL,
    [Address] VARCHAR (20) NULL,
    [City]    VARCHAR (12) NULL,
    [STATE]   VARCHAR (2)  NULL,
    PRIMARY KEY CLUSTERED ([UserID] ASC)
);

