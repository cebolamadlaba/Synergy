-- 4.1 --
-- Insert BOL Charge Code Type --

Insert Into rtblBOLChargeCodeType ([Description])
Values ('BOL SALARY PAYMENTS')


-- Create BOLChargeCodeRelationship --

CREATE TABLE [dbo].[rtblBOLChargeCodeRelationship](
	[pkBOLChargeCodeRelationshipId] [int] IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[fkChargeCodeTypeId] [int] NOT NULL,
	[fkChargeCodeId] [int] NOT NULL,
)

ALTER TABLE [dbo].[rtblBOLChargeCodeRelationship]  WITH CHECK ADD FOREIGN KEY([fkChargeCodeTypeId])
REFERENCES [dbo].[rtblBOLChargeCodeType] ([pkChargeCodeTypeId])
GO

ALTER TABLE [dbo].[rtblBOLChargeCodeRelationship]  WITH CHECK ADD FOREIGN KEY([fkChargeCodeId])
REFERENCES [dbo].[rtblBOLChargeCode] ([pkChargeCodeId])
GO

-- Insert existing links to BOLChargeCodeRelationship --

Insert Into [dbo].[rtblBOLChargeCodeRelationship] ([fkChargeCodeTypeId], [fkChargeCodeId])
Select	Distinct cct.[pkChargeCodeTypeId], cc.[pkChargeCodeId]
From [dbo].[rtblBOLChargeCodeType] cct
Inner Join	[dbo].[rtblBOLChargeCode] cc On	cc.fkChargeCodeTypeId =	cct.pkChargeCodeTypeId
Order By cct.pkChargeCodeTypeId

-- Link Charge Codes to "BOL Salary Payments" Charge code type --

Declare @chargeCodeTypeId Int

Select @chargeCodeTypeId = pkChargeCodeTypeId
From [dbo].[rtblBOLChargeCodeType]
Where Description like '%BOL SALARY PAYMENTS%'

Insert Into [dbo].[rtblBOLChargeCodeRelationship] ([fkChargeCodeTypeId], [fkChargeCodeId])
Select @chargeCodeTypeId, pkChargeCodeId
From rtblBOLChargeCode
Where ChargeCode In ('BCN0', 'BCV0', 'BFN0', 'BFV0', 'BAS0', 'SVA7', 'SVF5', 'SVA6', 'SVP4', 'EFTB', 'EFTP')

-- Optional To Drop the foreign key and the column ---

ALTER TABLE [dbo].[rtblBOLChargeCodeTest] 
DROP CONSTRAINT FK_rtblChargeCode_rtblChargeCodeType

ALTER TABLE [dbo].[rtblBOLChargeCode] 
DROP COLUMN fkChargeCodeTypeId


