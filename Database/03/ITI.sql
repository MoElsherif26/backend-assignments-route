Create Database ITI
Use ITI

-- ITI Database

Create Table Students
(
Id Int Primary Key Identity(1,1),
FName Varchar(20) Not Null,
LName Varchar(20) Not Null,
Age Int Not Null Check(Age Between 15 And 40),
[Address] Varchar(40),
Dept_Id Int -- Foreign Key References Departments
)

Create Table Departments
(
Id Int Primary Key Identity(1,1),
[Name] Varchar(20) Not Null,
Hiring_Date Date,
Ins_Id Int -- Foreign Key References Instructors
)

Create Table Instructors
(
Id Int Primary Key Identity(1,1),
[Name] Varchar(20) Not Null,
[Address] Varchar(40),
Bonus Int,
Salary Int Check(Not (Salary < 0)),
Hour_Rate Dec(7,3),
Dept_Id Int -- Foreign Key References Departments
)

Create Table Courses
(
Id Int Primary Key Identity(1,1),
[Name] Varchar(20) Not Null,
Duration Int Check(Duration > 0),
[Description] Varchar(40),
Top_Id Int -- Foreign Key References Topics
)

Create Table Topics
(
Id Int Primary Key Identity(1,1),
[Name] Varchar(20) Not Null
)

Create Table Stud_Course
(
Stud_Id Int References Students(Id),
Course_Id Int References Courses(Id)
Primary Key(Stud_Id,Course_Id)
)

Create Table Course_Instructor
(
Course_Id Int References Courses(Id),
Ins_Id Int References Instructors(Id)
Primary Key (Course_Id,Ins_Id)
)

Alter Table Students
Add Constraint FK_Ref_Dept Foreign Key(Dept_Id) References Departments(Id) 

Alter Table Departments 
Add Constraint FK_Ref_Ins Foreign Key(Ins_Id) References Instructors(Id) 

Alter Table Instructors
Add Constraint FK_Ref_Departments Foreign Key(Dept_Id) References Departments(Id)

Alter Table Courses
Add Constraint FK_Ref_Topics Foreign Key(Top_Id) References Topics(Id)
