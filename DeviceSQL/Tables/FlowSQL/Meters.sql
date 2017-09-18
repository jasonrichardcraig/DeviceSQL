CREATE TABLE [FlowSQL].[Meters] (
    [Id]          BIGINT        IDENTITY (1, 1) NOT NULL,
    [Name]        VARCHAR (255) NOT NULL,
    [Description] VARCHAR (255) NOT NULL,
    [RowVersion]  ROWVERSION    NULL,
    CONSTRAINT [PK_Meters] PRIMARY KEY CLUSTERED ([Id] ASC)
);

