
-- Airline database
Create Database Airline
Use Airline

Create Table Aircraft
(
Id Int Primary Key Identity(1,1),
Capacity Int Not Null Check(Not (Capacity < 1)),
Model Varchar(20),
Maj_Pilot Varchar(20),
Assistant  Varchar(20),
Host1 Varchar(20),
Host2 Varchar(20),
AL_Id Int -- Foreign Key Reference Airline (Id)
)

Create Table Airline
(
Id Int Primary Key Identity(1,1),
[Name] Varchar(20),
[Address] Varchar(30),
Cont_Person Varchar(20)
)

Create Table Airline_Phones
(
AL_Id Int References Airline(Id),
Phones Varchar(20) Not Null
Primary Key(AL_Id, Phones)
)

Create Table [Transaction]
(
Id Int Primary Key Identity(1,1),
[Description] Varchar(30),
Amout Decimal(7,3) Check(Not (Amout < 1)), 
[Date] Date,
AL_Id Int References Airline(Id)
)

Create Table Employee
(
Id Int Primary Key Identity(1,1),
[Name] Varchar(20) Not Null,
[Address] Varchar(30),
Gender Char(1) Check(Gender In ('M', 'F')),
DB_Year Int Check(DB_Year >= 1980 And DB_Year <= 2015),
DB_Month Int Check(DB_Month >= 1 And DB_Month <= 12),
DB_Day Int Check(DB_Day >= 1 And DB_Day <= 31),
AL_Id Int References Airline(Id)
)

Create Table Emp_Qualifications
(
Emp_Id Int References Employee(Id),
Qualification Varchar(20)
Primary Key (Emp_Id, Qualification)
)

Create Table [Route]
(
Id Int Primary Key Identity(1,1),
Distance Int Check(Not (Distance < 1)),
Origin Varchar(20),
[Classification] Varchar(20),
Destination Varchar(20)
)

Create Table Aircraft_Routes
(
AC_Id Int References Aircraft(Id),
Route_Id Int References [Route](Id),
Price Decimal(7,3)
Primary Key(AC_Id, Route_Id)
)

Alter Table Aircraft 
Add Constraint AC_FK_Ref_AL Foreign key(AL_Id) References Airline(Id) 