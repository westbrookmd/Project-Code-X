CREATE TABLE [dbo].[news_t]
(
	ArticleID 	int Not Null primary key,
	PublishDate 	date Null,
	Type 		varchar(30) Null,
	Link 		varchar(100) Null,
	ViewCount 	int Null,
	Author 		varchar(40) Null
	PRIMARY KEY CLUSTERED ([ArticleID] ASC)
);
