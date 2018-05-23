UPDATE Inventory_Books
SET Title = @title, Author = @author, [Format] = @format, ISBN10 = @isbn10, ISBN13 = @isbn13, [Length] = @length, Genre = @genre, Publisher = @publisher, Publication_Year = @publicationYear, Description = @description
WHERE (Library_ID = @libraryID)
