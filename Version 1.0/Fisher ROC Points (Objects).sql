USE [FisherROCPoints]
GO
/****** Object:  Table [dbo].[AlarmCodes]    Script Date: 2018-02-08 1:35:33 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AlarmCodes](
	[AlarmCode] [tinyint] NOT NULL,
	[Description] [nvarchar](45) NULL,
 CONSTRAINT [PK_AlarmCodes] PRIMARY KEY CLUSTERED 
(
	[AlarmCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ArchiveType]    Script Date: 2018-02-08 1:35:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ArchiveType](
	[ArchiveTypeID] [int] NOT NULL,
	[ArchiveType] [int] NULL,
	[AvgRateType] [int] NULL,
	[Description] [nvarchar](50) NULL,
 CONSTRAINT [PK_ArchiveType] PRIMARY KEY CLUSTERED 
(
	[ArchiveTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ArchiveType_407]    Script Date: 2018-02-08 1:35:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ArchiveType_407](
	[ArchiveTypeID] [int] NOT NULL,
	[ArchiveType] [int] NULL,
	[AvgRateType] [int] NULL,
	[Description] [nvarchar](50) NULL,
 CONSTRAINT [PK_ArchiveType_407] PRIMARY KEY CLUSTERED 
(
	[ArchiveTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ArchiveType_500]    Script Date: 2018-02-08 1:35:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ArchiveType_500](
	[ArchiveTypeID] [int] NOT NULL,
	[ArchiveType] [int] NULL,
	[AvgRateType] [int] NULL,
	[Description] [nvarchar](50) NULL,
 CONSTRAINT [PK_ArchiveType_500] PRIMARY KEY CLUSTERED 
(
	[ArchiveTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ArchiveType_800L]    Script Date: 2018-02-08 1:35:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ArchiveType_800L](
	[ArchiveTypeID] [int] NOT NULL,
	[ArchiveType] [int] NULL,
	[AvgRateType] [int] NULL,
	[Description] [nvarchar](50) NULL,
 CONSTRAINT [PK_ArchiveType_800L] PRIMARY KEY CLUSTERED 
(
	[ArchiveTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ArchiveType_DL8000]    Script Date: 2018-02-08 1:35:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ArchiveType_DL8000](
	[ArchiveTypeID] [int] NOT NULL,
	[ArchiveType] [int] NULL,
	[AvgRateType] [int] NULL,
	[Description] [nvarchar](50) NULL,
 CONSTRAINT [PK_ArchiveType_DL8000] PRIMARY KEY CLUSTERED 
(
	[ArchiveTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ArchiveType_GB]    Script Date: 2018-02-08 1:35:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ArchiveType_GB](
	[ArchiveTypeID] [int] NOT NULL,
	[ArchiveType] [int] NULL,
	[AvgRateType] [int] NULL,
	[Description] [nvarchar](50) NULL,
 CONSTRAINT [PK_ArchiveType_GB] PRIMARY KEY CLUSTERED 
(
	[ArchiveTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ArchiveTypeExtendedHistory_100]    Script Date: 2018-02-08 1:35:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ArchiveTypeExtendedHistory_100](
	[ArchiveTypeID] [int] NOT NULL,
	[ArchiveType] [int] NULL,
	[AvgRateType] [int] NULL,
	[Description] [nvarchar](50) NULL,
 CONSTRAINT [PK_ArchiveTypeExtendedHistory_100] PRIMARY KEY CLUSTERED 
(
	[ArchiveTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ArchiveTypeExtendedHistory_103]    Script Date: 2018-02-08 1:35:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ArchiveTypeExtendedHistory_103](
	[ArchiveTypeID] [int] NOT NULL,
	[ArchiveType] [int] NULL,
	[AvgRateType] [int] NULL,
	[Description] [nvarchar](50) NULL,
 CONSTRAINT [PK_ArchiveTypeExtendedHistory_103] PRIMARY KEY CLUSTERED 
(
	[ArchiveTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ArchiveTypeMeter_407]    Script Date: 2018-02-08 1:35:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ArchiveTypeMeter_407](
	[ArchiveTypeID] [int] NOT NULL,
	[ArchiveType] [int] NULL,
	[AvgRateType] [int] NULL,
	[Description] [nvarchar](50) NULL,
 CONSTRAINT [PK_ArchiveTypeMeter_407] PRIMARY KEY CLUSTERED 
(
	[ArchiveTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ArchiveTypeStandardHistory_100]    Script Date: 2018-02-08 1:35:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ArchiveTypeStandardHistory_100](
	[ArchiveTypeID] [int] NOT NULL,
	[ArchiveType] [int] NULL,
	[AvgRateType] [int] NULL,
	[Description] [nvarchar](50) NULL,
 CONSTRAINT [PK_ArchiveTypeStandardHistory_100] PRIMARY KEY CLUSTERED 
(
	[ArchiveTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ArchiveTypeStandardHistory_103]    Script Date: 2018-02-08 1:35:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ArchiveTypeStandardHistory_103](
	[ArchiveTypeID] [int] NOT NULL,
	[ArchiveType] [int] NULL,
	[AvgRateType] [int] NULL,
	[Description] [nvarchar](50) NULL,
 CONSTRAINT [PK_ArchiveTypeStandardHistory_103] PRIMARY KEY CLUSTERED 
(
	[ArchiveTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CommBuff]    Script Date: 2018-02-08 1:35:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CommBuff](
	[ID] [int] NOT NULL,
	[RocFirmwareVersion] [nvarchar](50) NULL,
	[Comm0TxSegment] [nvarchar](50) NULL,
	[Comm0TxOffset] [nvarchar](50) NULL,
	[Comm0RxSegment] [nvarchar](50) NULL,
	[Comm0RxOffset] [nvarchar](50) NULL,
	[Comm1TxOffset] [nvarchar](50) NULL,
	[Comm1TxSegment] [nvarchar](50) NULL,
	[Comm1RxOffset] [nvarchar](50) NULL,
	[Comm1RxSegment] [nvarchar](50) NULL,
	[Comm2TxOffset] [nvarchar](50) NULL,
	[Comm2TxSegment] [nvarchar](50) NULL,
	[Comm2RxOffset] [nvarchar](50) NULL,
	[Comm2RxSegment] [nvarchar](50) NULL,
	[RocId] [smallint] NULL,
 CONSTRAINT [PK_CommBuff] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Conversion]    Script Date: 2018-02-08 1:35:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Conversion](
	[ConversionID] [int] NOT NULL,
	[Method] [smallint] NULL,
	[Value1] [real] NULL,
	[Value2] [real] NULL,
 CONSTRAINT [PK_Conversion] PRIMARY KEY CLUSTERED 
(
	[ConversionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DataTypes]    Script Date: 2018-02-08 1:35:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DataTypes](
	[DataType] [smallint] NOT NULL,
	[Description] [nvarchar](8) NULL,
	[Length] [smallint] NOT NULL,
 CONSTRAINT [PK_DataTypes] PRIMARY KEY CLUSTERED 
(
	[DataType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DefaultValuesDVS_100]    Script Date: 2018-02-08 1:35:34 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DefaultValuesDVS_100](
	[ConfigDataID] [int] NOT NULL,
	[PointType] [smallint] NULL,
	[PointNumber] [smallint] NULL,
	[Parameter] [smallint] NULL,
	[Value] [nvarchar](50) NULL,
 CONSTRAINT [PK_DefaultValuesDVS_100] PRIMARY KEY CLUSTERED 
(
	[ConfigDataID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DefaultValuesHistoryL1_100]    Script Date: 2018-02-08 1:35:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DefaultValuesHistoryL1_100](
	[ConfigDataID] [int] NOT NULL,
	[PointType] [smallint] NULL,
	[PointNumber] [smallint] NULL,
	[Parameter] [smallint] NULL,
	[Value] [nvarchar](50) NULL,
 CONSTRAINT [PK_DefaultValuesHistoryL1_100] PRIMARY KEY CLUSTERED 
(
	[ConfigDataID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DefaultValuesPIM_100]    Script Date: 2018-02-08 1:35:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DefaultValuesPIM_100](
	[ConfigDataID] [int] NOT NULL,
	[PointType] [smallint] NULL,
	[PointNumber] [smallint] NULL,
	[Parameter] [smallint] NULL,
	[Value] [nvarchar](50) NULL,
 CONSTRAINT [PK_DefaultValuesPIM_100] PRIMARY KEY CLUSTERED 
(
	[ConfigDataID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DefaultValuesSysAI_100]    Script Date: 2018-02-08 1:35:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DefaultValuesSysAI_100](
	[ConfigDataID] [int] NOT NULL,
	[PointType] [smallint] NULL,
	[PointNumber] [smallint] NULL,
	[Parameter] [smallint] NULL,
	[Value] [nvarchar](50) NULL,
 CONSTRAINT [PK_DefaultValuesSysAI_100] PRIMARY KEY CLUSTERED 
(
	[ConfigDataID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DefaultValuesXIO_100]    Script Date: 2018-02-08 1:35:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DefaultValuesXIO_100](
	[ConfigDataID] [int] NOT NULL,
	[PointType] [smallint] NULL,
	[PointNumber] [smallint] NULL,
	[Parameter] [smallint] NULL,
	[Value] [nvarchar](50) NULL,
 CONSTRAINT [PK_DefaultValuesXIO_100] PRIMARY KEY CLUSTERED 
(
	[ConfigDataID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EventCodes]    Script Date: 2018-02-08 1:35:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EventCodes](
	[EventCode] [tinyint] NOT NULL,
	[Description] [nvarchar](30) NULL,
 CONSTRAINT [PK_EventCodes] PRIMARY KEY CLUSTERED 
(
	[EventCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ExtraFstParameters]    Script Date: 2018-02-08 1:35:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExtraFstParameters](
	[ParameterID] [int] NOT NULL,
	[PointType] [tinyint] NULL,
	[Parameter] [tinyint] NULL,
	[ParameterX] [real] NULL,
	[Name] [nvarchar](max) NULL,
	[Access] [nvarchar](3) NULL,
	[DataType] [nvarchar](10) NULL,
	[Length] [tinyint] NULL,
	[Abbrev] [nvarchar](8) NULL,
 CONSTRAINT [PK_ExtraFstParameters] PRIMARY KEY CLUSTERED 
(
	[ParameterID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FstArgTypes]    Script Date: 2018-02-08 1:35:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FstArgTypes](
	[FstArgType] [smallint] NOT NULL,
	[Description] [nvarchar](50) NULL,
 CONSTRAINT [PK_FstArgTypes] PRIMARY KEY CLUSTERED 
(
	[FstArgType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FstCommandCategories]    Script Date: 2018-02-08 1:35:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FstCommandCategories](
	[CategoryID] [smallint] NOT NULL,
	[Description] [nvarchar](20) NULL,
 CONSTRAINT [PK_FstCommandCategories] PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FstCommands]    Script Date: 2018-02-08 1:35:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FstCommands](
	[IDX] [smallint] NOT NULL,
	[CategoryID] [smallint] NULL,
	[CMD] [nvarchar](3) NULL,
	[DESC] [nvarchar](50) NULL,
	[NumArgs] [smallint] NULL,
	[Arg1Type] [smallint] NULL,
	[Arg2Type] [smallint] NULL,
 CONSTRAINT [PK_FstCommands] PRIMARY KEY CLUSTERED 
(
	[IDX] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HistoryDefAGA3_100]    Script Date: 2018-02-08 1:35:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HistoryDefAGA3_100](
	[HistoryDefID] [int] NOT NULL,
	[PointType] [tinyint] NULL,
	[Parameter] [tinyint] NULL,
	[ArchiveType] [tinyint] NULL,
	[AvgRateType] [tinyint] NULL,
	[Description] [nvarchar](30) NULL,
 CONSTRAINT [PK_HistoryDefAGA3_100] PRIMARY KEY CLUSTERED 
(
	[HistoryDefID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HistoryDefAGA3_800]    Script Date: 2018-02-08 1:35:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HistoryDefAGA3_800](
	[HistoryDefID] [int] NOT NULL,
	[PointType] [tinyint] NULL,
	[Parameter] [tinyint] NULL,
	[ArchiveType] [tinyint] NULL,
	[AvgRateType] [tinyint] NULL,
	[Description] [nvarchar](30) NULL,
 CONSTRAINT [PK_HistoryDefAGA3_800] PRIMARY KEY CLUSTERED 
(
	[HistoryDefID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HistoryDefAGA7_100]    Script Date: 2018-02-08 1:35:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HistoryDefAGA7_100](
	[HistoryDefID] [int] NOT NULL,
	[PointType] [tinyint] NULL,
	[Parameter] [tinyint] NULL,
	[ArchiveType] [tinyint] NULL,
	[AvgRateType] [tinyint] NULL,
	[Description] [nvarchar](30) NULL,
 CONSTRAINT [PK_HistoryDefAGA7_100] PRIMARY KEY CLUSTERED 
(
	[HistoryDefID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HistoryDefAGA7_800]    Script Date: 2018-02-08 1:35:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HistoryDefAGA7_800](
	[HistoryDefID] [int] NOT NULL,
	[PointType] [tinyint] NULL,
	[Parameter] [tinyint] NULL,
	[ArchiveType] [tinyint] NULL,
	[AvgRateType] [tinyint] NULL,
	[Description] [nvarchar](30) NULL,
 CONSTRAINT [PK_HistoryDefAGA7_800] PRIMARY KEY CLUSTERED 
(
	[HistoryDefID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HistoryDefAGA7_Kf_100]    Script Date: 2018-02-08 1:35:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HistoryDefAGA7_Kf_100](
	[HistoryDefID] [int] NOT NULL,
	[PointType] [tinyint] NULL,
	[Parameter] [tinyint] NULL,
	[ArchiveType] [tinyint] NULL,
	[AvgRateType] [tinyint] NULL,
	[Description] [nvarchar](30) NULL,
 CONSTRAINT [PK_HistoryDefAGA7_Kf_100] PRIMARY KEY CLUSTERED 
(
	[HistoryDefID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HistoryDefAGA7_Kf_800]    Script Date: 2018-02-08 1:35:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HistoryDefAGA7_Kf_800](
	[HistoryDefID] [int] NOT NULL,
	[PointType] [tinyint] NULL,
	[Parameter] [tinyint] NULL,
	[ArchiveType] [tinyint] NULL,
	[AvgRateType] [tinyint] NULL,
	[Description] [nvarchar](30) NULL,
 CONSTRAINT [PK_HistoryDefAGA7_Kf_800] PRIMARY KEY CLUSTERED 
(
	[HistoryDefID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HistoryDefLiquidBasic_800L]    Script Date: 2018-02-08 1:35:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HistoryDefLiquidBasic_800L](
	[HistoryDefID] [int] NOT NULL,
	[PointType] [tinyint] NULL,
	[Parameter] [tinyint] NULL,
	[ArchiveType] [tinyint] NULL,
	[AvgRateType] [tinyint] NULL,
	[Description] [nvarchar](30) NULL,
	[ShortDesc] [nvarchar](255) NULL,
 CONSTRAINT [PK_HistoryDefLiquidBasic_800L] PRIMARY KEY CLUSTERED 
(
	[HistoryDefID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HistoryDefLiquidBasicWithGasComp_800L]    Script Date: 2018-02-08 1:35:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HistoryDefLiquidBasicWithGasComp_800L](
	[HistoryDefID] [int] NOT NULL,
	[PointType] [tinyint] NULL,
	[Parameter] [tinyint] NULL,
	[ArchiveType] [tinyint] NULL,
	[AvgRateType] [tinyint] NULL,
	[Description] [nvarchar](30) NULL,
	[ShortDesc] [nvarchar](255) NULL,
 CONSTRAINT [PK_HistoryDefLiquidBasicWithGasComp_800L] PRIMARY KEY CLUSTERED 
(
	[HistoryDefID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HistoryDefMass_100]    Script Date: 2018-02-08 1:35:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HistoryDefMass_100](
	[HistoryDefID] [int] NOT NULL,
	[PointType] [tinyint] NULL,
	[Parameter] [tinyint] NULL,
	[ArchiveType] [tinyint] NULL,
	[AvgRateType] [tinyint] NULL,
	[Description] [nvarchar](30) NULL,
 CONSTRAINT [PK_HistoryDefMass_100] PRIMARY KEY CLUSTERED 
(
	[HistoryDefID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HistoryDefMass_800]    Script Date: 2018-02-08 1:35:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HistoryDefMass_800](
	[HistoryDefID] [int] NOT NULL,
	[PointType] [tinyint] NULL,
	[Parameter] [tinyint] NULL,
	[ArchiveType] [tinyint] NULL,
	[AvgRateType] [tinyint] NULL,
	[Description] [nvarchar](30) NULL,
 CONSTRAINT [PK_HistoryDefMass_800] PRIMARY KEY CLUSTERED 
(
	[HistoryDefID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LinkGroupList]    Script Date: 2018-02-08 1:35:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LinkGroupList](
	[LinkGroupListID] [int] NOT NULL,
	[TLPListID] [int] NULL,
	[ParamGroupID] [int] NULL,
	[Sequence] [int] NULL,
 CONSTRAINT [PK_LinkGroupList] PRIMARY KEY CLUSTERED 
(
	[LinkGroupListID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LinkParameterGroup]    Script Date: 2018-02-08 1:35:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LinkParameterGroup](
	[LinkParameterGroupID] [int] NOT NULL,
	[ParameterGroupID] [int] NULL,
	[ParameterID] [int] NULL,
	[Sequence] [int] NULL,
 CONSTRAINT [PK_LinkParameterGroup] PRIMARY KEY CLUSTERED 
(
	[LinkParameterGroupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LinkParameterRocTypeVersion]    Script Date: 2018-02-08 1:35:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LinkParameterRocTypeVersion](
	[LinkParameterRocTypeVersionID] [int] NOT NULL,
	[ParameterID] [int] NULL,
	[RocVersionID] [int] NULL,
	[RocTypeID] [int] NULL,
 CONSTRAINT [PK_LinkParameterRocTypeVersion] PRIMARY KEY CLUSTERED 
(
	[LinkParameterRocTypeVersionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Menu]    Script Date: 2018-02-08 1:35:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Menu](
	[MenuID] [int] NOT NULL,
	[Name] [nvarchar](50) NULL,
	[ParentID] [int] NULL,
	[SendKey] [nvarchar](1) NULL,
	[Note] [nvarchar](32) NULL,
 CONSTRAINT [PK_Menu] PRIMARY KEY CLUSTERED 
(
	[MenuID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MenuDefaults]    Script Date: 2018-02-08 1:35:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MenuDefaults](
	[ID] [int] NOT NULL,
	[ParentID] [int] NULL,
	[Name] [nvarchar](50) NULL,
	[Index] [int] NULL,
	[ScreenFunction] [int] NULL,
	[Description] [nvarchar](50) NULL,
	[ToolbarButton] [int] NULL,
	[ReadLevel] [int] NULL,
	[WriteLevel] [int] NULL,
	[Sequence] [int] NULL,
	[SameAs] [int] NULL,
	[Hide] [bit] NOT NULL,
 CONSTRAINT [PK_MenuDefaults] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MenuTop]    Script Date: 2018-02-08 1:35:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MenuTop](
	[ID] [int] NOT NULL,
	[ParentName] [nvarchar](50) NULL,
 CONSTRAINT [PK_MenuTop] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ParameterGroup]    Script Date: 2018-02-08 1:35:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ParameterGroup](
	[ParameterGroupID] [int] NOT NULL,
	[Name] [nvarchar](25) NULL,
 CONSTRAINT [PK_ParameterGroup] PRIMARY KEY CLUSTERED 
(
	[ParameterGroupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Parameters]    Script Date: 2018-02-08 1:35:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Parameters](
	[ParameterID] [int] NOT NULL,
	[PointType] [smallint] NULL,
	[Parameter] [real] NULL,
	[Name] [nvarchar](60) NULL,
	[Access] [nvarchar](30) NULL,
	[SystemOrUserUpdate] [nvarchar](6) NULL,
	[DataType] [nvarchar](10) NULL,
	[Length] [tinyint] NULL,
	[Range] [nvarchar](255) NULL,
	[Default] [nvarchar](120) NULL,
	[Description] [nvarchar](max) NULL,
	[ConversionID] [int] NULL,
	[RangeCheckID] [int] NULL,
	[Abbrev] [nvarchar](8) NULL,
	[Sequence] [smallint] NULL,
 CONSTRAINT [PK_Parameters] PRIMARY KEY CLUSTERED 
(
	[ParameterID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PointTypes]    Script Date: 2018-02-08 1:35:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PointTypes](
	[PointType] [smallint] NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
	[Abbrev] [nvarchar](10) NULL,
 CONSTRAINT [PK_PointTypes] PRIMARY KEY CLUSTERED 
(
	[PointType] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Range]    Script Date: 2018-02-08 1:35:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Range](
	[RangeCheckID] [int] NOT NULL,
	[LowType] [int] NULL,
	[LowLimit] [float] NULL,
	[HighType] [int] NULL,
	[HighLimit] [float] NULL,
 CONSTRAINT [PK_Range] PRIMARY KEY CLUSTERED 
(
	[RangeCheckID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RocTypes]    Script Date: 2018-02-08 1:35:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RocTypes](
	[RocTypeID] [smallint] NOT NULL,
	[RocDescription] [nvarchar](24) NULL,
 CONSTRAINT [PK_RocTypes] PRIMARY KEY CLUSTERED 
(
	[RocTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RocVersion]    Script Date: 2018-02-08 1:35:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RocVersion](
	[RocVersionID] [int] NOT NULL,
	[RocTypeID] [int] NULL,
	[Version] [nvarchar](12) NULL,
 CONSTRAINT [PK_RocVersion] PRIMARY KEY CLUSTERED 
(
	[RocVersionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RocVersionAll]    Script Date: 2018-02-08 1:35:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RocVersionAll](
	[RocVersionAllID] [int] NOT NULL,
	[Version] [nvarchar](12) NULL,
	[RocTypeID] [int] NULL,
	[RocVersionID] [int] NULL,
 CONSTRAINT [PK_RocVersionAll] PRIMARY KEY CLUSTERED 
(
	[RocVersionAllID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TlpList]    Script Date: 2018-02-08 1:35:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TlpList](
	[TLPListID] [int] NOT NULL,
	[ListName] [nvarchar](50) NULL,
	[ListNumber] [int] NULL,
 CONSTRAINT [PK_TlpList] PRIMARY KEY CLUSTERED 
(
	[TLPListID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Units]    Script Date: 2018-02-08 1:35:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Units](
	[Parameter] [smallint] NOT NULL,
	[Description] [nvarchar](32) NULL,
	[English] [nvarchar](12) NULL,
	[Metric] [nvarchar](12) NULL,
 CONSTRAINT [PK_Units] PRIMARY KEY CLUSTERED 
(
	[Parameter] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Version]    Script Date: 2018-02-08 1:35:36 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Version](
	[Version] [nvarchar](6) NOT NULL,
	[ROCPartNumberVersion] [nvarchar](20) NULL,
 CONSTRAINT [PK_Version] PRIMARY KEY CLUSTERED 
(
	[Version] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[FstCommands]  WITH CHECK ADD  CONSTRAINT [FK_FstCommands_FstArgTypes] FOREIGN KEY([Arg1Type])
REFERENCES [dbo].[FstArgTypes] ([FstArgType])
GO
ALTER TABLE [dbo].[FstCommands] CHECK CONSTRAINT [FK_FstCommands_FstArgTypes]
GO
ALTER TABLE [dbo].[FstCommands]  WITH CHECK ADD  CONSTRAINT [FK_FstCommands_FstArgTypes1] FOREIGN KEY([Arg2Type])
REFERENCES [dbo].[FstArgTypes] ([FstArgType])
GO
ALTER TABLE [dbo].[FstCommands] CHECK CONSTRAINT [FK_FstCommands_FstArgTypes1]
GO
ALTER TABLE [dbo].[FstCommands]  WITH CHECK ADD  CONSTRAINT [FK_FstCommands_FstCommandCategories] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[FstCommandCategories] ([CategoryID])
GO
ALTER TABLE [dbo].[FstCommands] CHECK CONSTRAINT [FK_FstCommands_FstCommandCategories]
GO
ALTER TABLE [dbo].[LinkGroupList]  WITH CHECK ADD  CONSTRAINT [FK_LinkGroupList_ParameterGroup] FOREIGN KEY([ParamGroupID])
REFERENCES [dbo].[ParameterGroup] ([ParameterGroupID])
GO
ALTER TABLE [dbo].[LinkGroupList] CHECK CONSTRAINT [FK_LinkGroupList_ParameterGroup]
GO
ALTER TABLE [dbo].[LinkGroupList]  WITH CHECK ADD  CONSTRAINT [FK_LinkGroupList_TlpList] FOREIGN KEY([TLPListID])
REFERENCES [dbo].[TlpList] ([TLPListID])
GO
ALTER TABLE [dbo].[LinkGroupList] CHECK CONSTRAINT [FK_LinkGroupList_TlpList]
GO
ALTER TABLE [dbo].[LinkParameterGroup]  WITH CHECK ADD  CONSTRAINT [FK_LinkParameterGroup_ParameterGroup] FOREIGN KEY([ParameterGroupID])
REFERENCES [dbo].[ParameterGroup] ([ParameterGroupID])
GO
ALTER TABLE [dbo].[LinkParameterGroup] CHECK CONSTRAINT [FK_LinkParameterGroup_ParameterGroup]
GO
ALTER TABLE [dbo].[LinkParameterGroup]  WITH CHECK ADD  CONSTRAINT [FK_LinkParameterGroup_Parameters] FOREIGN KEY([ParameterID])
REFERENCES [dbo].[Parameters] ([ParameterID])
GO
ALTER TABLE [dbo].[LinkParameterGroup] CHECK CONSTRAINT [FK_LinkParameterGroup_Parameters]
GO
ALTER TABLE [dbo].[Parameters]  WITH CHECK ADD  CONSTRAINT [FK_Parameters_Conversion] FOREIGN KEY([ConversionID])
REFERENCES [dbo].[Conversion] ([ConversionID])
GO
ALTER TABLE [dbo].[Parameters] CHECK CONSTRAINT [FK_Parameters_Conversion]
GO
ALTER TABLE [dbo].[Parameters]  WITH CHECK ADD  CONSTRAINT [FK_Parameters_PointTypes] FOREIGN KEY([PointType])
REFERENCES [dbo].[PointTypes] ([PointType])
GO
ALTER TABLE [dbo].[Parameters] CHECK CONSTRAINT [FK_Parameters_PointTypes]
GO
ALTER TABLE [dbo].[Parameters]  WITH CHECK ADD  CONSTRAINT [FK_Parameters_Range] FOREIGN KEY([RangeCheckID])
REFERENCES [dbo].[Range] ([RangeCheckID])
GO
ALTER TABLE [dbo].[Parameters] CHECK CONSTRAINT [FK_Parameters_Range]
GO
