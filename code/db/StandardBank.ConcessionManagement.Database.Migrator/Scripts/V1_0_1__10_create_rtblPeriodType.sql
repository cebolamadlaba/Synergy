CREATE TABLE [dbo].[rtblPeriodType](
	[pkPeriodTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](50) NOT NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_rtblPeriodType] PRIMARY KEY CLUSTERED 
(
	[pkPeriodTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[rtblPeriodType] ADD  CONSTRAINT [DF_rtblPeriodType_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO


