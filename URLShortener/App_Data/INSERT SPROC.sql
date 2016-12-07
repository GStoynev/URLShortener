CREATE PROCEDURE [dbo].[UrlShortenerViewModel_Insert]
	@OriginalURL nvarchar(max),
	@ShortURL nvarchar(max)
AS
BEGIN  TRAN
	IF @ShortURL IS NULL OR LEN(LTRIM(RTRIM(@ShortURL))) = 0 OR NOT EXISTS (SELECT 1 FROM [dbo].[UrlMap] WHERE [ShortURL] = @ShortURL)
	BEGIN
		INSERT [dbo].[UrlMap]([OriginalURL], [ShortURL])
		VALUES (@OriginalURL, @ShortURL)
    
		DECLARE @Id int
		SELECT @Id = [Id]
		FROM [dbo].[UrlMap]
		WHERE @@ROWCOUNT > 0 AND [Id] = scope_identity()
    
		SELECT t0.[Id]
		FROM [dbo].[UrlMap] AS t0
		WHERE @@ROWCOUNT > 0 AND t0.[Id] = @Id
	END
	ELSE
		SELECT 0 AS Id -- by convention, passing Id of 0 communicates to caller that INSERT didn't happen
COMMIT
RETURN 0