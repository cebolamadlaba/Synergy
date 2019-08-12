


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