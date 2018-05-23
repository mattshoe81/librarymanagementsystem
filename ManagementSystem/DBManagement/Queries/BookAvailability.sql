USE Library

SELECT Copies_In_Stock
FROM Inventory_Master
WHERE Library_ID = @library_id