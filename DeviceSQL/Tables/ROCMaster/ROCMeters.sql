CREATE TABLE [ROCMaster].[ROCMeters] (
    [Id]                BIGINT        NOT NULL,
    [Name]              VARCHAR (255) NOT NULL,
    [Description]       VARCHAR (255) NOT NULL,
    [ROCChannelId]      BIGINT        NOT NULL,
    [ROCChannelGroupId] INT           NOT NULL,
    [DeviceAddress]     TINYINT       NOT NULL,
    [DeviceGroup]       TINYINT       NOT NULL,
    [RowVersion]        ROWVERSION    NULL,
    CONSTRAINT [PK_ROCMeters] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_ROCMeters_ROCChannelGroup] FOREIGN KEY ([ROCChannelGroupId]) REFERENCES [ROCMaster].[ROCChannelGroup] ([Id]),
    CONSTRAINT [FK_ROCMeters_ROCChannels] FOREIGN KEY ([ROCChannelId]) REFERENCES [ROCMaster].[ROCChannels] ([Id])
);

