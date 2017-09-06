USE [DeviceSQL]
GO
DECLARE @deviceName nvarchar(512) = 'FB107-01';
DECLARE @century int = 2000; 
SELECT [ROCMaster].[GetDateTimeWithCentury] (@deviceName, @century)



