-- Part 04

Use ITI

-- 1
select COUNT(S.St_Age) as 'Number Of Students Who Has Age'
from Student as S

-- 2
select S.St_Id as 'Student ID',
       IsNull(S.St_Fname, 'FName') + ' ' + ISNULL(S.St_Lname, 'LName') as 'Student Full Name',
	   D.Dept_Name as 'Department Name'
from Student S join Department D
on D.Dept_Id = S.Dept_Id

-- 3
select Ins_Id, ISNULL(CONVERT(varchar(20),Salary), '0000') as 'Salary'
from Instructor

-- 4
select MAX(Salary) as 'Max Salary', MIN(Salary) as 'Min Salary'
from Instructor

-- 5
select AVG(Salary) as 'Average Salary'
from Instructor

select SUM(Salary)/COUNT(Salary) as 'Average Salary'
from Instructor

-- 6
select *
from Instructor
where Salary < (Select AVG(Salary) from Instructor)

-- 7
select Dept_Name
from Department
where Dept_Id = (select Dept_Id
                from Instructor
                where Salary = (select MIN(Salary) from Instructor))

select Top 1 D.Dept_Name
from Instructor as I join Department as D
on D.Dept_Id = I.Dept_Id
where I.Salary is not null
order by I.Salary asc

select D.Dept_Name
from Instructor as I inner join Department as D
on D.Dept_Id = I.Dept_Id
where I.Salary = (select MIN(Salary) from Instructor)



-- 8
select distinct Top 2 Salary
from Instructor
order by Salary desc

select Salary
from Instructor
where Salary in (
                  select distinct Top 2 Salary
				  from Instructor 
				  order by Salary desc)

