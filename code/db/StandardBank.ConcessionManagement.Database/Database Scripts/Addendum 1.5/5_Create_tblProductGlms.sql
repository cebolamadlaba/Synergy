
CREATE TABLE [dbo].[tblProductGlms](
	[pkProductGlmsId] [int] IDENTITY(1,1) NOT NULL,
	[fkRiskGroupId] [int] NULL,
	[fkLegalEntityId] [int] NULL,
	fkLegalEntityAccountId [int] NULL,
	[fkGroupId] [int] NULL,
	[GroupType] [varchar](50) NULL,
	[fkInterestTypeId] [int] NULL,
	[fkSlabTypeId] [int] NULL,
	[fkInterestPricingCategoryId] [int] NULL,
	[TierFrom] [int] NULL,
	[TierTo] [int] NULL,
	[RateType] [varchar](50) NULL,
	[fkBaseRateCodeId] [int] NULL,
	[Spread] DECIMAL (10, 2) NULL
	)

ALTER TABLE tblProductGlms
ADD FOREIGN KEY (fkRiskGroupId) REFERENCES tblRiskGroup(pkRiskGroupId);

ALTER TABLE tblProductGlms
ADD FOREIGN KEY (fkLegalEntityId) REFERENCES tblLegalEntity(pkLegalEntityId);

ALTER TABLE tblProductGlms
ADD FOREIGN KEY (fkInterestTypeId) REFERENCES tblInterestType(pkInterestTypeId);

ALTER TABLE tblProductGlms
ADD FOREIGN KEY (fkSlabTypeId) REFERENCES tblSlabType(pkSlabTypeId);

ALTER TABLE tblProductGlms
ADD FOREIGN KEY (fkInterestPricingCategoryId) REFERENCES tblInterestPricingCategory(pkInterestPricingCategoryId);

ALTER TABLE tblProductGlms
ADD FOREIGN KEY (fkBaseRateCodeId) REFERENCES tblBaseRateCode(pkBaseRateCodeId);

ALTER TABLE tblProductGlms
ADD FOREIGN KEY (fkGroupId) REFERENCES tblGlmsGroup(pkGlmsGroupId);

