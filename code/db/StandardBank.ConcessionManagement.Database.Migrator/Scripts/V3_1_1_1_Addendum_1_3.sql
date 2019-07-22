CREATE TABLE rtblSubRole (
    SubRoleId int IDENTITY(1,1) NOT NULL  PRIMARY KEY,
    Name varchar(255) NOT NULL,
    Active bit default 1
);

INSERT INTO rtblSubRole(name)
VALUES ('BOLConsultant')
        ,('TradeBanker');

ALTER TABLE tblUserRole
ADD  fkSubRoleId int  FOREIGN KEY (fkSubRoleId) REFERENCES rtblSubRole(SubRoleId) 

--update user store prod

DROP PROCEDURE [dbo].[UpdateUser]
GO



CREATE PROCEDURE [dbo].[UpdateUser]
	@ANumber varchar(50),
	@EmailAddress varchar(50),
	@FirstName varchar(50),
	@LastName varchar(50),
	@RoleId int,
	@SubRoleId int,
	@Id int,
	@ContactNumber varchar(50),
	@IsActive bit,
	@CanApprove bit
AS
BEGIN
	SET NOCOUNT ON;

    update [dbo].[tblUser]
	SET [ANumber]= @ANumber,
	[EmailAddress]= @EmailAddress,
	[FirstName] = @FirstName,
	[Surname] =@LastName,
	[ContactNumber] = @ContactNumber,
	[IsActive] = @IsActive,
	[CanApprove] = @CanApprove
	where ANumber = @ANumber

	update [dbo].[tblUserRole] 
	set fkRoleId = @RoleId,
	     fkSubRoleId=@SubRoleId
	where fkUserId = @Id

END


GO


--insert user store prod

DROP PROCEDURE [dbo].[CreateUser]
GO

CREATE PROCEDURE [dbo].[CreateUser]
	@ANumber varchar(50),
	@EmailAddress varchar(50),
	@FirstName varchar(50),
	@LastName varchar(50),
	@RoleId int,
	@SubRoleId int,
	@ContactNumber varchar(50),
	@IsActive bit,
	@CanApprove bit
AS
BEGIN
	Declare @userId  int
	SET NOCOUNT ON;

    insert into [dbo].[tblUser]([ANumber],[EmailAddress],[FirstName],[Surname],[IsActive],[ContactNumber],[CanApprove])
	values(@ANumber,@EmailAddress,@FirstName,@LastName,@IsActive,@ContactNumber,@CanApprove)
	set @userId = SCOPE_IDENTITY()

	insert into [dbo].[tblUserRole] ([fkUserId],[fkRoleId],[fkSubRoleId],[IsActive])
	values(@userId,@RoleId,@SubRoleId,1)

	select @userId
END


GO

-----
ALTER TABLE rtblBOLChargeCode
ADD StandardPricingOption1 Decimal(7,3),
     StandardPricingOption2 Decimal(7,3),
     StandardPricingOption3 Decimal(7,3)

-----
ALTER TABLE rtblTradeProduct
ADD StandardPricingOption1 Decimal(7,3)


--BOL code script-----

Declare @Description varchar(250),@ChargeCode varchar(30),@Length int,@fkChargeCodeTypeId int,@StandardPricingOption1 decimal,
        @StandardPricingOption2 decimal,@StandardPricingOption3 decimal,@IsActive bit;

--SET @Description='Account Management fixed monthly fee - ACM1';
--SET @ChargeCode='ACM1';
--SET @Length=5;
--SET @fkChargeCodeTypeId=1;
--SET @StandardPricingOption1=335.00;
--SET @StandardPricingOption2=415.00;
--SET @StandardPricingOption3=660.00;
--SET @IsActive=1;

--SET @Description='Account Management per transaction fee (balances and statements) - ACM3';
--SET @ChargeCode='ACM3';
--SET @Length=5;
--SET @fkChargeCodeTypeId=1;
--SET @StandardPricingOption1=4.10;
--SET @StandardPricingOption2=3.250;
--SET @StandardPricingOption3=2.800;
--SET @IsActive=1;

--SET @Description='Account verification service (batch) - AVS0';
--SET @ChargeCode='AVS0';
--SET @Length=5;
--SET @fkChargeCodeTypeId=1;
--SET @StandardPricingOption1=3.750;
--SET @StandardPricingOption2=3.750;
--SET @StandardPricingOption3=3.750;
--SET @IsActive=1;

--SET @Description='Real Time Account Verification via BOL on-us - AVR1';
--SET @ChargeCode='AVR1';
--SET @Length=5;
--SET @fkChargeCodeTypeId=1;
--SET @StandardPricingOption1=3.750;
--SET @StandardPricingOption2=3.750;
--SET @StandardPricingOption3=3.750;
--SET @IsActive=1;

MERGE rtblBOLChargeCode WITH (HOLDLOCK) AS target

USING (VALUES (@Description, @ChargeCode, @Length,@fkChargeCodeTypeId,@IsActive,@StandardPricingOption1,@StandardPricingOption2,@StandardPricingOption3))
    AS source (Description, ChargeCode, Length,fkChargeCodeTypeId,IsActive,StandardPricingOption1,StandardPricingOption2,StandardPricingOption3)
    ON target.Description = source.Description and target.ChargeCode = source.ChargeCode and target.fkChargeCodeTypeId = source.fkChargeCodeTypeId

WHEN MATCHED THEN
  UPDATE
   
  SET target.Description = source.Description 
	  ,target.ChargeCode = source.ChargeCode 
	  ,target.fkChargeCodeTypeId = source.fkChargeCodeTypeId
	  ,target.StandardPricingOption1 = source.StandardPricingOption1
      ,target.StandardPricingOption2 = source.StandardPricingOption2
      ,target.StandardPricingOption3 = source.StandardPricingOption3

WHEN NOT MATCHED THEN

	INSERT(Description
		   ,ChargeCode
		   ,Length
		   ,fkChargeCodeTypeId
		   ,IsActive								
		   ,StandardPricingOption1
		   ,StandardPricingOption2
		   ,StandardPricingOption3)
	Values(Description
	       ,ChargeCode
		   ,Length
		   ,fkChargeCodeTypeId
		   ,IsActive
		   ,StandardPricingOption1
		   ,StandardPricingOption2
		   ,StandardPricingOption3); 

Go

ALTER TABLE rtblBOLChargeCode
ADD IsNonUniversal Bit Not Null Default(0)

CREATE TABLE tblRiskGroupNonUniversalChargeCode
(
Id int IDENTITY(1,1) NOT NULL  PRIMARY KEY,
RiskGroupId INT NOT NULL,
ChargeCodeId INT,
IsActive Bit Not Null Default(1)
)

--put user back to normal AA role--
INSERT INTO [dbo].[rtblSubRole]
          ([name]
          ,[Active])
    VALUES
          ('No-Subrole'
          ,1)
GO




/****** Object:  View [dbo].[ConcessionInboxView]    Script Date: 19/07/2019 08:34:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*  
	2019-07-19 - Update: Add LEFT JOIN tblAENumberUser and LEFT JOIN tblCentreUser to get Concession AE Business Center
*/


    ALTER VIEW [dbo].[ConcessionInboxView]
    AS
    SELECT        Distinct c.pkConcessionId AS ConcessionId, rg.pkRiskGroupId AS RiskGroupId, rg.RiskGroupNumber, rg.RiskGroupName, le.pkLegalEntityId AS LegalEntityId, le.CustomerName, lea.pkLegalEntityAccountId AS LegalEntityAccountId, 
                             lea.AccountNumber, ct.pkConcessionTypeId AS ConcessionTypeId, ct.Description AS ConcessionType, c.ConcessionDate, s.pkStatusId AS StatusId, s.Description AS Status, ss.pkSubStatusId AS SubStatusId, 
                             ss.Description AS SubStatus, c.ConcessionRef, ms.pkMarketSegmentId AS MarketSegmentId, ms.Description AS Segment, c.DatesentForApproval, cd.pkConcessionDetailId AS ConcessionDetailId, cd.ExpiryDate, 
                             cd.DateApproved, c.fkAAUserId AS AAUserId, c.fkRequestorId AS RequestorId, c.fkBCMUserId AS BCMUserId, c.fkPCMUserId AS PCMUserId, c.fkHOUserId AS HOUserId, ce.pkCentreId AS CentreId, ce.CentreName, 
                             r.pkRegionId AS RegionId, r.Description AS Region, cd.IsMismatched, c.IsActive, c.IsCurrent, cd.PriceExported, cd.PriceExportedDate, c.Archived,
							--anu.pkAENumberUserId, aea.fkAccountExecutiveUserId AS CurrentAEUserId
							anu.fkUserId As CurrentAEUserId, aea.fkAccountAssistantUserId CurrentAAUserId
    FROM        dbo.tblConcession AS c 
    INNER JOIN  dbo.tblRiskGroup AS rg ON rg.pkRiskGroupId = c.fkRiskGroupId 
    INNER JOIN  dbo.rtblConcessionType AS ct ON ct.pkConcessionTypeId = c.fkConcessionTypeId 
    INNER JOIN  dbo.tblConcessionDetail AS cd ON cd.fkConcessionId = c.pkConcessionId 
    Inner JOIN  dbo.tblLegalEntity AS le ON le.pkLegalEntityId = cd.fkLegalEntityId 
    Inner JOIN  dbo.tblLegalEntityAccount AS lea ON lea.pkLegalEntityAccountId = cd.fkLegalEntityAccountId 
    INNER JOIN  dbo.rtblStatus AS s ON s.pkStatusId = c.fkStatusId 
    INNER JOIN  dbo.rtblSubStatus AS ss ON ss.pkSubStatusId = c.fkSubStatusId 
    INNER JOIN  dbo.rtblMarketSegment AS ms ON ms.pkMarketSegmentId = rg.fkMarketSegmentId 	
    LEFT JOIN   tblAENumberUser aenu On	aenu.pkAENumberUserId = c.fkAENumberUserId
    LEFT JOIN   tblCentreUser cu On	cu.fkUserId = aenu.fkUserId
    LEFT JOIN   dbo.tblCentre AS ce ON ce.pkCentreId = cu.fkCentreId 
    LEFT JOIN   dbo.rtblRegion AS r ON r.pkRegionId = ce.fkRegionId 
    LEFT JOIN   dbo.tblAENumberUser AS anu ON anu.pkAENumberUserId = c.fkAENumberUserId 
    LEFT JOIN   dbo.tblAccountExecutiveAssistant AS aea ON aea.fkAccountExecutiveUserId = anu.fkUserId
GO


