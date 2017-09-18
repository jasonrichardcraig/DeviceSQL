CREATE TABLE [ROCMaster].[FloBoss.103.MeterProcessVariables] (
    [ROCMeterId]                             BIGINT                  NOT NULL,
    [DateTimeStamp]                          DATETIMEOFFSET (7)      CONSTRAINT [DF_FloBoss.103.MeterProcessVariables_DateTimeStamp] DEFAULT (sysdatetime()) NOT NULL,
    [DifferentialPressure.AlarmingEnabled]   [ROCMaster].[Parameter] NOT NULL,
    [DifferentialPressure.ScanningEnabled]   [ROCMaster].[Parameter] NOT NULL,
    [DifferentialPressure.ScanPeriod]        [ROCMaster].[Parameter] NOT NULL,
    [DifferentialPressure.Filter]            [ROCMaster].[Parameter] NOT NULL,
    [DifferentialPressure.Units]             [ROCMaster].[Parameter] NOT NULL,
    [DifferentialPressure.Raw.Value]         [ROCMaster].[Parameter] NOT NULL,
    [DifferentialPressure.EU.Value]          [ROCMaster].[Parameter] NOT NULL,
    [DifferentialPressure.Raw.ZeroValue]     [ROCMaster].[Parameter] NOT NULL,
    [DifferentialPressure.Raw.SpanValue]     [ROCMaster].[Parameter] NOT NULL,
    [[DifferentialPressure.EU.ZeroValue]     [ROCMaster].[Parameter] NOT NULL,
    [DifferentialPressure.EU.SpanValue]      [ROCMaster].[Parameter] NOT NULL,
    [StaticPressure.AlarmingEnabled]         [ROCMaster].[Parameter] NOT NULL,
    [StaticPressurePressure.ScanningEnabled] [ROCMaster].[Parameter] NOT NULL,
    [StaticPressurePressure.ScanPeriod]      [ROCMaster].[Parameter] NOT NULL,
    [StaticPressurePressure.Filter]          [ROCMaster].[Parameter] NOT NULL,
    [StaticPressurePressure.Units]           [ROCMaster].[Parameter] NOT NULL,
    [StaticPressure.Raw.Value]               [ROCMaster].[Parameter] NOT NULL,
    [StaticPressure.EU.Value]                [ROCMaster].[Parameter] NOT NULL,
    [StaticPressure.Raw.ZeroValue]           [ROCMaster].[Parameter] NOT NULL,
    [StaticPressure.Raw.SpanValue]           [ROCMaster].[Parameter] NOT NULL,
    [StaticPressure.EU.ZeroValue]            [ROCMaster].[Parameter] NULL,
    [StaticPressure.EU.SpanValue]            [ROCMaster].[Parameter] NULL,
    [Temperature.AlarmingEnabled]            [ROCMaster].[Parameter] NOT NULL,
    [Temperature.ScanningEnabled]            [ROCMaster].[Parameter] NOT NULL,
    [Temperature.ScanPeriod]                 [ROCMaster].[Parameter] NOT NULL,
    [Temperature.Filter]                     [ROCMaster].[Parameter] NOT NULL,
    [Temperature.Units]                      [ROCMaster].[Parameter] NOT NULL,
    [Temperature.Raw.Value]                  [ROCMaster].[Parameter] NOT NULL,
    [Temperature.EU.Value]                   [ROCMaster].[Parameter] NOT NULL,
    [Temperature.Raw.ZeroValue]              [ROCMaster].[Parameter] NOT NULL,
    [Temperature.Raw.SpanValue]              [ROCMaster].[Parameter] NOT NULL,
    [Temperature.EU.ZeroValue]               [ROCMaster].[Parameter] NULL,
    [Temperature.EU.SpanValue]               [ROCMaster].[Parameter] NOT NULL,
    [RowVersion]                             ROWVERSION              NULL,
    CONSTRAINT [PK_FloBoss.103.MeterProcessVariables_1] PRIMARY KEY CLUSTERED ([ROCMeterId] ASC),
    CONSTRAINT [FK_FloBoss.103.MeterProcessVariables_ROCMeters] FOREIGN KEY ([ROCMeterId]) REFERENCES [ROCMaster].[ROCMeters] ([Id])
);


GO
CREATE NONCLUSTERED INDEX [IX_FloBoss.103.MeterProcessVariables]
    ON [ROCMaster].[FloBoss.103.MeterProcessVariables]([ROCMeterId] ASC, [DateTimeStamp] ASC);

