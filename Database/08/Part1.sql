-- Part 1
Use ITI

--1
Go
Create Or Alter View Student_Course_With_Grade_View
With Encryption
As
  Select S.St_Fname, S.St_Lname, C.Crs_Name
  From Student S
  Inner Join dbo.Stud_Course SC On S.St_Id = SC.St_Id
  Inner Join Course C On C.Crs_Id = SC.Crs_Id
  Where SC.Grade > 50 With Check Option
Go
Select * From Student_Course_With_Grade_View

-------------------------------------------------------------------------
-------------------------------------------------------------------------

-- 2
Go
Create Or Alter View Display_Manager_Topic_View(Manager, Topic)
With Encryption
As
  Select distinct I.Ins_Name, T.Top_Name
  From Instructor I
  Inner Join Department D On I.Ins_Id = D.Dept_Manager -- The result from this join to get instructors who are managers
  Inner Join Ins_Course IC on I.Ins_Id = IC.Ins_Id     -- Relate the managers to thier courses
  Inner Join Course C On C.Crs_Id = IC.Crs_Id          -- Relate the Courses
  Inner Join Topic T On T.Top_Id = C.Top_Id            -- Relate topic to the course 
Go

Select * 
From Display_Manager_Topic_View

-------------------------------------------------------------------------
-------------------------------------------------------------------------
-- 3
Go
Create Or Alter View Instructor_In_Specific_Dept_View
With SchemaBinding, Encryption
As
  Select I.Ins_Name, D.Dept_Name
  From dbo.Instructor I
  Inner Join dbo.Department D On D.Dept_Id = I.Dept_Id
  Where D.Dept_Name In ('SD', 'Java')
Go

Select * From Instructor_In_Specific_Dept_View

/*
What is SCHEMABINDING?
The view is tied (bound) to the structure (schema) of the tables it references.
You cannot modify the structure (like dropping or changing columns) of the underlying tables while this view exists.

Benefits of SCHEMABINDING:
1- Prevents accidental changes
You cannot drop or alter a column used in the view
❌ Action	                                   ❌ Example
Drop the column used in the view	           ALTER TABLE Instructor DROP COLUMN Ins_Name
Rename the column used in the view	           sp_rename 'Instructor.Ins_Name', 'Name', 'COLUMN'
Change the column type (if incompatible)       ALTER TABLE Instructor ALTER COLUMN Ins_Name INT
You can update, insert, delete data — just not drop or alter columns used by the view.

2-  Allows indexing the view. You must use SCHEMABINDING to create an indexed view

3- Forces explicit references, You must fully qualify table names (dbo.TableName)
You must fully qualify table names (dbo.Instructor not just Instructor)
You cannot use * — you must explicitly list columns.
You can’t reference other views that don’t have schema binding.

4- Improves stability, Ensures that dependent objects don't break silently
The Problem (Without SCHEMABINDING):
Let’s say you have a view:
							CREATE VIEW vw_InstructorNames
							AS
							SELECT Ins_Name FROM Instructor;
Then later, someone changes the table: ALTER TABLE Instructor DROP COLUMN Ins_Name;
SQL Server allows it — the column is removed.
Now your view is broken — but you won’t know it immediately.
The view still exists in the database.
But the next time someone tries to use it, they’ll get an error (Invalid column name 'Ins_Name'). 
That’s what we mean by: “it breaks silently” — no warning or error when the table is changed.
If the view is created with SCHEMABINDING: Now, SQL Server will block any attempt to drop or change 
ALTER TABLE Instructor DROP COLUMN Ins_Name; => ERROR: Cannot drop column because it's referenced by a schemabound view
*/

-------------------------------------------------------------------------
-------------------------------------------------------------------------
-- 4
Go
Create Or Alter View StudentInAlexOrCairo_View
With Encryption
As
  Select *
  From Student S
  Where S.St_Address = 'Cairo' Or S.St_Address = 'Alex' With Check Option
Go

Select * From StudentInAlexOrCairo_View

Update StudentInAlexOrCairo_View 
Set St_Address = 'Tanta'
where St_Address = 'Alex'

-------------------------------------------------------------------------
-------------------------------------------------------------------------
-- 5
Use MyCompany

Go
Create Or Alter View Project_And_Number_Of_Employees_Working_On_It_View
With Encryption
As
  Select P.Pname, COUNT(E.SSN) As 'Number Of Employees'
  From Employee E
  Inner Join dbo.Works_for W On E.SSN = W.ESSn
  Inner Join dbo.Project P On P.Pnumber = W.Pno
  Group by P.Pname
Go

Select * From Project_And_Number_Of_Employees_Working_On_It_View


-------------------------------------------------------------------------
-------------------------------------------------------------------------
-- use IKEA_Company_DB:
Use IKEA_Company

-- 1

Go
Create Or Alter View v_clerk
With Encryption
As
  Select E.EmpNo, P.ProjectNo, W.Enter_Date
  From Hr.Employee E
  Inner Join dbo.Works_on W On E.EmpNo = W.EmpNo
  Inner Join HR.Project P On P.ProjectNo = W.ProjectNo
  Where W.Job = 'Clerk'
Go

Select * From v_clerk

-------------------------------------------------------------------------
-------------------------------------------------------------------------
--2

Go
Create Or Alter View v_without_budget
With Encryption
As
  Select P.ProjectNo, P.ProjectName
  From HR.Project P
Go

Select * From v_without_budget

-------------------------------------------------------------------------
-------------------------------------------------------------------------
--3
Go
Create Or Alter View v_count(ProjectName, NumOfJobs)
With Encryption
As
  Select P.ProjectName, COUNT(W.Job)
  From HR.Project P 
  Inner Join dbo.Works_on W On P.ProjectNo = W.ProjectNo
  Group By P.ProjectName, W.ProjectNo
Go

Select * From v_count

-------------------------------------------------------------------------
-------------------------------------------------------------------------
--4

Go
Create Or Alter View v_project_p2
With Encryption
As
  Select v.EmpNo
  From v_clerk v
  Where v.ProjectNo = 2
Go

Select * From v_project_p2

-------------------------------------------------------------------------
-------------------------------------------------------------------------
-- 5
Go
Create Or Alter View v_without_budget
With Encryption
As
  Select P.*
  From HR.Project P
  Where P.ProjectNo = 1 Or P.ProjectNo = 2
Go

Select * From v_without_budget

-------------------------------------------------------------------------
-------------------------------------------------------------------------
-- 6
Drop View v_count, v_clerk

-------------------------------------------------------------------------
-------------------------------------------------------------------------
-- 7
Go
Create Or Alter View DisplayEmp
With Encryption 
As
  Select E.EmpNo, E.EmpLname
  From HR.Employee E
  Where E.DeptNo = 2
Go

Select * From DisplayEmp
-------------------------------------------------------------------------
-------------------------------------------------------------------------
-- 8
Go
Create Or Alter View DisplayEmployee
With Encryption
As
  Select *
  From DisplayEmp
  Where EmpLname like '%j%'
Go

Select * From DisplayEmployee

-------------------------------------------------------------------------
-------------------------------------------------------------------------
-- 9
Go
Create Or Alter View v_dept
With Encryption
As
  Select D.DeptNo, D.DeptName
  From dbo.Department D
Go

Select * From v_dept
-------------------------------------------------------------------------
-------------------------------------------------------------------------
-- 10
Go
Insert Into v_dept(DeptNo, DeptName)
Values(4, 'Development')
Go

-------------------------------------------------------------------------
-------------------------------------------------------------------------
-- 11
Go
create Or Alter View v_2006_check
With Encryption
As
  Select E.EmpNo, E.EmpFname, E.EmpLname, P.ProjectNo, P.ProjectName
  From HR.Employee E
  Inner Join dbo.Works_on W On E.EmpNo = W.EmpNo
  Inner Join HR.Project P On P.ProjectNo = W.ProjectNo
  Where Year(W.Enter_Date) = 2006  With Check Option
Go

Select * From v_2006_check
