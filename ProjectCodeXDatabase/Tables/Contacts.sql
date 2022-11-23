CREATE TABLE [dbo].[Contacts] (
    [ContactID] INT          IDENTITY (1, 1) NOT NULL,
    [FName]     VARCHAR (12) NOT NULL,
    [LName]     VARCHAR (12) NOT NULL,
    [Company]   VARCHAR (20) NULL,
    [Address]   VARCHAR (12) NULL,
    [City]      VARCHAR (12) NULL,
    [State]     CHAR (2)     NULL,
    [Phone]     VARCHAR (10) NOT NULL,
    [Email]     VARCHAR (25) NOT NULL,
    PRIMARY KEY CLUSTERED ([ContactID] ASC)
);

