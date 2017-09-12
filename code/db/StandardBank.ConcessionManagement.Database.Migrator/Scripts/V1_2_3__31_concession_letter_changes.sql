ALTER TABLE [dbo].[tblUser]
ADD [ContactNumber] varchar(50) NULL

GO

ALTER PROCEDURE CreateUser
	@ANumber varchar(50),
	@EmailAddress varchar(50),
	@FirstName varchar(50),
	@LastName varchar(50),
	@RoleId int,
	@RegionId int,
	@CentreId int,
	@ContactNumber varchar(50)
AS
BEGIN
	Declare @userId  int
	SET NOCOUNT ON;

    insert into [dbo].[tblUser]([ANumber],[EmailAddress],[FirstName],[Surname],[IsActive],[ContactNumber])
	values(@ANumber,@EmailAddress,@FirstName,@LastName,1,@ContactNumber)
	set @userId = SCOPE_IDENTITY()

	insert into [dbo].[tblUserRegion] ([fkUserId],[fkRegionId],[IsActive],[IsSelected])
	values(@userId,@RegionId,1,1)

	insert into [dbo].[tblUserRole] ([fkUserId],[fkRoleId],[IsActive])
	values(@userId,@RoleId,1)

	insert into [dbo].[tblCentreUser] ([fkCentreId],[fkUserId],[IsActive])
	values(@CentreId,@userId,1)

END
GO


ALTER PROCEDURE [dbo].[UpdateUser]
	@ANumber varchar(50),
	@EmailAddress varchar(50),
	@FirstName varchar(50),
	@LastName varchar(50),
	@RoleId int,
	@RegionId int,
	@CentreId int,
	@Id int,
	@ContactNumber varchar(50) null
AS
BEGIN
	SET NOCOUNT ON;

    update [dbo].[tblUser]
	SET [ANumber]= @ANumber,
	[EmailAddress]= @EmailAddress,
	[FirstName] = @FirstName,
	[Surname] =@LastName,
	[ContactNumber] = @ContactNumber
	where ANumber = @ANumber
	

	update [dbo].[tblUserRegion] 
	SET [fkRegionId] = @RegionId
	where fkUserId = @Id

	update [dbo].[tblUserRole] 
	set fkRoleId = @RoleId
	where fkUserId = @Id

	update [dbo].[tblCentreUser] 
	set fkCentreId = @CentreId
	where fkUserId = @Id

END

GO


ALTER TABLE [dbo].[tblLegalEntity]
ADD [ContactPerson] varchar(150) null

ALTER TABLE [dbo].[tblLegalEntity]
ADD [PostalAddress] varchar(250) null

ALTER TABLE [dbo].[tblLegalEntity]
ADD [City] varchar(150) null

ALTER TABLE [dbo].[tblLegalEntity]
ADD [PostalCode] varchar(50) null

GO

