-- Part 02
Use AdventureWorks2012

-- q1
select SOH.SalesOrderID, ShipDate
from Sales.SalesOrderHeader as SOH
where SOH.OrderDate between '7/28/2002' and '7/29/2014'

-- q2
select P.ProductID, P.Name
from Production.Product P
where P.StandardCost < 110

-- q3
select P.ProductID, P.Name
from Production.Product P
where p.Weight is null

-- q4
select P.ProductID, P.Name
from Production.Product P
where P.Color in (' Silver', 'Black', 'Red')

-- q5
select P.ProductID, P.Name
from Production.Product P
where P.Name like 'B%'

-- q6-7
UPDATE Production.ProductDescription 
SET Description = 'Chromoly steel_High of defects' 
WHERE ProductDescriptionID = 3

select PD.*
from Production.ProductDescription PD
where PD.Description like '%[_]%' 

-- q8
select distinct E.HireDate
from HumanResources.Employee E

-- q9
select CONCAT_WS(' ', P.Name, 'is only!', P.ListPrice) as ProductName_ListPrice
from Production.Product P
order by P.ListPrice
