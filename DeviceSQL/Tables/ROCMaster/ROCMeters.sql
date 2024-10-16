CREATE TABLE [RocMaster].[RocMeters] (
    [Id]                BIGINT        NOT NULL,
    [Name]              VARCHAR (255) NOT NULL,
    [Description]       VARCHAR (255) NOT NULL,
    [RocChannelId]      BIGINT        NOT NULL,
    [RocChannelGroupId] INT           NOT NULL,
    [DeviceAddress]     TINYINT       NOT NULL,
    [DeviceGroup]       TINYINT       NOT NULL,
    [RowVersion]        ROWVERSION    NULL,
    CONSTRAINT [PK_RocMeters] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_RocMeters_RocChannelGroup] FOREIGN KEY ([RocChannelGroupId]) REFERENCES [RocMaster].[RocChannelGroup] ([Id]),
    CONSTRAINT [FK_RocMeters_RocChannels] FOREIGN KEY ([RocChannelId]) REFERENCES [RocMaster].[RocChannels] ([Id])
);

