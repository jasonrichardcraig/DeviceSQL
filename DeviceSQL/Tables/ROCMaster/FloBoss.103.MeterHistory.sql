CREATE TABLE [RocMaster].[FloBoss.103.MeterHistory] (
    [Id]                   BIGINT                      IDENTITY (1, 1) NOT NULL,
    [RocMeterId]           INT                         NOT NULL,
    [FlowDuration]         [RocMaster].[HistoryRecord] NOT NULL,
    [DIfferentialPressure] [RocMaster].[HistoryRecord] NOT NULL,
    [StaticPressure]       [RocMaster].[HistoryRecord] NOT NULL,
    [Temperature]          [RocMaster].[HistoryRecord] NOT NULL,
    [IMV]                  [RocMaster].[HistoryRecord] NOT NULL,
    [HwPf]                 [RocMaster].[HistoryRecord] NOT NULL,
    [Energy]               [RocMaster].[HistoryRecord] NOT NULL,
    [Volume]               [RocMaster].[HistoryRecord] NOT NULL,
    [RowVersion]           ROWVERSION                  NULL,
    CONSTRAINT [PK_FloBoss.103.MeterHistory] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FloBoss.103.MeterHistory_RocMeters] FOREIGN KEY ([Id]) REFERENCES [RocMaster].[RocMeters] ([Id])
);

