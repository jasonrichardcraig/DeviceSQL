USE master 
GO 

CREATE ASYMMETRIC KEY [DeviceSqlKey] FROM EXECUTABLE FILE = 'C:\DLLTemp\DeviceSQL.dll' --Requires compiling the project and copying the project output (DeviceSQL.dll) to "C:\DLLTemp"
CREATE LOGIN [DeviceSqlClrLogin] FROM ASYMMETRIC KEY [DeviceSqlKey]
GRANT UNSAFE ASSEMBLY TO [DeviceSqlClrLogin]
GO 