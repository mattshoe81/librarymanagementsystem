USE Library;



/*
  Add a new row to the Inventory_Books table with the new book.
*/

INSERT INTO dbo.Inventory_Books 
	([Library_ID], [Title], [Author], [Format], [ISBN10], [ISBN13], [Length], [Genre], [Publisher], [Publication_Year], [Description])
VALUES (@libraryID, @title, @author, @format, @isbn10, @isbn13, @length, @genre, @publisher, @publicationYear, @description) 

/* 
  Find out whether a copy of the book already exists (duplicates allowed)
*/
DECLARE @record_count INT;
SET @record_count = (
		SELECT COUNT(Library_ID) 
		FROM Inventory_Master
		WHERE Library_ID = @libraryID
		);

/*
  if the book already exists, increment the copies in stock and the total copies,
  otherwise add a new row for the new book
*/
IF @record_count > 0
	BEGIN
		DECLARE @copies INT;
		SET @copies = (
			SELECT Copies_Total 
			FROM Inventory_Master 
			WHERE (Library_ID = @libraryID));
		SET @copies = @copies + 1;
		UPDATE Inventory_Master
		SET Copies_Total = @copies, Copies_In_Stock = @copies
		WHERE Library_ID = @libraryID 
	END
ELSE 
	BEGIN 
		INSERT INTO Inventory_Master (Library_ID, Title, Media, Copies_Total, Copies_In_Stock)
		VALUES
			(@libraryID, @title, @media, 1, 1)
	END