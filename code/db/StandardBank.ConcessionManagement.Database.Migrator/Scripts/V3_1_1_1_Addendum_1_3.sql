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
