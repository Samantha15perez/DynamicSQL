use AdventureWorks2012

EXEC sp_depends @objname = N'Sales.SalesOrderDetail';


select * from sysobjects where xtype IN ('U', 'V', 'P') AND uid = (Select database_id from sys.Databases WHERE name = 'AdventureWorks2012')

SELECT * FROM sys.Databases

select * from sysobjects where uid = (select uid from sys.databases where name = 'AdventureWorks2012') AND xtype IN ('IT', 'V', 'P')

use master;
select * from sysobjects where uid = (select uid from sys.databases where name = 'AdventureWorks2012') AND name = 'BusinessEntity'

select * from sysobjects



select Table_schema, Table_name from INFORMATION_SCHEMA.TABLES UNION Select SPECIFIC_SCHEMA, SPECIFIC_NAME from INFORMATION_SCHEMA.ROUTINES where ROUTINE_TYPE = 'Procedure'

select * from INFORMATION_SCHEMA.ROUTINES