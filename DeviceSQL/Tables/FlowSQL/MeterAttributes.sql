CREATE TABLE [FlowSQL].[MeterAttributes] (
    [MeterId]        BIGINT        NOT NULL,
    [AttributeName]  VARCHAR (255) NOT NULL,
    [AttributeValue] SQL_VARIANT   NULL,
    [RowVersion]     ROWVERSION    NULL,
    CONSTRAINT [PK_MeterAttributes] PRIMARY KEY CLUSTERED ([MeterId] ASC, [AttributeName] ASC),
    CONSTRAINT [FK_MeterAttributes_Meters] FOREIGN KEY ([MeterId]) REFERENCES [FlowSQL].[Meters] ([Id])
);

