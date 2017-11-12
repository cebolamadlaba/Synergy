﻿CREATE PROCEDURE [dbo].[UpdateUser]
	@ANumber varchar(50),
	@EmailAddress varchar(50),
	@FirstName varchar(50),
	@LastName varchar(50),
	@RoleId int,
	@RegionId int,
	@CentreId int,
	@Id int,
	@ContactNumber varchar(50) 
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