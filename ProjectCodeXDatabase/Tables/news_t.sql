CREATE TABLE [dbo].[news_t]
(
	ArticleID int primary key,
	PublishDate date,
	Type varchar(30),
	Link varchar(100),
	ViewCount int,
	Author varchar(40)
)
