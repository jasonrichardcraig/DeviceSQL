USE [DeviceSQL]
GO
DECLARE @startCounterValue int = [Watchdog].[GetCounterValue] ();
BEGIN
	WAITFOR DELAY '00:00:05.000'; -- The Watchdog worker spins at 300 milliseconds so this is more than enough time to find the fault
END
BEGIN 
	IF @startCounterValue = [Watchdog].[GetCounterValue]()
		BEGIN
			PRINT 'Watchdog stopped at ' + CAST(@startCounterValue as nvarchar(512));
		END
	ELSE
	SELECT [Watchdog].[GetCounterValue]()
END


