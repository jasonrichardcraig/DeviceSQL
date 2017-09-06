USE [DeviceSQL]
GO
DECLARE @channelName nvarchar(512) = 'tcp://192.168.0.8:1000';
DECLARE @hostName nvarchar(512) = '192.168.0.8';
DECLARE @hostPort int = 1004;
DECLARE @writeTimeout int = 5000;
DECLARE @readTimeout int = 5000;

SELECT [ChannelManager].[RegisterTcpChannel] (@channelName,
												@hostName,
												@hostPort,
												@readTimeout,
												@writeTimeout)
GO


