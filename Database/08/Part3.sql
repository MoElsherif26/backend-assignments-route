-- Part 3

-- 1
Go
Create Or Alter Proc SP_GetSum @Start Int, @End Int
With Encryption
As
Begin
     Declare @Counter Int = @Start, @Sum Int = 0
	 
	 While @Counter <= @End
	 Begin
	      Set @Sum += @Counter
	      Set @Counter += 1
	 End
	 Print @Sum
End
Go

Exec SP_GetSum 1, 5
---------------------------------------------------
---------------------------------------------------
-- 2
Go
Create Or Alter Proc SP_Calculate_Circle_Are @Radius Int
With Encryption
As
Begin
     Print 2 * 3.14 * (@Radius * @Radius)
End
Go

SP_Calculate_Circle_Are @Radius = 2

---------------------------------------------------
---------------------------------------------------
-- 3
Go
Create Or Alter Proc SP_Age_Category @Age Int
With Encryption
As
Begin
     If(@Age < 18)
	   Print 'Child'
	Else If(@Age Between 18 And 59)
	   Print 'Adult'
	Else 
	   Print 'Senior'
End
Go

Exec SP_Age_Category @Age = 12
Exec SP_Age_Category @Age = 59
Exec SP_Age_Category @Age = 70

---------------------------------------------------
---------------------------------------------------
-- 4
Go
Create Or Alter Proc SP_Operations @Numbers Varchar(Max)
With Encryption 
As
Begin
     With NumbersTable As
	 (
	  Select TRY_CAST(Value As Int) As Number
	  From string_split(@Numbers, ',')
	  Where TRY_CAST(Value As Int) Is Not Null 
	 )
	 Select Max(Number) As MaxVal, Min(Number) As MinVal, avg(Number) As AvgVal
	 From NumbersTable

End
Go

Exec SP_Operations @Numbers = '5, 10, 15, 20, 25'