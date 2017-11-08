DELETE FROM [dbo].[tblUserRole]
WHERE [fkRoleId] IN (
SELECT [pkRoleId] FROM [dbo].[rtblRole]
WHERE [RoleName] in ('Suite Head ', 'Suite Head'))

DELETE FROM [dbo].[rtblRole]
WHERE [RoleName] = 'Suite Head '

DELETE FROM [dbo].[rtblRole]
WHERE [RoleName] = 'Suite Head'

GO