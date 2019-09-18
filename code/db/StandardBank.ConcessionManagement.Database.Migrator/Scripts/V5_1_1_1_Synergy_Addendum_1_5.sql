
Create Table tblMarketSegmentEnablementTeamUser
(
	pkMarketSegmentEnablementTeamId Int Identity(1,1) Not Null Primary Key,
	fkMarketSegmentId Int Not Null Foreign Key (fkMarketSegmentId) References rtblMarketSegment(pkMarketSegmentId),
	fkUserId Int Not Null Foreign Key (fkUserId) References tblUser(pkUserId),
	IsActive Bit Not Null Default(1)
)
Go
Alter Table [tblMarketSegmentEnablementTeamUser]
Add Constraint UC_MarketSegmentEnablementTeamUser Unique ([fkMarketSegmentId],[fkUserId])
Go

Declare @userId Int = Null,
		@marketSegmentId Int = Null

Select Top 1 @marketSegmentId = pkMarketSegmentId From rtblMarketSegment Where [Description] = 'Business'
Select Top 1 @userId = pkUserId From tblUser Where	ANumber = 'A141147'
INSERT INTO [dbo].[tblMarketSegmentEnablementTeamUser]([fkMarketSegmentId],[fkUserId],[IsActive])
VALUES(@marketSegmentId,@userId,1)
Select Top 1 @userId = pkUserId From tblUser Where	ANumber = 'A191601'
INSERT INTO [dbo].[tblMarketSegmentEnablementTeamUser]([fkMarketSegmentId],[fkUserId],[IsActive])
VALUES(@marketSegmentId,@userId,1)

Select Top 1 @marketSegmentId = pkMarketSegmentId From rtblMarketSegment Where [Description] = 'Commercial'
Select Top 1 @userId = pkUserId From tblUser Where	ANumber = 'A197282'
INSERT INTO [dbo].[tblMarketSegmentEnablementTeamUser]([fkMarketSegmentId],[fkUserId],[IsActive])
VALUES(@marketSegmentId,@userId,1)

Select Top 1 @marketSegmentId = pkMarketSegmentId From rtblMarketSegment Where [Description] = 'Small Enterprise'
Select Top 1 @userId = pkUserId From tblUser Where	ANumber = 'A131135'
INSERT INTO [dbo].[tblMarketSegmentEnablementTeamUser]([fkMarketSegmentId],[fkUserId],[IsActive])
VALUES(@marketSegmentId,@userId,1)
Select Top 1 @userId = pkUserId From tblUser Where	ANumber = 'A083300'
INSERT INTO [dbo].[tblMarketSegmentEnablementTeamUser]([fkMarketSegmentId],[fkUserId],[IsActive])
VALUES(@marketSegmentId,@userId,1)

Select * From [tblMarketSegmentEnablementTeamUser]
Go