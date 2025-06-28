-- Part 1

use MyCompany

-- q1
select * 
from Employee

-- q2
select Fname, Lname, Salary, Dno
from Employee

-- q3
select (Fname + ' ' + Lname) as FullName, (Salary * 12 * 0.1) as ANNUAL_COMM
from Employee


-- q4
select *
from Employee
where Salary > 1000

-- q5
select SSN, Fname, Lname
from Employee
where (Salary * 12 ) > 10000


-- q6
select Fname, Lname, Salary
from Employee
where Sex = 'F'

-- q7
select Dnum, Dname
from Departments
where MGRSSN = 968574

-- q8
select Pnumber, Pname, Plocation
from Project
where Dnum = 10

