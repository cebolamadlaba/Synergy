USE [CMS_Dev_V2]
GO



CREATE TABLE [dbo].[tblRateType](
	[pkRateTypeId] [int] IDENTITY(1,1) PRIMARY KEY NOT NULL,
	[Description] [varchar](50) NOT NULL,
	[IsActive] [bit] NOT NULL,
)

