CREATE TABLE [dbo].[Inventory] (
    [InvID]         INT        IDENTITY (1, 1) NOT NULL,
    [ItemName]      VARCHAR(30) NULL,
    [Qnty]          INT        NULL,
    [Description]   VARCHAR(100) NULL,
    PRIMARY KEY CLUSTERED ([InvID] ASC)
);
