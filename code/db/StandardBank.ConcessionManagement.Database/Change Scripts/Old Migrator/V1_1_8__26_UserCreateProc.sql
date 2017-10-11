SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE CreateUser
	@ANumber varchar(50),
	@EmailAddress varchar(50),
	@FirstName varchar(50),
	@LastName varchar(50),
	@RoleId int,
	@RegionId int,
	@CentreId int
AS
BEGIN
	Declare @userId  int
	SET NOCOUNT ON;

    insert into [dbo].[tblUser]([ANumber],[EmailAddress],[FirstName],[Surname],[IsActive])
	values(@ANumber,@EmailAddress,@FirstName,@LastName,1)
	set @userId = SCOPE_IDENTITY()

	insert into [dbo].[tblUserRegion] ([fkUserId],[fkRegionId],[IsActive],[IsSelected])
	values(@userId,@RegionId,1,1)

	insert into [dbo].[tblUserRole] ([fkUserId],[fkRoleId],[IsActive])
	values(@userId,@RoleId,1)

	insert into [dbo].[tblCentreUser] ([fkCentreId],[fkUserId],[IsActive])
	values(@CentreId,@userId,1)

END
GO
