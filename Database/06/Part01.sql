-- Part 1

Use ITI

-- q1
select I.*
from Instructor I
where I.Salary < (select AVG(Salary)
                  from Instructor
				  )
------------------------------------------------
with AvgSalary as(
                  select AVG(Salary) as avgSal
				  from Instructor
				  where Salary is not null)
select I.*, A.avgSal
from Instructor I, AvgSalary A
where I.Salary < A.avgSal
------------------------------------------------
------------------------------------------------

--q2
select Top(1) D.Dept_Id, D.Dept_Name, I.Dept_Id, I.Salary, ROW_NUMBER() over (order by I.Salary) as Ranking
from Instructor I
inner join Department D on D.Dept_Id = I.Dept_Id
where I.Salary is not null
------------------------------------------------
select D.Dept_Id, D.Dept_Name
from Instructor I
inner join Department D
on D.Dept_Id = I.Dept_Id
where I.Salary = (select MIN(I.Salary)
                  from Instructor I
                  where I.Salary is not null)
------------------------------------------------
select Top(1) D.Dept_Id, D.Dept_Name
from Instructor I
inner join Department D
on D.Dept_Id = I.Dept_Id
where I.Salary is not null
order by I.Salary
------------------------------------------------
select *
from Department
where Dept_Id in (select Top(1) I.Dept_Id
                 from Instructor I
                 where I.Salary is not null
                 order by I.Salary)
-------------------------------------------------
select Dept_Contains_Min_Ins_Salary.Dept_Id, Dept_Contains_Min_Ins_Salary.Dept_Name
from (select D.Dept_Id, D.Dept_Name, ROW_NUMBER() over(order by I.Salary) as Ranking
      from Instructor I 
      inner join Department D on D.Dept_Id = I.Dept_Id
      where I.Salary is not null) as Dept_Contains_Min_Ins_Salary
where Dept_Contains_Min_Ins_Salary.Ranking = 1

------------------------------------------------
------------------------------------------------

-- q3
select I1.Ins_Id, I1.Ins_Name, I1.Salary
from Instructor I1
where I1.Salary is not null and  2 > 
									  (select COUNT(Salary)
									   from Instructor I2
									   where I1.Salary < I2.Salary)
------------------------------------------------
select *
from (select Salary, ROW_NUMBER() over (order by Salary desc) as Ranking							
      from Instructor
      where Salary is not null) as Ins_Salries
where Ins_Salries.Ranking in (1, 2)
------------------------------------------------
select top(2) Salary
from Instructor
where Salary is not null
order by Salary desc
------------------------------------------------
with Ins_Salaries as (select Salary, ROW_NUMBER() over (order by Salary desc) as Ranking							
					  from Instructor
					  where Salary is not null)
select Ins_Salaries.Salary
from Ins_Salaries
where Ins_Salaries.Ranking <=2
------------------------------------------------
------------------------------------------------

Use MyCompany

-- q4
select Top(1) D.Dnum, D.Dname
from Employee E
inner join Departments D on D.Dnum = E.Dno
order by E.SSN asc
------------------------------------------------
with Min_EmpId_Dept as (select Top(1) E.Dno
                  from Employee E
				  where E.Dno is not null
				  order by E.SSN)
select D.Dnum, D.Dname
from Departments D, Min_EmpId_Dept MED
where D.Dnum = MED.Dno
------------------------------------------------
select Min_EmpId_Dept.Dnum, Min_EmpId_Dept.Dname
from (select D.Dnum, D.Dname, ROW_NUMBER() over (order by E.SSN asc) as Ranking
		from Employee E
		inner join Departments D on D.Dnum = E.Dno) as Min_EmpId_Dept
where Min_EmpId_Dept.Ranking = 1
------------------------------------------------
select D.Dnum, D.Dname
from Departments D
where D.Dnum = (select Top(1) E.Dno
                  from Employee E
				  where E.Dno is not null
				  order by E.SSN)
------------------------------------------------
------------------------------------------------

-- q5
-- If the query means the managers of the employees who have no dependents:
select M.*, D.*
from Employee E 
inner join Employee M on M.SSN = E.Superssn
left join Dependent D on M.SSN = D.ESSN
where D.ESSN is null
------------------------------------------------
select M.*
from Employee E
inner join Employee M on M.SSN = E.Superssn
where not exists(select 1
                 from Dependent D
			     where M.SSN = D.ESSN)
------------------------------------------------
select M.*
from Employee E
inner join Employee M on M.SSN = E.Superssn
where M.SSN not in (select D.ESSN
                from Dependent D)
------------------------------------------------
--  If the query means the managers of the departments who have no dependents:
select M.*
from Employee M
inner join Departments D on M.SSN = D.MGRSSN
left join Dependent Dep on M.SSN = Dep.ESSN
where Dep.ESSN is null
------------------------------------------------
select M.*
from Employee M
inner join Departments D on M.SSN = D.MGRSSN
where not exists(select 1
                  from Dependent dep
				  where dep.ESSN = M.SSN)
------------------------------------------------
select M.*
from Employee M
inner join Departments D on M.SSN = D.MGRSSN
where M.SSN not in (select D.ESSN
                from Dependent D)
------------------------------------------------
------------------------------------------------

-- q6
select D.Dnum, D.Dname, AVG(Salary) as Dept_Avg_Salary, COUNT(E.SSN) as NumperOfEmployees
from Employee E 
inner join Departments D on D.Dnum = E.Dno
where Salary is not null
group by D.Dnum, D.Dname
having AVG(Salary) < (select AVG(Salary)
                      from Employee)
------------------------------------------------
select distinct *
from (select D.Dnum,
       D.Dname,
	   COUNT(E.SSN) over (partition by D.Dnum) as EmpCount,
	   AVG(E.Salary) over (partition by D.Dnum) as DeptAvgSalary
	   from Employee E 
	   inner join Departments D on D.Dnum = E.Dno) as Sub
where Sub.DeptAvgSalary < (select AVG(Salary)
                           from Employee)
------------------------------------------------
------------------------------------------------

-- q7
select distinct E1.Salary
from Employee E1
where E1.Salary is not null and 2 > (select COUNT(*)
									 from Employee E2
									 where E2.Salary > E1.Salary)
------------------------------------------------
with RankedSalary as (select Salary, ROW_NUMBER() over (order by Salary desc) as Ranking
                      from Employee)
select Salary
from RankedSalary
where Ranking <= 2
------------------------------------------------
select Salary
from (select Salary, ROW_NUMBER() over (order by Salary desc) as Ranking
      from Employee) as RankedSalary
where Ranking in (1, 2) 
------------------------------------------------
------------------------------------------------

-- q8
select *
from Employee E
where exists(select 1
             from Dependent D
			 where E.SSN = D.ESSN)
------------------------------------------------
------------------------------------------------
Use ITI

-- q9
with RankedSalaries as (select Salary, ROW_NUMBER() over (order by Salary desc) as Ranking
                        from Instructor)
select *
from RankedSalaries
where Ranking <= 2
------------------------------------------------
select *
from (select Salary, ROW_NUMBER() over (order by Salary desc) as Ranking
                        from Instructor) as RankedSalaries
where Ranking <= 2

------------------------------------------------
------------------------------------------------

-- q10
select * 
from Student
order by NEWID()
------------------------------------------------
with RandomStudentTable as (select *, ROW_NUMBER() over (order by NEWID()) as Ranking
                            from Student)
select *
from RandomStudentTable
where Ranking = 1
------------------------------------------------
select *
from (select *, ROW_NUMBER() over (order by NEWID()) as Ranking
                            from Student) as RandomStudentTable
where Ranking = 1

















