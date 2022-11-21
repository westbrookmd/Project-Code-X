CREATE TABLE [dbo].[news_t]
(
	[ArticleID] 	INT           IDENTITY (1, 1) NOT NULL,
	[PublishDate] 	DATE NULL,
	[Summary] 	VARCHAR(30) NULL,
	[ViewCount] 	INT NULL,
	[Author] 	VARCHAR(40) NULL,
	PRIMARY KEY CLUSTERED ([ArticleID] ASC)
);
