﻿CREATE TABLE [dbo].[Events] (
    [EventID]      INT           NOT NULL,
    [Name]         VARCHAR (50)  NULL,
    [Date]         DATE          NULL,
    [Time]         TIME (7)      NULL,
    [Location]     VARCHAR (50)  NULL,
    [EventType]    VARCHAR (50)  NULL,
    [Attendees]    INT           NULL,
    [AmountRaised] SMALLMONEY    NULL,
    [Cost]         SMALLMONEY    NULL,
    [Notes]        VARCHAR (500) NULL,
    PRIMARY KEY CLUSTERED ([EventID] ASC)
);


