CREATE TABLE tblGlmsTierData (
    GlmsTierDataId INT PRIMARY KEY IDENTITY (1, 1),
    fkGlmsConcessionId INT NOT NULL,
    TierFrom decimal(18, 2)NOT NULL,
    TierTo decimal(18, 2) NOT NULL ,
    fkRateTypeId INT  ,
    fkBaseRateId INT ,
	Spread decimal(18, 2),
	Value decimal(18, 2)
);


ALTER TABLE [dbo].[tblGlmsTierData]  WITH CHECK ADD FOREIGN KEY([fkRateTypeId])
REFERENCES [dbo].[tblRateType] ([pkRateTypeId])
GO


ALTER TABLE [dbo].[tblGlmsTierData]  WITH CHECK ADD FOREIGN KEY([fkBaseRateId])
REFERENCES [dbo].[tblBaseRateCode] ([pkBaseRateCodeId])

GO

ALTER TABLE [dbo].[tblGlmsTierData]  WITH CHECK ADD FOREIGN KEY([fkGlmsConcessionId])
REFERENCES [dbo].[tblConcessionGlms] ([pkConcessionGlmsId])

GO
