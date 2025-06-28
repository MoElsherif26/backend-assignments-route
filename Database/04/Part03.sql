-- Part 3

use MyCompany

-- q1
select Pnumber, Pname, Plocation
from Project
where City in ('Cairo', 'Alex')

-- q2
select *
from Project
where Pname like 'a%'

-- q3
select *
from Employee
where Dno = 30 and Salary between 1000 and 2000