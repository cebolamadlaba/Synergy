MERGE [dbo].[tblUser] AS TARGET
USING (VALUES  
('A215840', 'Mpho.Nxiwa@standardbank.co.za', 'Mpho', 'Nxiwa', 1), 
('A150458', 'AnthonyEdgar.Ogle@standardbank.co.za', 'Anthony', 'Ogle', 1), 
('A209089', 'Thomas.Nowe@standardbank.co.za', 'Thomas', 'Nowe', 1)) 
AS SOURCE ([ANumber], [EmailAddress], [FirstName], [Surname], [IsActive])
ON TARGET.[ANumber] = SOURCE.[ANumber]
WHEN NOT MATCHED THEN 
INSERT ([ANumber], [EmailAddress], [FirstName], [Surname], [IsActive]) 
VALUES (SOURCE.[ANumber], SOURCE.[EmailAddress], SOURCE.[FirstName], SOURCE.[Surname], SOURCE.[IsActive]);

DECLARE
	@A215840 int,
	@A150458 int,
	@A209089 int,
	@Requestor int,
	@BCM int,
	@PCM int

SELECT @A215840 = [pkUserId] FROM [dbo].[tblUser] WHERE [ANumber] = 'A215840'
SELECT @A150458 = [pkUserId] FROM [dbo].[tblUser] WHERE [ANumber] = 'A150458'
SELECT @A209089 = [pkUserId] FROM [dbo].[tblUser] WHERE [ANumber] = 'A209089'

SELECT @Requestor = [pkRoleId] FROM [dbo].[rtblRole] WHERE [RoleName] = 'Requestor'
SELECT @BCM = [pkRoleId] FROM [dbo].[rtblRole] WHERE [RoleName] = 'BCM'
SELECT @PCM = [pkRoleId] FROM [dbo].[rtblRole] WHERE [RoleName] = 'PCM'

MERGE [dbo].[tblUserRole] AS TARGET
USING (VALUES  
(@A215840, @Requestor, 1),
(@A150458, @BCM, 1),
(@A209089, @PCM, 1))
AS SOURCE ([UserId], [RoleId], [IsActive])
ON TARGET.[fkUserId] = SOURCE.[UserId]
WHEN MATCHED THEN
UPDATE SET [fkRoleId] = SOURCE.[RoleId], [IsActive] = SOURCE.[IsActive]
WHEN NOT MATCHED THEN
INSERT ([fkUserId], [fkRoleId], [IsActive]) VALUES (SOURCE.[UserId], SOURCE.[RoleId], SOURCE.[IsActive]);

MERGE [dbo].[tblUserRegion] AS TARGET
USING (SELECT [pkUserId], 1, 1, 1 FROM [dbo].[tblUser])
AS SOURCE ([UserId], [RegionId], [IsActive], [IsSelected])
ON TARGET.[fkUserId] = SOURCE.[UserId]
WHEN MATCHED THEN
UPDATE SET [fkRegionId] = SOURCE.[RegionId], [IsActive] = SOURCE.[IsActive], [IsSelected] = SOURCE.[IsSelected]
WHEN NOT MATCHED THEN
INSERT ([fkUserId], [fkRegionId], [IsActive], [IsSelected]) VALUES (SOURCE.[UserId], SOURCE.[RegionId], SOURCE.[IsActive], SOURCE.[IsSelected]);

MERGE [dbo].[tblCentreUser] AS TARGET
USING (SELECT 1, [pkUserId], 1 FROM [dbo].[tblUser])
AS SOURCE ([CentreId], [UserId], [IsActive])
ON TARGET.[fkUserId] = SOURCE.[UserId]
WHEN MATCHED THEN
UPDATE SET [fkCentreId] = SOURCE.[CentreId], [IsActive] = SOURCE.[IsActive]
WHEN NOT MATCHED THEN
INSERT ([fkCentreId], [fkUserId], [IsActive]) VALUES (SOURCE.[CentreId], SOURCE.[UserId], SOURCE.[IsActive]);

