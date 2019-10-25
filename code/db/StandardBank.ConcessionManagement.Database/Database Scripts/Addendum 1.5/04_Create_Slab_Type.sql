CREATE TABLE [dbo].[tblSlabType](
	[pkSlabTypeId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Description] [varchar](50) NOT NULL,
	[IsActive] [bit] NOT NULL,
)
