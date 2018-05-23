INSERT INTO Loaned_Items (LIbrary_ID, Checkout_Date, Due_Date, MediaType, Borrower_ID)
VALUES (@libraryID, @checkoutDate, @dueDate, @mediaType, @borrowerID)

UPDATE Inventory_Master
SET Copies_In_Stock = @copiesInStockUpdated
WHERE (Library_ID = @libraryID)