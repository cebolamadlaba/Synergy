
if exists (select 1 from sys.procedures where name ='UpdateUser')
DROP PROCEDURE [dbo].[UpdateUser]
GO

/****** Object:  StoredProcedure [dbo].[UpdateUser]    Script Date: 2017/09/08 10:23:18 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateUser]
	@ANumber varchar(50),
	@EmailAddress varchar(50),
	@FirstName varchar(50),
	@LastName varchar(50),
	@RoleId int,
	@RegionId int,
	@CentreId int,
	@Id int
AS
BEGIN
	SET NOCOUNT ON;

    update [dbo].[tblUser]
	SET [ANumber]= @ANumber,
	[EmailAddress]= @EmailAddress,
	[FirstName] = @FirstName,
	[Surname] =@LastName
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


