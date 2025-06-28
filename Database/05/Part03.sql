-- Part 03

Use MyCompany

-- 1
select D.Dnum, D.Dname, E.SSN as 'Mgr SSN', E.Fname as 'Mgr FName', E.Lname as 'Mgr LName'
from Departments D inner join Employee E
on E.SSN = D.MGRSSN

-- 2
select D.Dname, P.Pname
 from Project as P inner join Departments as D
 on D.Dnum = P.Dnum

 -- 3
 select Dep.Dependent_name, Dep.Sex, Dep.Bdate, Dep.ESSN, E.Fname, E.Lname
 from Dependent as Dep join Employee as E
 on E.SSN = Dep.ESSN

 -- 4
 select distinct E.Fname, E.Lname
 from Employee as E join Works_for as W
 on E.SSN = W.ESSn
 where W.Hours >= 10 and E.Dno = 20

 -- 5
 select E.Fname, E.Lname
 from Employee as E join Employee as Mgr
 on Mgr.SSN = E.Superssn
 where Mgr. Fname = 'Kamel' and Mgr.Lname = 'Mohamed'

 -- 6
 select distinct Mgr.SSN,
        CONCAT_WS(' ', Mgr.Fname, Mgr.Lname) as 'Manager Name',
        Mgr.Salary, Mgr.Address, Mgr.Bdate, Mgr.Sex, Mgr.Dno
 from Employee as E join Employee as Mgr
 on Mgr.SSN = E.Superssn

 -- 7
 select E.Fname, P.Pname
 from Employee as E inner join Works_for as W
 on E.SSN = W.ESSn
 inner join Project as P
 on P.Pnumber = W.Pno
 order by P.Pname
 
 -- 8
 select P.Pnumber, P.Pname, D.Dname, Dept_Mgr.Lname, Dept_Mgr.Address, Dept_Mgr.Bdate
 from Project as P join Departments as D
 on D.Dnum = P.Dnum
 join Employee as Dept_Mgr
 on Dept_Mgr.SSN = D.MGRSSN
 where P.City = 'Cairo'

 -- 9
 select E.*, D.*
 from Employee as E left join Dependent as D
 on E.SSN = D.ESSN