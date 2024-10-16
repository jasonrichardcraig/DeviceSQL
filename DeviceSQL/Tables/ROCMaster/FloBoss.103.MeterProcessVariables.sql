CREATE TABLE [RocMaster].[FloBoss.103.MeterProcessVariables] (
    [RocMeterId]                             BIGINT                  NOT NULL,
    [DateTimeStamp]                          DATETIMEOFFSET (7)      CONSTRAINT [DF_FloBoss.103.MeterProcessVariables_DateTimeStamp] DEFAULT (sysdatetime()) NOT NULL,
    [DifferentialPressure.AlarmingEnabled]   [RocMaster].[Parameter] NOT NULL,
    [DifferentialPressure.ScanningEnabled]   [RocMaster].[Parameter] NOT NULL,
    [DifferentialPressure.ScanPeriod]        [RocMaster].[Parameter] NOT NULL,
    [DifferentialPressure.Filter]            [RocMaster].[Parameter] NOT NULL,
    [DifferentialPressure.Units]             [RocMaster].[Parameter] NOT NULL,
    [DifferentialPressure.Raw.Value]         [RocMaster].[Parameter] NOT NULL,
    [DifferentialPressure.EU.Value]          [RocMaster].[Parameter] NOT NULL,
    [DifferentialPressure.Raw.ZeroValue]     [RocMaster].[Parameter] NOT NULL,
    [DifferentialPressure.Raw.SpanValue]     [RocMaster].[Parameter] NOT NULL,
    [[DifferentialPressure.EU.ZeroValue]     [RocMaster].[Parameter] NOT NULL,
    [DifferentialPressure.EU.SpanValue]      [RocMaster].[Parameter] NOT NULL,
    [StaticPressure.AlarmingEnabled]         [RocMaster].[Parameter] NOT NULL,
    [StaticPressurePressure.ScanningEnabled] [RocMaster].[Parameter] NOT NULL,
    [StaticPressurePressure.ScanPeriod]      [RocMaster].[Parameter] NOT NULL,
    [StaticPressurePressure.Filter]          [RocMaster].[Parameter] NOT NULL,
    [StaticPressurePressure.Units]           [RocMaster].[Parameter] NOT NULL,
    [StaticPressure.Raw.Value]               [RocMaster].[Parameter] NOT NULL,
    [StaticPressure.EU.Value]                [RocMaster].[Parameter] NOT NULL,
    [StaticPressure.Raw.ZeroValue]           [RocMaster].[Parameter] NOT NULL,
    [StaticPressure.Raw.SpanValue]           [RocMaster].[Parameter] NOT NULL,
    [StaticPressure.EU.ZeroValue]            [RocMaster].[Parameter] NOT NULL,
    [StaticPressure.EU.SpanValue]            [RocMaster].[Parameter] NOT NULL,
    [Temperature.AlarmingEnabled]            [RocMaster].[Parameter] NOT NULL,
    [Temperature.ScanningEnabled]            [RocMaster].[Parameter] NOT NULL,
    [Temperature.ScanPeriod]                 [RocMaster].[Parameter] NOT NULL,
    [Temperature.Filter]                     [RocMaster].[Parameter] NOT NULL,
    [Temperature.Units]                      [RocMaster].[Parameter] NOT NULL,
    [Temperature.Raw.Value]                  [RocMaster].[Parameter] NOT NULL,
    [Temperature.EU.Value]                   [RocMaster].[Parameter] NOT NULL,
    [Temperature.Raw.ZeroValue]              [RocMaster].[Parameter] NOT NULL,
    [Temperature.Raw.SpanValue]              [RocMaster].[Parameter] NOT NULL,
    [Temperature.EU.ZeroValue]               [RocMaster].[Parameter] NOT NULL,
    [Temperature.EU.SpanValue]               [RocMaster].[Parameter] NOT NULL,
    [RowVersion]                             ROWVERSION              NULL,
    CONSTRAINT [PK_FloBoss.103.MeterProcessVariables_1] PRIMARY KEY CLUSTERED ([RocMeterId] ASC),
    CONSTRAINT [FK_FloBoss.103.MeterProcessVariables_RocMeters] FOREIGN KEY ([RocMeterId]) REFERENCES [RocMaster].[RocMeters] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_FloBoss.103.MeterProcessVariables]
    ON [RocMaster].[FloBoss.103.MeterProcessVariables]([RocMeterId] ASC, [DateTimeStamp] ASC);

