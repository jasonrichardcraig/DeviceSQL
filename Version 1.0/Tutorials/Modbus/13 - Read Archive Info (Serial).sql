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
DECLARE @archiveInformation [ROCMaster].[ArchiveInformation];
DECLARE @archiveInfo TABLE (AlarmLogPointer int, EventLogPointer int, CurrentAuditLogPointer int, MinutesPerHistoricalPeriod tinyint, BaseRamCurrentHistoricalDay tinyint, BaseRamCurrentHistoricalHour int, BaseRamNumberOfDays tinyint, Base1RamCurrentHistoricalDay tinyint, BaseRam1CurrentHistoricalHour int, BaseRam1NumberOfDays tinyint, BaseRam2CurrentHistoricalDay tinyint, BaseRam2CurrentHistoricalHour int, BaseRam2NumberOfDays tinyint, MaximumNumberOfAlarms int, MaximumNumberOfEvents int); 

PRINT [ChannelManager].[RegisterSerialPortChannel] (@channelName, @portName, @baudRate, @dataBits, @parity, @stopBits, @readTimeout, @writeTimeout);
PRINT [DeviceManager].[RegisterROCMaster] (@channelName, @deviceName, @deviceAddress, @deviceGroup, @hostAddress, @hostGroup, @numberOfRetries, @waitToRetry, @requestWriteDelay, @responseReadDelay);

SET @archiveInformation = [ROCMaster].[GetArchiveInfo](@deviceName);

INSERT INTO @archiveInfo (AlarmLogPointer,
							EventLogPointer,
							CurrentAuditLogPointer,
							MinutesPerHistoricalPeriod, 
							BaseRamCurrentHistoricalDay,
							BaseRamCurrentHistoricalHour, 
							BaseRamNumberOfDays,
							Base1RamCurrentHistoricalDay,
							BaseRam1CurrentHistoricalHour,
							BaseRam1NumberOfDays,
							BaseRam2CurrentHistoricalDay,
							BaseRam2CurrentHistoricalHour,
							BaseRam2NumberOfDays,
							MaximumNumberOfAlarms,
							MaximumNumberOfEvents)
VALUES (@archiveInformation.AlarmLogPointer,
		 @archiveInformation.EventLogPointer,
		 @archiveInformation.CurrentAuditLogPointer,
		 @archiveInformation.MinutesPerHistoricalPeriod,
		 @archiveInformation.BaseRamCurrentHistoricalDay,
		 @archiveInformation.BaseRamCurrentHistoricalHour,
		 @archiveInformation.BaseRamNumberOfDays,
		 @archiveInformation.Base1RamCurrentHistoricalDay,
		 @archiveInformation.BaseRam1CurrentHistoricalHour,
		 @archiveInformation.BaseRam1NumberOfDays,
		 @archiveInformation.BaseRam2CurrentHistoricalDay,
		 @archiveInformation.BaseRam2CurrentHistoricalHour,
		 @archiveInformation.BaseRam2NumberOfDays,
		 @archiveInformation.MaximumNumberOfAlarms,
		 @archiveInformation.MaximumNumberOfEvents);

PRINT [DeviceManager].[UnregisterDevice] (@deviceName);
PRINT [ChannelManager].[UnregisterChannel] (@channelName);

SELECT * FROM @archiveInfo