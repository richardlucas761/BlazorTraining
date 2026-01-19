CREATE TABLE [dbo].[Movie]
(
	[MovieId]       INT IDENTITY(1,1)   NOT NULL, 
    [Title]         NVARCHAR(255)       NOT NULL, 
    [ReleaseDate]   DATE                NOT NULL, 
    [Genre]         NVARCHAR(50)        NOT NULL, 
    [Price]         DECIMAL(18, 2)      NOT NULL,
    [Rating]        NVARCHAR(5)         NOT NULL, 
    CONSTRAINT [PK_Movie] PRIMARY KEY CLUSTERED ([MovieId])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Unique identifier of this Movie.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Movie',
    @level2type = N'COLUMN',
    @level2name = N'MovieId'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Title of the Movie.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Movie',
    @level2type = N'COLUMN',
    @level2name = N'Title'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'When this Movie was released.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Movie',
    @level2type = N'COLUMN',
    @level2name = N'ReleaseDate'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Genre of the Movie.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Movie',
    @level2type = N'COLUMN',
    @level2name = N'Genre'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'Currency cost of this Movie.',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Movie',
    @level2type = N'COLUMN',
    @level2name = N'Price'
GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'The Motion Picture Association rating for this film',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Movie',
    @level2type = N'COLUMN',
    @level2name = N'Rating'