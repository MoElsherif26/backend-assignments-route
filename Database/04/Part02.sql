--Part 2

use ITI

-- q1
select distinct Ins_name
from Instructor

-- Bonus
select @@VERSION
/*
Returns a string containing version information about the SQL Server instance.
Includes:

SQL Server version (e.g., SQL Server 2022)

Build number

Operating system information
*/


select @@SERVERNAME
/*
Returns the name of the local server that's running SQL Server.
This is typically the network name of the computer.
*/

