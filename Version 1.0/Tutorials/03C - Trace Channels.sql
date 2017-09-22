DECLARE @channelName nvarchar(512) = 'com1://localhost';

-- NOTE: THIS QUERY RUNS FOREVER UNLESS QUERY IS CANCELED

SELECT [Trace].[MessageDateTimeStamp], [Trace].[Sequence], [Trace].[ChannelName], [Trace].[ChannelType], [Trace].[Operation], [Trace].[StartTime], [Trace].[Duration], [Trace].[Count], [Trace].[Data]
FROM [ChannelManager].[TraceChannels] () [Trace]
WHERE [Trace].[ChannelName] = @channelName


