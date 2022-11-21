CREATE TABLE [dbo].[Donations] (
    [DonationID]   INT           IDENTITY (1, 1) NOT NULL,
    [UserID]       INT           NULL,
    [Amount]       SMALLMONEY    NULL,
    [DonationDate] DATE          NULL,
    [Notes]        VARCHAR (250) NULL,
    PRIMARY KEY CLUSTERED ([DonationID] ASC)
);



