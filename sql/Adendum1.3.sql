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


