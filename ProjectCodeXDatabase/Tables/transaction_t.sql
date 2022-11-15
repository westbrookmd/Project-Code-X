CREATE TABLE [dbo].[transaction_t] (
    [TransID]   INT           NOT NULL,
    [TransType] VARCHAR (20)  NULL,
    [Amount]    SMALLMONEY    NULL,
    [Notes]     VARCHAR (500) NULL,
    PRIMARY KEY CLUSTERED ([TransID] ASC)
);


