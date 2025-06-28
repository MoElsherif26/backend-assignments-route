
-- Demo file
/*
Comment with multiple line
*/

-- Variables

--1. Global Variables
Select @@VERSION
Select @@SERVERNAME
Select @@LANGUAGE

--2. Local Variables
Declare @Age Int = 10
Select @Age

Set @Age = 20
Select @Age

--camelCase-- myName myAge numberOfHours
--pascalCase-- MyName MyAge NumerOfHours

Bit        1 byte
TinyInt    1 Byte
SmallInt   2 Btyte
Int        4 Byte
BigInt     8 Byte

SmallMoney 4B.0000              -- 4 numbers after the point
Money      8B.0000              -- 4 numbers after the point
Real       4B.0000000           -- 7 numbers after the point
Float      8B.000000000000000   -- 15 numbers after the point
Dec
Decimal                         -- data type with validation

Declare @X Decimal(5,3) = 12.345
Print @X

-- 

Char(10)         -- Fixed length character
Varchar(10)      -- Variable length character
Nchar(10)        -- Like char but with unicode
Nvarchar(10)     -- Like Varchar but with unicode
Varchar(Max)     -- Up to 2GB
Nvarchar(Max)

Declare @Name Char(5) = 'Ali'
Select @Name

-- 

Date              -- MM/DD/YYYY
Time              -- HH:MM:SS.123 
Time(5)           -- HH:MM:SS.12345
SmallDateTime     -- MM/DD/YYYY HH:MM:00
DateTime          -- MM/DD/YYYY HH:MM:SS.123
DateTime(4)       -- MM/DD/YYYY HH:MM:SS.1234
DateTimeOffset    -- Date Time TimeZone

-- 
Binary    -- 0111001101
Image

--

XML
Sql_Variant  -- Like var

-- 
--To create Database
Create Database MyCompanyG02
--Select Database
Use MyCompanyG02
--To create table

Create Table Employee
(
SSN Int Primary Key Identity(1,1),
FName Varchar(15) Not Null,
LName Varchar(15),
Gender Char(1),     -- M F
BirthDate Date,
DNum Int,
Super_SSN Int References Employee(SSN) 
)

Create Table Department
(
DNum Int Primary Key Identity(10,10),
DName Varchar(15) Not Null,
Manager_SSN Int References Employee(SSN),
Hiring_Date Date
)

Create Table Department_Locations
(
DNum Int References Department(DNum),
[Location] Varchar(50)
Primary Key(DNum, [Location])
)

Create Table Project
(
PNum Int Primary Key Identity,
PName Varchar(20) Not Null,
[Location] Varchar(50) Default 'Cairo',
City Varchar(20),
DNum Int References Department(DNum)
)

Create Table Dependents
(
[Name] Varchar(20),
BirthDate Date,
Gender Char(1),
Essn Int References Employee(SSN)
Primary Key([Name],Essn)
)

Create Table Employee_Projects
(
Essn Int References Employee(SSN),
PNum Int References Project(PNum),
NumOfHours TinyInt
Primary Key(Essn,PNum)
)

Alter Table Employee 
Add Constraint FK_Work Foreign Key(DNum) References Department(DNum)


Alter Database MyCompanyG02
Modify Name = Test

Alter Database Test
Modify Name = MyCompanyG02

Alter Table Employee
Add Test Int

Alter Table Employee
Add Constraint UQ_Test Unique(Test)

Alter Table Employee
Drop Constraint UQ_Test

Alter Table Employee
Alter Column Test TinyInt

Alter Table Employee
Drop Column Test

Drop Table Department

Drop Database MyCompanyG02