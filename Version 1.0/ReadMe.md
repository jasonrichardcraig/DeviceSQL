### The script installation method requires Microsoft SQL Server 2008 (or Higher), the file restore method requires Microsoft SQL 2016 (or higher) due to file versioning. Device SQL works with all SQL Server editions (Express, Standard & Datacenter) an runs on Microsoft Windows Azure, on premise and even credit card sized embedded PC's.

#### To create an ASYMMETRIC KEY, download [DeviceSQL.dll](https://github.com/jasonrichardcraig/DeviceSQL/raw/master/Version%201.0/DeviceSQL.dll) and copy it to 'C:\DLLTemp\DeviceSQL.dll' or other path your SQL Server process has access to and run the script below.

##### USE master 
##### GO 
##### sp_configure 'show advanced options', 1;  
##### GO  
##### RECONFIGURE;  
##### GO  
##### sp_configure 'clr enabled', 1;  
##### GO  
##### RECONFIGURE;  
##### GO

##### CREATE ASYMMETRIC KEY [DeviceSqlKey] FROM EXECUTABLE FILE = 'C:\DLLTemp\DeviceSQL.dll'
##### CREATE LOGIN [DeviceSqlClrLogin] FROM ASYMMETRIC KEY [DeviceSqlKey]
##### GRANT UNSAFE ASSEMBLY TO [DeviceSqlClrLogin]


