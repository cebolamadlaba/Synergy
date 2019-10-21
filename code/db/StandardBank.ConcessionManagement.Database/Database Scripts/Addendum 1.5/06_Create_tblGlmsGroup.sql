CREATE TABLE [dbo].[tblGlmsGroup](
	[pkGlmsGroupId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[GroupNumber] int  NOT NULL,
	[GroupName] [varchar](200) NOT NULL,
	[IsActive] [bit] NOT NULL,
)
