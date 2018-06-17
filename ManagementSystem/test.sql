USE Library
/*
	SELECT *
	FROM Loaned_Items
	RIGHT JOIN Account_Details
	ON Loaned_Items.Borrower_Email = Account_Details.Email
	WHERE Loaned_Items.LIbrary_ID = 0
*/

SELECT * 
FROM Account_Details