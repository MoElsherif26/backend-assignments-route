--1. Write a query that displays Full name of an employee who has more than 3
--letters in his/her First Name.{1 Point}
SELECT Fname + ' ' + Lname AS [Full Name]
FROM Employee
WHERE LEN(Fname) > 3;

--2. Write a query to display the total number of Programming books
--available in the library with alias name ‘NO OF PROGRAMMING
--BOOKS’ {1 Point}
SELECT COUNT(*) AS [NO OF PROGRAMMING BOOKS]
FROM Book
JOIN Category ON Book.Cat_id = Category.Id
WHERE Cat_name = 'Programming';

--3. Write a query to display the number of books published by
--(HarperCollins) with the alias name 'NO_OF_BOOKS'. {1 Point}SELECT COUNT(*) AS NO_OF_BOOKS
FROM Book
JOIN Publisher ON Book.Publisher_id = Publisher.Id
WHERE Publisher.Name = 'HarperCollins';

--4. Write a
--query to display the User SSN and name, date of borrowing and due date of
--the User whose due date is before July 2022. {1 Point}
SELECT U.SSN, U.User_Name, B.Borrow_date, B.Due_date
FROM Borrowing B
JOIN Users U ON B.User_ssn = U.SSN
WHERE B.Due_date < '2022-07-01';

--5. Write a query to display book title, author name and display in the
--following format,
--' [Book Title] is written by [Author Name]. {2 Points}
SELECT 
    '[' + Book.Title + '] is written by [' + Author.Name + '].' AS Description
FROM Book
JOIN Book_Author ON Book.Id = Book_Author.Book_id
JOIN Author ON Author.Id = Book_Author.Author_id;

--6. Write a query to display the name of users who have letter 'A' in their
--names. {1 Point}
SELECT User_Name
FROM Users
WHERE User_Name LIKE '%A%' OR User_Name LIKE '%a%';

--7. Write a query that display user SSN who makes the most borrowing{2
--Points}
SELECT TOP 1 User_ssn
FROM Borrowing
GROUP BY User_ssn
ORDER BY COUNT(*) DESC;

--8. Write a query that displays the total amount of money that each user paid
--for borrowing books. {2 Points}
SELECT User_ssn, SUM(Amount) AS Total_Amount_Paid
FROM Borrowing
GROUP BY User_ssn;

--9. write a query that displays the category which has the book that has the
--minimum amount of money for borrowing. {2 Points}
SELECT TOP 1 C.Cat_name
FROM Borrowing B
JOIN Book ON B.Book_id = Book.Id
JOIN Category C ON Book.Cat_id = C.Id
WHERE B.Amount IS NOT NULL
ORDER BY B.Amount ASC;

--10.write a query that displays the email of an employee if it's not found,
--display address if it's not found, display date of birthday. {1 Point}
SELECT 
    Fname + ' ' + Lname AS Full_Name,
    ISNULL(Email, ISNULL(Address, CONVERT(VARCHAR, DOB, 103))) AS Contact_Info
FROM Employee;

--11.Write a query to list the category and number of books in each category
--with the alias name 'Count Of Books'. {1 Point}
SELECT 
    C.Cat_name, 
    COUNT(B.Id) AS [Count Of Books]
FROM Category C
LEFT JOIN Book B ON C.Id = B.Cat_id
GROUP BY C.Cat_name;

--12.Write a query that display books id which is not found in floor num = 1
--and shelf-code = A1.{2 Points}
SELECT B.Id
FROM Book B
JOIN Shelf S ON B.Shelf_code = S.Code
WHERE NOT (S.Floor_num = 1 AND B.Shelf_code = 'A1');

--13.Write a query that displays the floor number , Number of Blocks and
--number of employees working on that floor.{2 Points}
SELECT 
    F.Number AS Floor_Number,
    F.Num_blocks,
    COUNT(E.Id) AS Employee_Count
FROM Floor F
LEFT JOIN Employee E ON E.Floor_no = F.Number
GROUP BY F.Number, F.Num_blocks;

--14.Display Book Title and User Name to designate Borrowing that occurred
--within the period ‘3/1/2022’ and ‘10/1/2022’.{2 Points}
SELECT 
    B.Title, 
    U.User_Name
FROM Borrowing BR
JOIN Book B ON BR.Book_id = B.Id
JOIN Users U ON BR.User_ssn = U.SSN
WHERE BR.Borrow_date BETWEEN '2022-03-01' AND '2022-10-01';

--15.Display Employee Full Name and Name Of his/her Supervisor as
--Supervisor Name.{2 Points}
SELECT 
    E.Fname + ' ' + E.Lname AS Employee_Name,
    S.Fname + ' ' + S.Lname AS Supervisor_Name
FROM Employee E
LEFT JOIN Employee S ON E.Super_id = S.Id;

--16.Select Employee name and his/her salary but if there is no salary display
--Employee bonus. {2 Points}
SELECT 
    Fname + ' ' + Lname AS Employee_Name,
    ISNULL(CAST(Salary AS VARCHAR), CAST(Bouns AS VARCHAR)) AS Salary_Or_Bonus
FROM Employee;

--17.Display max and min salary for Employees {2 Points}
SELECT 
    MAX(Salary) AS Max_Salary,
    MIN(Salary) AS Min_Salary
FROM Employee;

--18.Write a function that take Number and display if it is even or odd {2 Points}
CREATE OR ALTER FUNCTION dbo.IsEvenOrOdd (@Number INT)
RETURNS VARCHAR(10)
AS
BEGIN
    RETURN CASE 
        WHEN @Number % 2 = 0 THEN 'Even'
        ELSE 'Odd'
    END;
END;

SELECT dbo.IsEvenOrOdd(17)

--19.write a function that take category name and display Title of books in that
--category {2 Points}
CREATE OR ALTER FUNCTION dbo.GetBookTitlesByCategory (@CatName VARCHAR(50))
RETURNS TABLE
AS
RETURN
(
    SELECT B.Title [Book Title]
    FROM Book B
    JOIN Category C ON B.Cat_id = C.Id
    WHERE C.Cat_name = @CatName
);

SELECT * FROM dbo.GetBookTitlesByCategory('programming ')

--20. write a function that takes the phone of the user and displays Book Title ,
--user-name, amount of money and due-date. {2 Points}
CREATE OR ALTER FUNCTION dbo.GetBorrowingByUserPhone (@Phone VARCHAR(11))
RETURNS TABLE
AS
RETURN
(
    SELECT 
        B.Title [Book Title],
        U.User_Name,
        BR.Amount [amount of money],
        BR.Due_date
    FROM User_phones UP
    JOIN Users U ON UP.User_ssn = U.SSN
    JOIN Borrowing BR ON U.SSN = BR.User_ssn
    JOIN Book B ON B.Id = BR.Book_id
    WHERE UP.Phone_num = @Phone
);

SELECT * FROM dbo.GetBorrowingByUserPhone('0102585555')

--21.Write a function that take user name and check if it's duplicated
--return Message in the following format ([User Name] is Repeated
--[Count] times) if it's not duplicated display msg with this format [user
--name] is not duplicated,if it's not Found Return [User Name] is Not
--Found {2 Points}
CREATE OR ALTER FUNCTION dbo.CheckUserNameDuplicate (@UserName VARCHAR(50))
RETURNS VARCHAR(100)
AS
BEGIN
    DECLARE @Count INT;
    DECLARE @Result VARCHAR(100);

    SELECT @Count = COUNT(*) FROM Users WHERE User_Name = @UserName;

    IF @Count > 1
        SET @Result = CONCAT('[', @UserName, ']', ' is Repeated ', '[', @Count, ']', ' times');
    ELSE IF @Count = 1
        SET @Result = CONCAT('[', @UserName, ']', ' is not duplicated');
    ELSE
        SET @Result = CONCAT('[', @UserName, ']', ' is Not Found');

    RETURN @Result;
END;

SELECT dbo.CheckUserNameDuplicate('Amr Ahmed');

--22.Create a scalar function that takes date and Format to return Date With
--That Format. {2 Points}
CREATE OR ALTER FUNCTION dbo.FormatDate (
    @InputDate DATE,
    @FormatCode VARCHAR(10)
)
RETURNS VARCHAR(100)
AS
BEGIN
    RETURN FORMAT(@InputDate, @FormatCode);
END;

SELECT dbo.FormatDate(GETDATE(), 'dd-MM-yyyy');
SELECT dbo.FormatDate(GETDATE(), 'MMMM yyyy');

--23.Create a stored procedure to show the number of books per Category.{2
--Points}
CREATE OR ALTER PROCEDURE dbo.GetBookCountByCategory
AS
BEGIN
    SELECT C.Cat_name [Category Name], COUNT(B.Id) AS [number of books per Category]
    FROM Category C
    LEFT JOIN Book B ON C.Id = B.Cat_id
    GROUP BY C.Cat_name;
END;

EXEC dbo.GetBookCountByCategory;

--24.Create a stored procedure that will be used in case there is an old manager
--who has left the floor and a new one becomes his replacement. The
--procedure should take 3 parameters (old Emp.id, new Emp.id and the
--floor number) and it will be used to update the floor table. {3 Points}
CREATE OR ALTER PROCEDURE dbo.UpdateFloorManager
    @OldEmpId INT,
    @NewEmpId INT,
    @FloorNumber INT
AS
BEGIN
    UPDATE Floor
    SET MG_ID = @NewEmpId
    WHERE MG_ID = @OldEmpId AND Number = @FloorNumber;
END;

SELECT * FROM Floor WHERE Number = 2;
EXEC dbo.UpdateFloorManager @OldEmpId = 1, @NewEmpId = 7, @FloorNumber = 2;
SELECT * FROM Floor WHERE Number = 2;

--25.Create a view AlexAndCairoEmp that displays Employee data for users
--who live in Alex or Cairo. {2 Points}
CREATE OR ALTER VIEW AlexAndCairoEmp AS
SELECT *
FROM Employee
WHERE Address LIKE '%Alex%' OR Address LIKE '%Cairo%';

SELECT * FROM dbo.AlexAndCairoEmp

--26.create a view "V2" That displays number of books per shelf {2 Points}
CREATE OR ALTER VIEW V2 AS
SELECT Shelf_code, COUNT(Id) AS Number_Of_Books
FROM Book
GROUP BY Shelf_code;

SELECT * FROM dbo.V2

--27.create a view "V3" That display the shelf code that have maximum
--number of books using the previous view "V2" {2 Points}CREATE OR ALTER VIEW V3 AS
SELECT Shelf_code
FROM V2
WHERE Number_Of_Books = (
    SELECT MAX(Number_Of_Books) FROM V2
);

SELECT * FROM dbo.V3

--28.Create a table named ‘ReturnedBooks’ With the Following Structure :
--User SSN | Book Id | Due Date | Return Date | fees
--then create A trigger that instead of inserting the data of returned book
--checks if the return date is the due date or not if not so the user must pay
--a fee and it will be 20% of the amount that was paid before. {3 Points}
CREATE TABLE ReturnedBooks (
    User_ssn VARCHAR(50),
    Book_id INT,
    Due_date DATE,
    Return_date DATE,
    Fees DECIMAL(10,2)
);

CREATE OR ALTER TRIGGER trg_CalculateFeesOnReturn
ON ReturnedBooks
INSTEAD OF INSERT
AS
BEGIN
    INSERT INTO ReturnedBooks (User_ssn, Book_id, Due_date, Return_date, Fees)
    SELECT 
        i.User_ssn,
        i.Book_id,
        i.Due_date,
        i.Return_date,
        CASE 
            WHEN i.Return_date > i.Due_date THEN 
                CAST(B.Amount * 0.2 AS DECIMAL(10,2))
            ELSE 0
        END AS Fees
    FROM inserted i
    JOIN Borrowing B ON B.Book_id = i.Book_id AND B.User_ssn = i.User_ssn AND B.Due_date = i.Due_date;
END;

--29.In the Floor table insert new Floor With Number of blocks 2 , employee
--with SSN = 20 as a manager for this Floor,The start date for this manager
--is Now. Do what is required if you know that : Mr.Omar Amr(SSN=5)
--moved to be the manager of the new Floor (id = 7), and they give Mr. Ali
--Mohamed(his SSN =12) His position . {3 Points}-- Step 1: Add new Floor 7 with temporary manager Mahmoud (Id = 20)
INSERT INTO Floor (Number, Num_blocks, MG_ID, Hiring_Date)
VALUES (7, 2, 20, GETDATE());

UPDATE Floor
SET MG_ID = 5, Hiring_Date = GETDATE()
WHERE Number = 7;

DECLARE @OldFloor INT;
SELECT @OldFloor = Number
FROM Floor
WHERE MG_ID = 5 AND Number <> 7;

IF @OldFloor IS NOT NULL
BEGIN
    UPDATE Floor
    SET MG_ID = 12, Hiring_Date = GETDATE()
    WHERE Number = @OldFloor;
END;

SELECT * FROM Floor WHERE Number = @OldFloor;

--30.Create view name (v_2006_check) that will display Manager id, Floor
--Number where he/she works , Number of Blocks and the Hiring Date
--which must be from the first of March and the end of May 2022.this view
--will be used to insert data so make sure that the coming new data must
--match the condition then try to insert this 2 rows and
--Mention What will happen {3 Point}
--Employee Id | Floor Number | Number of Blocks | Hiring Date
--   2            6               2                7-8-2023
--   4            7               1                4-8-2022CREATE OR ALTER VIEW v_2006_check AS
SELECT MG_ID, Number AS FloorNumber, Num_blocks, Hiring_Date
FROM Floor
WHERE Hiring_Date BETWEEN '2022-03-01' AND '2022-05-31'
WITH CHECK OPTION;

-- Attempt 1
INSERT INTO v_2006_check (MG_ID, FloorNumber, Num_blocks, Hiring_Date)
VALUES (2, 6, 2, '2023-08-07'); -- Will fail

-- Attempt 2
INSERT INTO v_2006_check (MG_ID, FloorNumber, Num_blocks, Hiring_Date)
VALUES (4, 7, 1, '2022-08-04'); -- Will fail

--31.Create a trigger to prevent anyone from Modifying or Delete or Insert in
--the Employee table ( Display a message for user to tell him that he can’t
--take any action with this Table) {3 Point}CREATE OR ALTER TRIGGER trg_BlockEmployeeChanges
ON Employee
INSTEAD OF INSERT, UPDATE, DELETE
AS
BEGIN
    RAISERROR('You are not allowed to Insert, Update, or Delete in the Employee table.', 16, 1);
END;

--32.Testing Referential Integrity , Mention What Will Happen When:
--A. Add a new User Phone Number with User_SSN = 50 in
--User_Phones Table {1 Point}
INSERT INTO User_phones (User_ssn, Phone_num)
VALUES (50, '01123456789'); -- Fails — Foreign key violation on Users(SSN).

--B. Modify the employee id 20 in the employee table to 21 {1 Point}
UPDATE Employee
SET Id = 21
WHERE Id = 20; -- Msg 50000, Level 16, State 1, Procedure trg_BlockEmployeeChanges, Line 6 [Batch Start Line 381]
               -- You are not allowed to Insert, Update, or Delete in the Employee table.

--C. Delete the employee with id 1 {1 Point}
DELETE FROM Employee WHERE Id = 1; -- Msg 50000, Level 16, State 1, Procedure trg_BlockEmployeeChanges, Line 6 [Batch Start Line 381]
                                   -- You are not allowed to Insert, Update, or Delete in the Employee table.

--D. Delete the employee with id 12 {1 Point}
DELETE FROM Employee WHERE Id = 12; -- Msg 50000, Level 16, State 1, Procedure trg_BlockEmployeeChanges, Line 6 [Batch Start Line 381]
                                    -- You are not allowed to Insert, Update, or Delete in the Employee table.

--E. Create an index on column (Salary) that allows you to cluster the
--data in table Employee. {1 Point}
CREATE CLUSTERED INDEX IX_Employee_Salary
ON Employee(Salary); -- Cannot create more than one clustered index on table 'Employee'. Drop the existing clustered index 'PK_Employee' before creating another.

--33.Try to Create Login With Your Name And give yourself access Only to
--Employee and Floor tables then allow this login to select and insert data
--into tables and deny Delete and update (Don't Forget To take screenshot
--to every step) {5 Points}

-- Create Login
CREATE LOGIN [MohamedOmarElsherif] WITH PASSWORD = 'Ram+1';

-- Create user in current DB
CREATE USER [MohamedOmarElsherif] FOR LOGIN [MohamedOmarElsherif];

-- Grant Access to Employee and Floor (Only SELECT/INSERT)
GRANT SELECT, INSERT ON Employee TO [MohamedElsherif];
GRANT SELECT, INSERT ON Floor TO [MohamedElsherif];

-- Deny DELETE and UPDATE
DENY DELETE, UPDATE ON Employee TO [MohamedElsherif];
DENY DELETE, UPDATE ON Floor TO [MohamedElsherif];

