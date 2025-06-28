-- Part 02

Use ITI

-- 1
select I.Ins_Name, D.Dept_Name
from Instructor as I left outer join Department as D
on D.Dept_Id = I.Dept_Id

-- 2
select CONCAT_WS(' ', S.St_Fname, S.St_Lname) as [Student Name],
       C.Crs_Name, SC.Grade
from Student as S inner join Stud_Course as SC
     on S.St_Id = SC.St_Id
	 inner join Course as C
	 on C.Crs_Id = SC.Crs_Id
where SC.Grade is not null

-- 3
select S.St_Fname as 'Student Name', 
       Super.St_Id as 'Supervisor ID',
       CONCAT_WS(' ', Super.St_Fname, Super.St_Lname) as 'Supervisor Name',
	   Super.St_Address as 'Supervisor Address',
	   Super.St_Age as 'Supervisor Age',
	   Super.Dept_Id as 'Supervisor Dept_Id',
	   Super.St_super as 'Supervisor' 
from Student as S inner join Student as super
on super.St_Id = S.St_super

-- 4
select S.St_Id as['Student ID], S.St_Fname as [Student Full Name], D.Dept_Name as [Department Name]
from Student as S inner join Department as D
on D.Dept_Id = S.Dept_Id




