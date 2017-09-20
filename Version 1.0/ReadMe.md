### The script installation method requires Microsoft SQL Server 2008 (or Higher), the file restore method requires Microsoft SQL 2016 (or higher) due to file versioning.

#### To create ASYMMETRIC KEY Download DeviceSQL.dll and copy it to 'C:\DLLTemp\DeviceSQL.dll' or other path your SQL Server process has access to and run the script below.

CREATE ASYMMETRIC KEY [DeviceSqlKey] FROM EXECUTABLE FILE = 'C:\DLLTemp\DeviceSQL.dll'
CREATE LOGIN [DeviceSqlClrLogin] FROM ASYMMETRIC KEY [DeviceSqlKey]
GRANT UNSAFE ASSEMBLY TO [DeviceSqlClrLogin]
GO 
