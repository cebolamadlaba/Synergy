DROP TABLE [dbo].[tblUserRegion]

GO

DROP TABLE [Audit].[tblUserRegion]

GO

ALTER PROCEDURE [dbo].[CreateUser]
	@ANumber varchar(50),
	@EmailAddress varchar(50),
	@FirstName varchar(50),
	@LastName varchar(50),
	@RoleId int,
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

	insert into [dbo].[tblUserRole] ([fkUserId],[fkRoleId],[IsActive])
	values(@userId,@RoleId,1)

	select @userId
END

GO

ALTER PROCEDURE [dbo].[UpdateUser]
	@ANumber varchar(50),
	@EmailAddress varchar(50),
	@FirstName varchar(50),
	@LastName varchar(50),
	@RoleId int,
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
	set fkRoleId = @RoleId
	where fkUserId = @Id

END

GO

ALTER TABLE [dbo].[tblUser]
ADD [CanApprove] bit NULL

GO

UPDATE [dbo].[tblUser]
SET [CanApprove] = 1

GO

ALTER TABLE [dbo].[tblUser] ADD  CONSTRAINT [DF_tblUser_CanApprove]  DEFAULT ((1)) FOR [CanApprove]

GO


ALTER TABLE [dbo].[tblUser]
ALTER COLUMN [CanApprove] bit NOT NULL

GO

