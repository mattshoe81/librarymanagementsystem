DECLARE @formatCode INT
SET @formatCode = (
	SELECT Media
	FROM Inventory_Master
	WHERE (Library_ID = @libraryID)
	);

SELECT Media_Name
FROM Media_Details
WHERE (Media_Key = 0)