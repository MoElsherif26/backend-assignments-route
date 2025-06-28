-- Hospital Database
Create Database Hospital
Use Hospital

Create Table Patient
(
Id Int Primary Key Identity(1,1),
[Name] Varchar(20) Not Null,
DOB Date,
Ward_Id Int, --Foreign Key reference Ward
Con_Id Int -- Foreign Key reference Consultant
)

Create Table Ward
(
Id Int Primary Key Identity(1,1),
[Name] Varchar(20) Not Null,
Nurse_Num Int -- Foreign Key reference Nurse  
)

Create Table Nurse
(
Number Int Primary Key Identity(1,1),
[Name] Varchar(20) Not Null,
[Address] Varchar(40),
Ward_Id Int References Ward(Id)
)

Create Table Consultant
(
Id Int Primary Key Identity(1,1),
[Name] Varchar(20) Not Null 
)

Create Table Patient_Con
(
Con_Id Int References Consultant(Id),
Pat_Id Int References Patient(Id)
Primary Key(Con_Id,Pat_Id)
)

Create Table Drugs
(
Code Int Primary Key Identity(1,1),
Dosage Dec(7,3)
)

Create Table Drug_Brand
(
Code Int References Drugs(Code),
Brand Varchar(20)
Primary Key(Code,Brand)
)

Create Table Nurse_Drug_Patient
(
Nur_Num Int References Nurse(Number),
Drug_Code Int References Drugs(Code),
Pat_Id Int References Patient(Id),
[Date] Date,
[Time] Time(5),
Dosage Dec(7,3)
Primary Key(Pat_Id,[Date],[Time])
)

Alter Table Patient
Add Constraint FK_Ref_Ward Foreign Key(Ward_Id) References Ward(Id)

Alter Table Patient
Add Constraint FK_Ref_Con Foreign Key(Con_Id) References Consultant(Id)

Alter Table Ward
Add Constraint FK_Ref_Nur Foreign Key(Nurse_Num) References Nurse(Number)