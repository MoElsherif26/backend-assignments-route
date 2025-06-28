-- Part1

Use ITI

Go
Create Or Alter Trigger PreventInsertOnDepartment
On Department
With Encryption
Instead Of Insert
As
  Print 'You can''t insert in this table.'
Go

Insert Into Department(Dept_Name, Dept_Desc, Dept_Location)
Values('IT', 'Information Technology', 'Alex')
-------------------------------------------------------------------------

Create Table StudentAudit
(
Server_User_Name Varchar(Max),
[Date] Date,
Note Varchar(Max)
);

Alter Table StudentAudit
Alter Column [Date] DateTime

Go
Create Or Alter Trigger StudentAuditForInsert
On Student
With Encryption
After Insert
As
Begin
  Insert Into StudentAudit
  Select SUSER_NAME(),
         GETDATE(), 
		 CONCAT_WS(' ', SUSER_NAME(),
	                    'Insert New Row with Key = ',
				        St_Id,
						'in table Student'
				  )
	From inserted
End
Go

Insert Into Student(St_Id, St_Fname, St_Lname, St_Address, St_Age)
Values(9944, 'Mohamed', 'Omar', 'Cairo', 20)

select * from StudentAudit

Go
Create Or Alter Trigger StudentAuditForDelete
On Student
With Encryption
Instead Of Delete
As
Begin
     Insert Into StudentAudit
	 Select SUSER_NAME(),
	        GetDate(),
			CONCAT_WS(' ', SUSER_NAME(),
			          'try to delete Row with Id = ',
					  St_Id)
	from deleted
End
Go

Delete From Student Where St_Id = 9944

select * from StudentAudit


---------------------------------------------------------------------------
---------------------------------------------------------------------------
-- Part2

Use MyCompany

Go
Create Or Alter Trigger PreventInsertionInMarchForEmployee
On Employee
With Encryption
Instead Of Insert
As
Begin
     If(DATENAME(MONTH, GETDATE()) != 'March')
	 Begin
	      Insert Into Employee
	      Select * From inserted
		  Print 'Data added successfully'
	 End

	 Else
	     Print 'You can''t insert data for today.'

End
Go

Insert Into Employee(SSN, Fname, Lname, Salary, Address, Bdate, Sex, Dno)
Values (768222, 'Mostafa', 'Amin', 5500, 'Nasr city', '1999-7-12', 'M', 10),
       (568111, 'Ahmed', 'Yasser', 3500, 'Al fayoum', '2002-9-4', 'M', 10),
       (968532, 'Seif', 'Omar', 4000, '6 October', '2000-9-12', 'M', 10)

---------------------------------------------------------------------------

Use IKEA_Company

Create Table ProjectAudit_Budget
(
ProjectNo Int,
UserName Varchar(Max),
ModificationDate DateTime,
Budget_Old Int,
Budget_new Int
)

Go
Create Or Alter Trigger ProjectAuditForBudget
On HR.Project
With Encryption
For Update
As
Begin
      Declare @OldBudget int, @NewBudget int -- variables to store the values of the budget
	  
	  Select * from inserted
	  Select * from deleted
	  Select @OldBudget = Budget From deleted
	  Select @NewBudget = Budget From inserted
	  -- If the two values are different then the modification was on Budget column
	  If(@OldBudget != @NewBudget)
	  Begin
	       Insert Into ProjectAudit_Budget
		   Select ProjectNo, USER_NAME(), GETDATE(), @OldBudget, @NewBudget
		   From inserted
	  End
End
Go

Update HR.Project  -- The change is on column ProjectName, so the Project_Audit table will not be affected 
Set ProjectName = 'CS'
Where ProjectNo = 5

Select * From ProjectAudit_Budget

Update HR.Project
Set Budget = 3000
where ProjectNo = 3

Select * From ProjectAudit_Budget
