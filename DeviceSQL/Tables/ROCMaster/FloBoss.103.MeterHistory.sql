CREATE TABLE [ROCMaster].[FloBoss.103.MeterHistory] (
    [Id]                   BIGINT                      IDENTITY (1, 1) NOT NULL,
    [ROCMeterId]           INT                         NOT NULL,
    [FlowDuration]         [ROCMaster].[HistoryRecord] NOT NULL,
    [DIfferentialPressure] [ROCMaster].[HistoryRecord] NOT NULL,
    [StaticPressure]       [ROCMaster].[HistoryRecord] NOT NULL,
    [Temperature]          [ROCMaster].[HistoryRecord] NOT NULL,
    [IMV]                  [ROCMaster].[HistoryRecord] NOT NULL,
    [HwPf]                 [ROCMaster].[HistoryRecord] NOT NULL,
    [Energy]               [ROCMaster].[HistoryRecord] NOT NULL,
    [Volume]               [ROCMaster].[HistoryRecord] NOT NULL,
    [RowVersion]           ROWVERSION                  NULL,
    CONSTRAINT [PK_FloBoss.103.MeterHistory] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_FloBoss.103.MeterHistory_ROCMeters] FOREIGN KEY ([Id]) REFERENCES [ROCMaster].[ROCMeters] ([Id])
);

