USE master 
GO 
sp_configure 'show advanced options', 1;  
GO  
RECONFIGURE;  
GO  
sp_configure 'clr enabled', 1;  
GO  
RECONFIGURE;  
GO

-- Reccomended by Microsoft

--CREATE ASYMMETRIC KEY [DeviceSqlKey] FROM EXECUTABLE FILE = 'C:\DLLTemp\DeviceSQL.dll' --Requires compiling the project and copying the project output (DeviceSQL.dll) to "C:\DLLTemp"
--CREATE LOGIN [DeviceSqlClrLogin] FROM ASYMMETRIC KEY [DeviceSqlKey]
--GRANT UNSAFE ASSEMBLY TO [DeviceSqlClrLogin]
--GO 

-- Git R Done Quick (Less Secure)
ALTER DATABASE [DeviceSQL] SET TRUSTWORTHY ON;
GO