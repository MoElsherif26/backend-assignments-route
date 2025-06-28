-- Part 01

Use MyCompany

select P.Pname, P.Plocation, d.Dname
from Project as P inner join Departments as D
on D.Dnum = P.Dnum