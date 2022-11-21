CREATE TABLE [dbo].[Purchase] (
    [PurchID]   INT           IDENTITY (1, 1) NOT NULL,
    [UserID]    VARCHAR (10)  NULL,
    [Price]     SMALLMONEY    NULL,
    [PurchDate] DATE          NULL,
    [Notes]     VARCHAR (250) NULL,
    PRIMARY KEY CLUSTERED ([PurchID] ASC)
);



