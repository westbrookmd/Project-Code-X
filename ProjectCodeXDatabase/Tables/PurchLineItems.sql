CREATE TABLE [dbo].[PurchLineItems] (
    [PLineID] INT        IDENTITY (1, 1) NOT NULL,
    [PurchID] INT        NULL,
    [Qnty]    INT        NULL,
    [Price]   SMALLMONEY NULL,
    PRIMARY KEY CLUSTERED ([PLineID] ASC),
    FOREIGN KEY ([PurchID]) REFERENCES [dbo].[Purchase] ([PurchID])
);

