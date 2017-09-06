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
DECLARE @deviceDateTime datetime;
DECLARE @index int = 0;
DECLARE @historyRecordSegment int = 0;
DECLARE @timeStampHistoryRecordArray [ROCMaster].[HistoryRecordArray] = [ROCMaster].[HistoryRecordArray]::Empty();
DECLARE @flowDurationHistoryRecordArray [ROCMaster].[HistoryRecordArray] = [ROCMaster].[HistoryRecordArray]::Empty();
DECLARE @averageDifferentialPressureRecordArray [ROCMaster].[HistoryRecordArray] = [ROCMaster].[HistoryRecordArray]::Empty();
DECLARE @averageStaticPressureHistoryRecordArray [ROCMaster].[HistoryRecordArray] = [ROCMaster].[HistoryRecordArray]::Empty();
DECLARE @averageFlowingTemperaureHistoryRecordArray [ROCMaster].[HistoryRecordArray] = [ROCMaster].[HistoryRecordArray]::Empty();
DECLARE @averageImvBmvHistoryRecordArray [ROCMaster].[HistoryRecordArray] = [ROCMaster].[HistoryRecordArray]::Empty();
DECLARE @averageHwpfHistoryRecordArray [ROCMaster].[HistoryRecordArray] = [ROCMaster].[HistoryRecordArray]::Empty();
DECLARE @accumulatedGasVolumeHistoryRecordArray [ROCMaster].[HistoryRecordArray] = [ROCMaster].[HistoryRecordArray]::Empty();
DECLARE @accumulatedEnergyHistoryRecordArray [ROCMaster].[HistoryRecordArray] = [ROCMaster].[HistoryRecordArray]::Empty();
DECLARE @historyRecords TABLE (HistoryRecord [ROCMaster].[HistoryRecord]);

PRINT [ChannelManager].[RegisterSerialPortChannel] (@channelName, @portName, @baudRate, @dataBits, @parity, @stopBits, @readTimeout, @writeTimeout);
PRINT [DeviceManager].[RegisterROCMaster] (@channelName, @deviceName, @deviceAddress, @deviceGroup, @hostAddress, @hostGroup, @numberOfRetries, @waitToRetry, @requestWriteDelay, @responseReadDelay);

SET @deviceDateTime = [ROCMaster].[GetRealTimeClockValueWithCentury] (@deviceName, 2000);

WHILE (14 > @historyRecordSegment)
	BEGIN
		DECLARE @startIndex int = (@historyRecordSegment * 60);
		SET @timeStampHistoryRecordArray = [ROCMaster].[GetHistory](@deviceName, 0, 254, 60, @startIndex);
		SET @flowDurationHistoryRecordArray = [ROCMaster].[GetHistory](@deviceName, 0, 0, 60, @startIndex);
		SET @averageDifferentialPressureRecordArray = [ROCMaster].[GetHistory](@deviceName, 0, 1, 60, @startIndex);
		SET @averageStaticPressureHistoryRecordArray = [ROCMaster].[GetHistory](@deviceName, 0, 2, 60, @startIndex);
		SET @averageFlowingTemperaureHistoryRecordArray = [ROCMaster].[GetHistory](@deviceName, 0, 3, 60, @startIndex);
		SET @averageImvBmvHistoryRecordArray = [ROCMaster].[GetHistory](@deviceName, 0, 4, 60, @startIndex);
		SET @averageHwpfHistoryRecordArray = [ROCMaster].[GetHistory](@deviceName, 0, 5, 60, @startIndex);
		SET @accumulatedGasVolumeHistoryRecordArray = [ROCMaster].[GetHistory](@deviceName, 0, 6, 60, @startIndex);
		SET @accumulatedEnergyHistoryRecordArray = [ROCMaster].[GetHistory](@deviceName, 0, 7, 60, @startIndex);
		DECLARE @recordIndex int = 0;
		WHILE(60 > @recordIndex)
			BEGIN
					INSERT INTO @historyRecords (HistoryRecord)
					VALUES (@timeStampHistoryRecordArray.GetHistoryRecord(@recordIndex));
					INSERT INTO @historyRecords (HistoryRecord)
					VALUES (@flowDurationHistoryRecordArray.GetHistoryRecord(@recordIndex));
					INSERT INTO @historyRecords (HistoryRecord)
					VALUES (@averageDifferentialPressureRecordArray.GetHistoryRecord(@recordIndex));
					INSERT INTO @historyRecords (HistoryRecord)
					VALUES (@averageStaticPressureHistoryRecordArray.GetHistoryRecord(@recordIndex));
					INSERT INTO @historyRecords (HistoryRecord)
					VALUES (@averageFlowingTemperaureHistoryRecordArray.GetHistoryRecord(@recordIndex));
					INSERT INTO @historyRecords (HistoryRecord)
					VALUES (@averageImvBmvHistoryRecordArray.GetHistoryRecord(@recordIndex));
					INSERT INTO @historyRecords (HistoryRecord)
					VALUES (@averageHwpfHistoryRecordArray.GetHistoryRecord(@recordIndex));
					INSERT INTO @historyRecords (HistoryRecord)
					VALUES (@accumulatedGasVolumeHistoryRecordArray.GetHistoryRecord(@recordIndex));
					INSERT INTO @historyRecords (HistoryRecord)
					VALUES (@accumulatedEnergyHistoryRecordArray.GetHistoryRecord(@recordIndex));
					SET @recordIndex = @recordIndex + 1;
			END
		SET @historyRecordSegment = @historyRecordSegment + 1;
	END

PRINT [DeviceManager].[UnregisterDevice] (@deviceName);
PRINT [ChannelManager].[UnregisterChannel] (@channelName);

SELECT hp254.[HistoryRecord].[Index],
hp254.[HistoryRecord].[ToDateTimeStamp](@deviceDateTime) AS DateTimeStamp,
hp0.[HistoryRecord].[ToFloat]() AS FlowDuration,
hp1.[HistoryRecord].[ToFloat]() AS AverageDifferentialPressure,
hp2.[HistoryRecord].[ToFloat]() AS AverageStaticPressure,
hp3.[HistoryRecord].[ToFloat]() AS AverageFlowingTemperature,
hp4.[HistoryRecord].[ToFloat]() AS AverageImvBmv,
hp5.[HistoryRecord].[ToFloat]() AS AverageHwPf,
hp6.[HistoryRecord].[ToFloat]() AS AccumulatedGasVolume,
hp7.[HistoryRecord].[ToFloat]() AS AccumulatedEnergy
FROM (SELECT * FROM @historyRecords WHERE [HistoryRecord].[HistoryPointNumber] = 254) as hp254 JOIN
@historyRecords hp0 ON hp254.[HistoryRecord].[Index] = hp0.[HistoryRecord].[Index] AND hp0.[HistoryRecord].[HistoryPointNumber] = 0 INNER JOIN
@historyRecords hp1 ON hp254.[HistoryRecord].[Index] = hp1.[HistoryRecord].[Index] AND hp1.[HistoryRecord].[HistoryPointNumber] = 1 INNER JOIN
@historyRecords hp2 ON hp254.[HistoryRecord].[Index] = hp2.[HistoryRecord].[Index] AND hp2.[HistoryRecord].[HistoryPointNumber] = 2 INNER JOIN
@historyRecords hp3 ON hp254.[HistoryRecord].[Index] = hp3.[HistoryRecord].[Index] AND hp3.[HistoryRecord].[HistoryPointNumber] = 3 INNER JOIN
@historyRecords hp4 ON hp254.[HistoryRecord].[Index] = hp4.[HistoryRecord].[Index] AND hp4.[HistoryRecord].[HistoryPointNumber] = 4 INNER JOIN
@historyRecords hp5 ON hp254.[HistoryRecord].[Index] = hp5.[HistoryRecord].[Index] AND hp5.[HistoryRecord].[HistoryPointNumber] = 5 INNER JOIN
@historyRecords hp6 ON hp254.[HistoryRecord].[Index] = hp6.[HistoryRecord].[Index] AND hp6.[HistoryRecord].[HistoryPointNumber] = 6 INNER JOIN
@historyRecords hp7 ON hp254.[HistoryRecord].[Index] = hp7.[HistoryRecord].[Index] AND hp7.[HistoryRecord].[HistoryPointNumber] = 7


