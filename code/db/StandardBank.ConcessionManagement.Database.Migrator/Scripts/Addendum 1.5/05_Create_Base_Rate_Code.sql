CREATE TABLE [dbo].[tblBaseRateCode](
	[pkBaseRateCodeId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Description] [varchar](50) NOT NULL,
	[IsActive] [bit] NOT NULL,
)
