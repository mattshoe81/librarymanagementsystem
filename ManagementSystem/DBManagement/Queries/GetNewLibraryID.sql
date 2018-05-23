DECLARE @maxBookID INT;
SET @maxBookID = (
	SELECT MAX(Library_ID)
	FROM Inventory_Books
)

DECLARE @maxMovieID INT;
SET @maxMovieID = (
	SELECT MAX(Library_ID)
	FROM Inventory_Books
)

DECLARE @max INT;
SET @max = (
	SELECT MAX(id) 
	FROM (Values (@maxBookID), (@maxMovieID)) AS value(ID)
)

SET @max = @max + 1;

SELECT @max
GO
