use master

EXEC sp_depends @objname = N'master';


select * from sysobjects where xtype IN ('IT', 'V', 'P') AND uid = (Select uid from sys.Databases WHERE name = 'master')

SELECT * FROM sys.Databases
