USE [DeviceSQL]
GO
DECLARE @channelName nvarchar(512) = 'tcp://192.168.0.8:1000'; 
DECLARE @deviceName nvarchar(512) = 'FB107-01';
DECLARE @deviceAddress tinyint = 1;
DECLARE @deviceGroup tinyint = 2;
DECLARE @hostAddress tinyint = 3;
DECLARE @hostGroup tinyint = 1;
DECLARE @numberOfRetries int = 5;
DECLARE @waitToRetry int = 2500;
DECLARE @requestWriteDelay int = 0;
DECLARE @responseReadDelay int = 10;

SELECT [DeviceManager].[RegisterROCMaster] (@channelName,
											@deviceName,
											@deviceAddress,
											@deviceGroup,
											@hostAddress,
											@hostGroup,
											@numberOfRetries,
											@waitToRetry,
											@requestWriteDelay,
											@responseReadDelay);

GO


