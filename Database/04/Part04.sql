--Part 4

use MyCompany

select * from Departments
select * from Employee 

-- q1
insert into Departments(Dname, Dnum, MGRSSN, [MGRStart Date])
values ('DEPT IT', 100, 112233, '1-11-2006')

select * 
from Departments
where Dnum = 100


-- q2
--a
update Departments
set MGRSSN = 968574
where Dnum = 100

--b
insert into Employee
values('Belal', 'Mohamed', 102672, '04-04-2000', '1 Foad seif st.Giza', 'M', 2000, null, 20)

update Departments
set MGRSSN = 102672
where Dnum = 20

--c
insert into Employee
values('Moaaz', 'Naser', 102660, '09-09-2000', '1 ahmed fahmy st.Giza', 'M', 2000, null, 20)

update Employee
set Superssn = 102672
where SSN = 102660



-- q3
-- See if ssn 223344 is foreign key in another tables or tuples
select *
from Employee
where Superssn = 223344

select *
from Departments
where MGRSSN = 223344

select *
from Dependent
where ESSN = 223344

select * 
from Works_for
where ESSn = 223344

-- Now we can't delete this tuple because its primary key is foreign key in other tables and tuples
-- So we should replace all the foreign key with value = 223344 and replace it with another existing primary key
-- The requirement is to temporarily take his position 
-- So replace every foreign key = 223344 with my primary key which is 102672

-- First update table Employee
update Employee
set Superssn = 102672
where Superssn = 223344

select *
from Dependent
where ESSN = 102672

-- Second Upate table Dependent
update Dependent 
set ESSN = 102672
where ESSN = 223344

select *
from Dependent
where ESSN = 102672

-- Third update table Works_for
update Works_for
set ESSn = 102672
where ESSn = 223344

select *
from Works_for
where ESSn = 102672

-- Fourth update table Department
update Departments
set MGRSSN = 102672
where MGRSSN = 223344

select * 
from Departments
where MGRSSN = 102672

-- Then finally we can remove the tuples with SSN = 223344
delete from Employee
where SSN = 223344