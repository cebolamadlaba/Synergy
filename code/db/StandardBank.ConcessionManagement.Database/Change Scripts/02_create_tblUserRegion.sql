CREATE TABLE [dbo].[tblUserRegion](
	[pkUserRegionId] [int] IDENTITY(1,1) NOT NULL,
	[fkUserId] [int] NOT NULL,
	[fkRegionId] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[IsSelected] [bit] NOT NULL,
 CONSTRAINT [PK_tblUserRegion] PRIMARY KEY CLUSTERED 
(
	[pkUserRegionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[tblUserRegion] ADD  CONSTRAINT [DF_tblUserRegion_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO

ALTER TABLE [dbo].[tblUserRegion] ADD  CONSTRAINT [DF_tblUserRegion_IsSelected]  DEFAULT ((0)) FOR [IsSelected]
GO


