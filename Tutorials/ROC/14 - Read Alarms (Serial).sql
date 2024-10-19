SET NOCOUNT ON;
DECLARE @century int = 2000;
DECLARE @channelName nvarchar(512) = 'com1://localhost';
DECLARE @portName nvarchar(max) = 'COM1';
DECLARE @baudRate int = 19200;
DECLARE @dataBits tinyint = 8;
DECLARE @parity tinyint = 0;
DECLARE @stopBits tinyint = 1;
DECLARE @readTimeout int = 250;
DECLARE @writeTimeout int = 250;
DECLARE @numberOfRetries int = 5;
DECLARE @waitToRetry int = 125;
DECLARE @deviceName nvarchar(255) = 'FB103-01';
DECLARE @requestWriteDelay int = 0;
DECLARE @responseReadDelay int = 0;
DECLARE @deviceAddress tinyint= 1;
DECLARE @deviceGroup tinyint= 2;
DECLARE @hostAddress tinyint= 3;
DECLARE @hostGroup tinyint= 1;
DECLARE @index int = 0;
DECLARE @alarmRecordSegment int = 0;

DECLARE @alarmRecordArray [ROCMaster].[AlarmRecordArray] = [ROCMaster].[AlarmRecordArray]::Empty();
DECLARE @alarms TABLE ([Index] int, DateTimeStamp datetime NULL, AlarmCode nvarchar(64), AlarmClass nvarchar(64), AlarmState nvarchar(64), Tag nvarchar(64), [Value] real NULL); 

PRINT [ChannelManager].[RegisterSerialPortChannel] (@channelName, @portName, @baudRate, @dataBits, @parity, @stopBits, @readTimeout, @writeTimeout);
PRINT [DeviceManager].[RegisterROCMaster] (@channelName, @deviceName, @deviceAddress, @deviceGroup, @hostAddress, @hostGroup, @numberOfRetries, @waitToRetry, @requestWriteDelay, @responseReadDelay);

WHILE (24 > @alarmRecordSegment)
	BEGIN
		SET @alarmRecordArray = [ROCMaster].[GetAlarms](@deviceName, 10, @index);
		DECLARE @recordIndex int = 0;
		WHILE(10 > @recordIndex)
			BEGIN
					DECLARE @alarmRecord [ROCMaster].[AlarmRecord] = @alarmRecordArray.GetAlarmRecord(@recordIndex);
					INSERT INTO @alarms ([Index], DateTimeStamp, AlarmCode, AlarmClass, AlarmState, Tag, [Value])
					VALUES (@alarmRecord.[Index], @alarmRecord.DateTimeStamp, @alarmRecord.AlarmCode, @alarmRecord.AlarmClass, @alarmRecord.AlarmState, @alarmRecord.Tag, @alarmRecord.[Value]);
					SET @recordIndex = @recordIndex + 1;
			END
		SET @index = @index + 10;
		SET @alarmRecordSegment = @alarmRecordSegment + 1;
	END

PRINT [DeviceManager].[UnregisterDevice] (@deviceName);
PRINT [ChannelManager].[UnregisterChannel] (@channelName);

SELECT * FROM @alarms