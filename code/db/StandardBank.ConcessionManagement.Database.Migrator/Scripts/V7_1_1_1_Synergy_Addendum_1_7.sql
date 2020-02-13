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

-- Create GLMS Standard Pricing --

CREATE TABLE [dbo].[rtblGlmsStandardPricing](
	[pkGlmsStandardPricingId] int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[BalanceFrom] int NOT NULL,
	[BalanceTo] int NULL,
	[FixedTiered] decimal(5,2) NOT NULL,
	[PrimeLinked] decimal(5,2) NOT NULL,
)

-- Insert GLMS Standard Pricing --

Insert Into rtblGlmsStandardPricing 
      ([BalanceFrom], 
	   [BalanceTo], 
	   [FixedTiered], 
	   [PrimeLinked])
Values (0, 999999, 0.00, 0.00),
       (1000000, 9999999, 0.25, 0.25),
	   (10000000, 49999999, 3.73, 4.40),
	   (50000000, 99999999, 3.93, 4.60),
	   (100000000, 249999999, 4.33, 5.00),
	   (250000000, 499999999, 4.83, 5.50),
	   (500000000, 999999999, 5.08, 5.75),
	   (1000000000, Null, 5.33, 6.00)

-- Create Archive Type --

CREATE TABLE [dbo].[rtblArchiveType](
	[pkArchiveTypeId] int IDENTITY(1,1) NOT NULL PRIMARY KEY,
	[Description] varchar(50) NOT NULL
)

-- Insert Archive Type --

Insert Into rtblArchiveType ([Description])
Values ('Standard linked to prime'),
       ('Standard fixed rate')

-- Add Archive Type foreign Key to tblConcessionGlms --

Alter Table tblConcessionGlms
Add fkArchiveTypeId int Null Foreign Key (fkArchiveTypeId) References rtblArchiveType(pkArchiveTypeId)


