/****** Object:  Table [dbo].[UFRecords]    Script Date: 2/29/2016 19:23:04 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UFUserFormSecurity](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[User] [nvarchar](50) NOT NULL,
	[Form] [uniqueidentifier] NOT NULL,
	[HasAccess] [bit] NOT NULL,
	[AllowInEditor] [bit] NOT NULL,
	[SecurityType] [int] NOT NULL,
 CONSTRAINT [Id] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO



CREATE TABLE [dbo].[UFUserSecurity](
	[User] [nvarchar](50) NOT NULL,
	[ManageDataSources] [bit] NOT NULL,
	[ManagePreValueSources] [bit] NOT NULL,
	[ManageWorkflows] [bit] NOT NULL,
	[ManageForms] [bit] NOT NULL
) ON [PRIMARY]

GO



CREATE TABLE [dbo].[UFRecords](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Form] [uniqueidentifier] NOT NULL,
	[Created] [datetime] NOT NULL,
	[Updated] [datetime] NOT NULL,
	[CurrentPage] [uniqueidentifier] NULL,
	[UmbracoPageId] [int] NULL,
	[IP] [nvarchar](255) NULL,
	[MemberKey] [nvarchar](255) NULL,
	[UniqueId] [uniqueidentifier] NOT NULL,
	[State] [nvarchar](50) NULL,
	[RecordData] [ntext] NOT NULL,
 CONSTRAINT [PK_UFRecords] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

CREATE TABLE [dbo].[UFRecordFields](
	[Key] [uniqueidentifier] NOT NULL,
	[FieldId] [uniqueidentifier] NOT NULL,
	[Record] [int] NOT NULL,
	[Alias] [nvarchar](255) NOT NULL,
	[DataType] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_UFRecordFields] PRIMARY KEY CLUSTERED 
(
	[Key] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

CREATE TABLE [dbo].[UFRecordDataBit](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Key] [uniqueidentifier] NOT NULL,
	[Value] [bit] NULL,
 CONSTRAINT [PK_UFRecordDataBit] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[UFRecordDataBit]  WITH CHECK ADD  CONSTRAINT [FK_UFRecordDataBit_UFRecordFields_Key] FOREIGN KEY([Key])
REFERENCES [dbo].[UFRecordFields] ([Key])
GO

ALTER TABLE [dbo].[UFRecordDataBit] CHECK CONSTRAINT [FK_UFRecordDataBit_UFRecordFields_Key]
GO

CREATE TABLE [dbo].[UFRecordDataDateTime](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Key] [uniqueidentifier] NOT NULL,
	[Value] [datetime] NULL,
 CONSTRAINT [PK_UFRecordDataDateTime] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[UFRecordDataDateTime]  WITH CHECK ADD  CONSTRAINT [FK_UFRecordDataDateTime_UFRecordFields_Key] FOREIGN KEY([Key])
REFERENCES [dbo].[UFRecordFields] ([Key])
GO

ALTER TABLE [dbo].[UFRecordDataDateTime] CHECK CONSTRAINT [FK_UFRecordDataDateTime_UFRecordFields_Key]
GO



CREATE TABLE [dbo].[UFRecordDataInteger](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Key] [uniqueidentifier] NOT NULL,
	[Value] [int] NULL,
 CONSTRAINT [PK_UFRecordDataInteger] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[UFRecordDataInteger]  WITH CHECK ADD  CONSTRAINT [FK_UFRecordDataInteger_UFRecordFields_Key] FOREIGN KEY([Key])
REFERENCES [dbo].[UFRecordFields] ([Key])
GO

ALTER TABLE [dbo].[UFRecordDataInteger] CHECK CONSTRAINT [FK_UFRecordDataInteger_UFRecordFields_Key]
GO



CREATE TABLE [dbo].[UFRecordDataLongString](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Key] [uniqueidentifier] NOT NULL,
	[Value] [ntext] NULL,
 CONSTRAINT [PK_UFRecordDataLongString] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[UFRecordDataLongString]  WITH CHECK ADD  CONSTRAINT [FK_UFRecordDataLongString_UFRecordFields_Key] FOREIGN KEY([Key])
REFERENCES [dbo].[UFRecordFields] ([Key])
GO

ALTER TABLE [dbo].[UFRecordDataLongString] CHECK CONSTRAINT [FK_UFRecordDataLongString_UFRecordFields_Key]
GO




CREATE TABLE [dbo].[UFRecordDataString](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Key] [uniqueidentifier] NOT NULL,
	[Value] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_UFRecordDataString] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[UFRecordDataString]  WITH CHECK ADD  CONSTRAINT [FK_UFRecordDataString_UFRecordFields_Key] FOREIGN KEY([Key])
REFERENCES [dbo].[UFRecordFields] ([Key])
GO

ALTER TABLE [dbo].[UFRecordDataString] CHECK CONSTRAINT [FK_UFRecordDataString_UFRecordFields_Key]
GO


