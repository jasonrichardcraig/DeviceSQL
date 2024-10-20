SET NOCOUNT ON;
DECLARE @channelName nvarchar(512) = 'tcp://127.0.0.1:502';
DECLARE @hostName nvarchar(512) = '127.0.0.1';
DECLARE @hostPort int = 502;
DECLARE @connectAttempts int = 3;
DECLARE @connectionRetryDelay int = 5000;
DECLARE @writeTimeout int = 5000;
DECLARE @readTimeout int = 5000;
DECLARE @numberOfRetries int = 5;
DECLARE @waitToRetry int = 2000;
DECLARE @deviceName nvarchar(255) = 'ModRSsim2';
DECLARE @requestWriteDelay int = 50;
DECLARE @responseReadDelay int = 50;
DECLARE @UseMbapHeaders BIT = 1;
DECLARE @UseExtendedAddressing BIT = 0;
DECLARE @UnitId INT = 1;
DECLARE @date DateTime;
DECLARE @sampleRate int = 250;
DECLARE @turnaroundDelay int = 150;
DECLARE @delay DateTime = '00:00:00.000'
DECLARE @samples int = 0;
DECLARE @maxSamples int = 86400;

SET @delay = DATEADD(ms, @sampleRate - @turnaroundDelay, @delay);

DECLARE @HoldingRegister [ModbusMaster].[HoldingRegister] = [ModbusMaster].[HoldingRegister]::Parse('True;1,False,0'); -- AddressIsZeroBased;Address,ByteSwap,Value
DECLARE @ModbusAddress [ModbusMaster].[ModbusAddress] = @HoldingRegister.Address;
DECLARE @HoldingRegisterArray [ModbusMaster].[HoldingRegisterArray] = [ModbusMaster].[HoldingRegisterArray]::Empty();

SET @HoldingRegisterArray = @HoldingRegisterArray.AddHoldingRegister(@HoldingRegister);

PRINT [ChannelManager].[RegisterTcpChannel] (@channelName, @hostName, @hostPort, @connectAttempts, @connectionRetryDelay, @readTimeout, @writeTimeout);
PRINT [DeviceManager].[RegisterModbusMaster] (@ChannelName, @DeviceName, @UseMbapHeaders, @UseExtendedAddressing, @UnitId, @NumberOfRetries, @WaitToRetry, @RequestWriteDelay, @ResponseReadDelay);
	WHILE (@samples < @maxSamples)
	BEGIN
		WAITFOR DELAY @delay
		SET @HoldingRegisterArray = [ModbusMaster].[ReadHoldings](@deviceName, @HoldingRegisterArray);
		INSERT INTO [ModbusValues].[dbo].[HoldingRegisterValues]
           ([DateTime]
           ,[Value])
	    VALUES
           (GetDate(), @HoldingRegisterArray.GetShort(0, 0))
		SET @samples = @samples + 1
	END
PRINT [DeviceManager].[UnregisterDevice] (@deviceName);
PRINT [ChannelManager].[UnregisterChannel] (@channelName);