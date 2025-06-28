-- Part 2

Use ITI

-- 1
Go
Create Or Alter Procedure SP_Display_NumOf_Students_PerDept
With Encryption
As
Begin
     Select S.Dept_Id, Count(S.St_Id)
	 From Student S
	 Group By S.Dept_Id
End

Exec SP_Display_NumOf_Students_PerDept

------------------------------------------------
------------------------------------------------
-- 2
Go
Use MyCompany
Go
Create Or Alter Proc SP_Check_Number_Of_Employees_In_Project_100
With Encryption
As
Begin
     Declare @NumberOfEmployees Int 
	 
     Select @NumberOfEmployees = Count(E.SSN)
	 From Employee E
	 Inner  Join dbo.Works_for W On E.SSN = W.ESSn
	 Inner Join Project P On P.Pnumber = W.Pno
	 Where P.Pnumber = 100

	 If(@NumberOfEmployees > 3)
	   Print 'The number of employees in the project 100 is 3 or more'


	 Else
	 Begin
	      Print 'The following employees work for the project 100: '

		  Declare EmpCursor Cursor For -- Declare cursor to fetch the result set
	      Select E.Fname, E.Lname
		  From Employee E
		  Inner Join Works_for W On E.SSN = W.ESSn
		  Where W.Pno = 100

		  Declare @FName Varchar(30), @LName Varchar(30) -- Variables to hold the values

		  Open EmpCursor; -- Active the cursor to start fetching
		  Fetch Next From EmpCursor Into @FName, @LName

		  While @@FETCH_STATUS = 0 -- While fetching is success the status = 0, when it = -1 this means there is no more data
		  Begin
		       Print Concat_Ws(' ', @FName, @LName)
			   Fetch Next From EmpCursor Into @FName, @LName
		  End
	 End
End


Exec SP_Check_Number_Of_Employees_In_Project_100

------------------------------------------------
------------------------------------------------
-- 3
Go
Create Or Alter Proc SP_Replace_Employee_In_Project @OldEmpNum Int, @NewEmpNum Int, @PNum Int
With Encryption 
As
Begin
     Update Works_for 
	 Set ESSn = @NewEmpNum
	 Where ESSn = @OldEmpNum And Pno = @PNum
End
Go

Select W.ESSn
From Works_for W
Where W.Pno = 100

Exec SP_Replace_Employee_In_Project 112233, 512463, 100
Exec SP_Replace_Employee_In_Project 512463, 112233, 100







