CREATE TABLE [dbo].[tblInterestType](
	[pkInterestTypeId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Description] [varchar](50) NOT NULL,
	[IsActive] [bit] NOT NULL,
)
