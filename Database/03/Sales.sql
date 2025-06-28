Create Database Sales
Use Sales

-- Sales Database

Create Table Sales_Office
(
Number Int Primary Key Identity(1,1),
[Location] Varchar(20) Not Null,
Emp_Id Int -- Foreign Key References Employee
)

Create Table Employee
(
Id Int Primary Key Identity(1,1),
[Name] Varchar(20) Not Null,
Off_Number Int References Sales_Office(Number)
)

Create Table Property
(
Id Int Primary Key Identity(1,1),
[Address] Varchar(20),
City Varchar(20), 
[State] Varchar(20),
Code Int,
Off_Number Int References Sales_Office(Number)
)

Create Table [Owner]
(
Id Int Primary Key Identity(1,1),
[Name] Varchar(20) Not Null 
)

Create Table Prop_Owner
(
Prop_Id Int References Property(Id),
Own_Id Int References [Owner](Id)
Primary Key(Prop_Id, Own_Id)
)

Alter Table Sales_Office
Add Constraint FK_Ref_Emp Foreign Key(Emp_Id) References Employee(Id)
