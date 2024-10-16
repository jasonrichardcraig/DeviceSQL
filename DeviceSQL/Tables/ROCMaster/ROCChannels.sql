CREATE TABLE [RocMaster].[RocChannels] (
    [Id]                  BIGINT         IDENTITY (1, 1) NOT NULL,
    [Name]                VARCHAR (1024) NOT NULL,
    [IsSerialPortChannel] BIT            NOT NULL,
    [PortName]            VARCHAR (8)    NOT NULL,
    [BaudRate]            INT            NOT NULL,
    [HostName]            VARCHAR (255)  NOT NULL,
    [HostPort]            INT            NOT NULL,
    [RowVersion]          ROWVERSION     NULL,
    CONSTRAINT [PK_RocChannels] PRIMARY KEY CLUSTERED ([Id] ASC)
);

