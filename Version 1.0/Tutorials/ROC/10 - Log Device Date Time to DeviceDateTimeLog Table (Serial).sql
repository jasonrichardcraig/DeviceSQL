SET NOCOUNT ON
DECLARE @century int = 2000;
DECLARE @channelName nvarchar(512) = 'com1://localhost';
DECLARE @portName nvarchar(max) = 'COM1';
DECLARE @baudRate int = 19200;
DECLARE @dataBits tinyint = 8;
DECLARE @parity tinyint = 0;
DECLARE @stopBits tinyint = 1;
DECLARE @readTimeout int = 3000;
DECLARE @writeTimeout int = 3000;
DECLARE @numberOfRetries int = 5;
DECLARE @waitToRetry int = 1500;
DECLARE @deviceName nvarchar(255) = 'FB103-01';
DECLARE @requestWriteDelay int = 140;
DECLARE @responseReadDelay int = 210;
DECLARE @deviceAddress tinyint= 1;
DECLARE @deviceGroup tinyint= 2;
DECLARE @hostAddress tinyint= 3;
DECLARE @hostGroup tinyint= 1;
DECLARE @date DateTime;
DECLARE @delay DateTime = '00:00:00.840'
DECLARE @samples int = 0;
DECLARE @maxSamples int = 86400;

PRINT [ChannelManager].[RegisterSerialPortChannel] (@channelName, @portName, @baudRate, @dataBits, @parity, @stopBits, @readTimeout, @writeTimeout);
PRINT [DeviceManager].[RegisterROCMaster] (@channelName, @deviceName, @deviceAddress, @deviceGroup, @hostAddress, @hostGroup, @numberOfRetries, @waitToRetry, @requestWriteDelay, @responseReadDelay);
	WHILE (@samples < @maxSamples)
	BEGIN
		WAITFOR DELAY @delay
		INSERT INTO DeviceDateTimeLog ([DateTimeStamp], [DeviceDateTime]) VALUES (SYSDATETIME(), [ROCMaster].[GetDateTimeWithCentury] (@deviceName, @century));
		SET @samples = @samples + 1
	END
PRINT [DeviceManager].[UnregisterDevice] (@deviceName);
PRINT [ChannelManager].[UnregisterChannel] (@channelName);
