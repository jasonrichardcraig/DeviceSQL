USE [DeviceSQL]
GO
DECLARE @channelName nvarchar(512) = 'com1://localhost';
DECLARE @portName nvarchar(max) = 'COM1';
DECLARE @baudRate int = 19200;
DECLARE @dataBits tinyint = 8;
DECLARE @parity tinyint = 0;
DECLARE @stopBits tinyint = 1;
DECLARE @readTimeout int = 5000;
DECLARE @writeTimeout int = 5000;

SELECT [ChannelManager].[RegisterSerialPortChannel] (@channelName,
													@portName,
													@baudRate,
													@dataBits,
													@parity,
													@stopBits,
													@readTimeout,
													@writeTimeout)
GO


