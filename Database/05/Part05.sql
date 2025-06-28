-- Part 05

Use MyCompany

-- 1
select Dname
from Departments
where Dnum = (select E.Dno
              from Employee as E
			  where E.SSN = (select MIN(SSN) 
			                 from Employee))

select D.Dname
from Employee E 
join Departments D on D.Dnum = E.Dno
where E.SSN = (select MIN(SSN) from Employee)

select Dname
from Departments
where Dnum = (select Top 1 Dno
              from Employee 
			  order by SSN asc)


-- 2
select Super.Fname, Super.Lname
from Employee E 
inner join Employee Super on Super.SSN = E.Superssn
left join Dependent D on Super.SSN = D.ESSN
where D.ESSN is null

select Super.Fname, Super.Lname
from Employee as Super
where SSN in (
               select distinct E.Superssn
			   from Employee as E
			   where E.Superssn not in (
			                             select distinct D.ESSN
										 from Dependent as D))



-- 3
select distinct Salary
from Employee
where Salary in(
                 select distinct Top 2 Salary
				 from Employee
				 order by Salary desc)


select distinct E1.Salary
from Employee E1
where 2 > (
            select COUNT(distinct E2.Salary)
            from Employee E2
            where E2.Salary > E1.Salary)


-- 4

select E.SSN, E.Fname, E.Lname
from Employee E
where Exists (
               select 1
			   from Dependent D
			   where E.SSN = D.ESSN)