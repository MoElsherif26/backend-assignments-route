USE ITI

-- 1
GO
Create Or Alter Function GetMonthFromDate(@Date Date)
Returns int
As
Begin 
     Declare @Month varchar(20)
	 Select @Month = MONTH(@Date)
	 Return @Month
End
GO
Select dbo.GetMonthFromDate('05-04-2022')
----------------------------------------------------------
GO
Create Or Alter Function GetMonthFromDate2(@Date Date)
Returns varchar(20)
As
Begin
    Declare @Month varchar(20)
	Select @Month = DateName(MONTH, @Date)
	Return @Month
End
GO
Select dbo.GetMonthFromDate2('05-04-2022')
----------------------------------------------------------
GO
Create Or Alter Function GetMonthFromDate3(@Date Date)
Returns int
As
Begin
     Declare @Month int
	 Select @Month = DatePart(MONTH, @Date)
	 Return @Month
End
GO
Select dbo.GetMonthFromDate3('05-04-2022')
----------------------------------------------------------
----------------------------------------------------------

-- 2
-- required: Multi statements table-valued function
GO
Create Or Alter Function InsertRangeIntoTable(@Start int, @End int)
Returns @Range Table
(
Number int Null,
ErrorMessage varchar(Max) Null
)
As
Begin
     if(@End < @Start)
	 Begin
	   Insert Into @Range(ErrorMessage)
	   Select 'This whould end to uninfinity loop, make sure the start is bigger than the end'
	End

	Else If(@End = @Start)
	Begin
	   Insert Into @Range(ErrorMessage)
	   Select 'Start number equal to end, there are no numbers in between'
	End

	Else
	Begin
	     Declare @Counter int = @Start + 1;
	     While @Counter < @End
		 Begin
		      Insert Into @Range(Number)
			  Select @Counter

			  Set @Counter += 1
		 End
	End
	Return;
End
GO

select * from dbo.InsertRangeIntoTable(3, 7)
select * from dbo.InsertRangeIntoTable(3, 3)
select * from dbo.InsertRangeIntoTable(7, 3)

---------------------------------------------------
-- Other Solutions:
GO
Create Or Alter Function GetRange(@Start int, @End int)
Returns varchar(max)
As 
Begin
Declare @Range varchar(Max)

    If(@Start > @End)
	Select @Range = 'This would in an infinity loop'

	Else If(@Start = @End)
	Select @Range = 'No range since the start equal to end'

	Else
	Begin
	     Declare @Counter int = @Start + 1
		 While @Counter < @End
		 Begin
		      Select @Range = CONCAT_WS(', ', @Range, @Counter)
			  Set @Counter += 1
		 End
	End
	Return @Range
End
GO

Select dbo.GetRange(3, 7)
Select dbo.GetRange(3, 3)
Select dbo.GetRange(7, 3)
-------------------------------------------------------
GO
Create or Alter Procedure SP_GetRange @Start int, @End int
with Encryption
As
Begin
     If(@Start > @End)
	 Begin
	      Print 'This would end to and infinity loop'
		  Return;
	 End

	 Else If(@Start = @End)
	 Begin
	      Print 'There is no range since the start is eaual to end'
		  Return;
	 End

	 Else
	 Begin
	      Create Table Numbers(Number int);
		  Declare @Counter int = @Start + 1
		  
		  while @Counter < @End
		  Begin
		       Insert Into Numbers Values(@Counter)
			   Set @Counter += 1
		  End
		  select * from Numbers
	 End
End
GO

Exec dbo.SP_GetRange 3, 7

-------------------------------------------------------
-------------------------------------------------------
 -- 3
GO
Create Or Alter Function Get_Student_With_Dept_ByStudentNo(@Id int)
Returns varchar(Max)
As
Begin
     Declare @StudentInfo varchar(Max)
     Select @StudentInfo = CONCAT_WS(' ', S.St_Fname, S.St_Lname, D.Dept_Name)
	 from Student S
	 inner join Department D on D.Dept_Id = S.Dept_Id
	 Where S.St_Id = @Id

	 Return @StudentInfo
End
GO
select * from Student
Select dbo.Get_Student_With_Dept_ByStudentNo(7)
-------------------------------------------------------------
Go
Create Or Alter Function Get_Student_With_Dept_ByStudentNo2(@Id int)
Returns Table
As
Return
(
Select Concat_WS(' ', S.St_Fname, S.St_Lname) as 'Full Name', D.Dept_Name
from Student S
inner join Department D on D.Dept_Id = S.Dept_Id
Where S.St_Id = @Id
)
Go

select * From dbo.Get_Student_With_Dept_ByStudentNo2(7)
-------------------------------------------------------------
Go 
Create Or Alter Function Get_Student_With_Dept_ByStudentNo3(@Id Int)
Returns @StudentInfo Table
(
StudName Varchar(20),
DeptName Varchar(20)
)
As
Begin
     Insert Into @StudentInfo
	 Select CONCAT_WS(' ', S.St_Fname, S.St_Lname), D.Dept_Name 
	 From Student S
	 Inner Join Department D on D.Dept_Id = S.Dept_Id
	 where S.St_Id = @Id

	 Return;
End
Go

select * from dbo.Get_Student_With_Dept_ByStudentNo3(7)
-------------------------------------------------------------
-------------------------------------------------------------
-- 4
Go
Create Or Alter Function Check_First_And_LastName_Of_Student(@Id int)
Returns Varchar(100)
As
Begin
     Declare @FName varchar(20), @LName varchar(20), @Result Varchar(100)
	 Select @FName = S.St_Fname, @LName = S.St_Lname
	 From Student S
	 Where S.St_Id = @Id

	 If(@FName is Null and @LName is Null)
	   Set @Result = 'First name & last name are null.'

	Else If(@FName is Null)
	   Set @Result = 'first name is null.'

	Else If(@LName is Null)
	   Set @Result = 'last name is null.'

	Else 
	   Set @Result = 'First name & last name are not null.'

	Return @Result
End
Go
select * from Student
Select dbo.Check_First_And_LastName_Of_Student(7) -- First name & last name are not null.
Select dbo.Check_First_And_LastName_Of_Student(13) -- last name is null.

-------------------------------------------------------------
-------------------------------------------------------------
-- 5
Go
Create Or Alter Function DisplayManagerInfo(@Format int)
Returns Table
As
Return
(
Select D.Dept_Name, I.Ins_Name, CONVERT(Varchar(Max), D.Manager_hiredate, @Format) as [Manager Hiring Date]
From Department D
Inner Join Instructor I on I.Ins_Id = D.Dept_Manager
)
Go

Select * From Dbo.DisplayManagerInfo(101)
Select * From Dbo.DisplayManagerInfo(102)
-------------------------------------------------------------
Go
Create Or Alter Function DisplayManagerInfo2(@Format Int)
Returns @ManagerInfo Table
(
DeptName Varchar(30),
ManagerName Varchar(30),
HiringDate Varchar(30)
)
As
Begin
     Insert Into @ManagerInfo
     Select D.Dept_Name, I.Ins_Name, CONVERT(Varchar(Max), D.Manager_hiredate, @Format)
	 From Instructor I
	 Inner Join Department D on I.Ins_Id = D.Dept_Manager
	 Return;
End
Go

Select * From Dbo.DisplayManagerInfo2(111)
Select * From Dbo.DisplayManagerInfo2(103)

-------------------------------------------------------------
-------------------------------------------------------------
 -- 6
 Go
Create Or Alter Function Check_FirstName_And_LastName(@FisrtName Varchar(50), @LastName Varchar(50))
Returns @Result Table
(
Message Varchar(100)
)
As
Begin
     If (ISNULL(@FisrtName, ' ') = ' ' and ISNULL(@LastName, ' ') = ' ')
	    Insert Into @Result Values ('First name & last name are null')

	 Else If (ISNULL(@FisrtName, ' ') = ' ' )
	    Insert Into @Result Values ('first name is null')

	Else If (ISNULL(@LastName, ' ') = ' ' )
	    Insert Into @Result Values ('last name is null')

	Else
	    Insert Into @Result Values ('First name & last name are not null')

     Return;
End
Go

Select * from dbo.Check_FirstName_And_LastName('Ali', 'Mohamed')
Select * from dbo.Check_FirstName_And_LastName('Ali', Null)
Select * from dbo.Check_FirstName_And_LastName(Null, 'Mohamed')
Select * from dbo.Check_FirstName_And_LastName(Null, Null)

-------------------------------------------------------------
-------------------------------------------------------------
-- 7
Use MyCompany
Go
Create Or Alter Function Get_Employees_By_ProjectId(@ProjectId Int)
Returns Table
As
Return
(
Select E.*, W.Hours, P.* 
From Employee E
Inner Join Works_for W On E.SSN = W.ESSn
Inner Join Project P On P.Pnumber = W.Pno
Where W.Pno = @ProjectId
)
Go

Select * From dbo.Get_Employees_By_ProjectId(100) 


