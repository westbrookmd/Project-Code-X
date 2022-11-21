CREATE TABLE [dbo].[Users] (
    [UserID]  INT          IDENTITY (1, 1) NOT NULL,
    [FName]   VARCHAR (12) NULL,
    [LName]   VARCHAR (12) NULL,
    [Address] VARCHAR (20) NULL,
    [City]    VARCHAR (12) NULL,
    [STATE]   VARCHAR (2)  NULL,
    [Phone]     VARCHAR (10) NULL,
    [Email]     VARCHAR (25) NULL,
    [PayMethod] VARCHAR (15) NULL,
    [NextBillDate] DATE NULL,
    PRIMARY KEY CLUSTERED ([UserID] ASC)
);

