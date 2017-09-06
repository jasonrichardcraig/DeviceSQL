DECLARE @century int = 2000;
DECLARE @channelName nvarchar(512) = 'tcp://192.168.0.8:1000';
DECLARE @hostName nvarchar(512) = '192.168.0.8';
DECLARE @hostPort int = 1004;
DECLARE @writeTimeout int = 5000;
DECLARE @readTimeout int = 5000;
DECLARE @numberOfRetries int = 5;
DECLARE @waitToRetry int = 2000;
DECLARE @deviceName nvarchar(255) = 'FB107-01';
DECLARE @requestWriteDelay int = 50;
DECLARE @responseReadDelay int = 50;
DECLARE @deviceAddress tinyint= 1;
DECLARE @deviceGroup tinyint= 2;
DECLARE @hostAddress tinyint= 3;
DECLARE @hostGroup tinyint= 1;
DECLARE @date DateTime;
DECLARE @sampleRate int = 250;
DECLARE @turnaroundDelay int = 162;
DECLARE @delay DateTime = '00:00:00.000'
DECLARE @samples int = 0;
DECLARE @maxSamples int = 40;

SET NOCOUNT ON;

SET @delay = DATEADD(ms, @sampleRate - @turnaroundDelay, @delay);

PRINT [ChannelManager].[RegisterTcpChannel] (@channelName, @hostName, @hostPort, @readTimeout, @writeTimeout);
PRINT [DeviceManager].[RegisterROCMaster] (@channelName, @deviceName, @deviceAddress, @deviceGroup, @hostAddress, @hostGroup, @numberOfRetries, @waitToRetry, @requestWriteDelay, @responseReadDelay);
	WHILE (@samples < @maxSamples)
	BEGIN
		WAITFOR DELAY @delay
		INSERT INTO DeviceDateTimeLog ([DateTimeStamp], [DeviceDateTime]) VALUES (SYSDATETIME(), [ROCMaster].[GetDateTimeWithCentury] (@deviceName, @century));
		SET @samples = @samples + 1
	END
PRINT [DeviceManager].[UnregisterDevice] (@deviceName);
PRINT [ChannelManager].[UnregisterChannel] (@channelName);
