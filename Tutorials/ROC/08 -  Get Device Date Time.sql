USE [DeviceSQL]
GO
DECLARE @deviceName nvarchar(512) = 'FB103-01';
DECLARE @century int = 2000; 
SELECT [ROCMaster].[GetRealTimeClockValueWithCentury] (@deviceName, @century)



