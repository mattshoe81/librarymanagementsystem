DELETE FROM Inventory_Books
WHERE (Library_ID = @libraryID)

DELETE FROM Inventory_Master
WHERE (Library_ID = @libraryID)