CREATE PROCEDURE [dbo].[CreateUser]
	@ANumber varchar(50),
	@EmailAddress varchar(50),
	@FirstName varchar(50),
	@LastName varchar(50),
	@RoleId int,
	@ContactNumber varchar(50)
AS
BEGIN
	Declare @userId  int
	SET NOCOUNT ON;

    insert into [dbo].[tblUser]([ANumber],[EmailAddress],[FirstName],[Surname],[IsActive],[ContactNumber])
	values(@ANumber,@EmailAddress,@FirstName,@LastName,1,@ContactNumber)
	set @userId = SCOPE_IDENTITY()

	insert into [dbo].[tblUserRole] ([fkUserId],[fkRoleId],[IsActive])
	values(@userId,@RoleId,1)

	select @userId
END