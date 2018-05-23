DELETE FROM Loaned_Items
WHERE LIbrary_ID = @libraryID

UPDATE Inventory_Master
SET Copies_In_Stock = @copiesInStockUpdated
WHERE (Library_ID = @libraryID)