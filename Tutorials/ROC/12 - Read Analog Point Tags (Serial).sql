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
DECLARE @parameterArray [ROCMaster].[ParameterArray] = [ROCMaster].[ParameterArray]::Empty();


PRINT [ChannelManager].[RegisterSerialPortChannel] (@channelName, @portName, @baudRate, @dataBits, @parity, @stopBits, @readTimeout, @writeTimeout);
PRINT [DeviceManager].[RegisterROCMaster] (@channelName, @deviceName, @deviceAddress, @deviceGroup, @hostAddress, @hostGroup, @numberOfRetries, @waitToRetry, @requestWriteDelay, @responseReadDelay);

SET @parameterArray = @parameterArray.AddParameter([ROCMaster].[Parameter]::ParseAc10(3,0,0,''));
SET @parameterArray = @parameterArray.AddParameter([ROCMaster].[Parameter]::ParseFl(3,0,14,0));
SET @parameterArray = @parameterArray.AddParameter([ROCMaster].[Parameter]::ParseAc10(3,1,0,''));
SET @parameterArray = @parameterArray.AddParameter([ROCMaster].[Parameter]::ParseFl(3,1,14,0));


SET @parameterArray = [ROCMaster].[ReadParameters](@deviceName, @parameterArray);

SELECT @parameterArray.GetParameter(0).ToString() as AnaloPoint1Tag,
		@parameterArray.GetParameter(1).ToFl() as AnalogPoint1Value,
		@parameterArray.GetParameter(2).ToString() as AnaloPoint1Tag,
		@parameterArray.GetParameter(3).ToFl() as AnalogPoint1Value

PRINT [DeviceManager].[UnregisterDevice] (@deviceName);
PRINT [ChannelManager].[UnregisterChannel] (@channelName);
