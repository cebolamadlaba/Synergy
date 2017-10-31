

GO


GO




GO


GO


GO


GO



GO


GO


GO


GO


GO


GO


GO




GO


GO


GO


GO

SET IDENTITY_INSERT [dbo].[rtblRelationship] ON

MERGE [dbo].[rtblRelationship] AS TARGET
USING (VALUES  (1, 'Extension', 1), (2, 'Renewal', 1)) AS SOURCE ([Id], [Description], [IsActive])
ON TARGET.[pkRelationshipId] = SOURCE.[Id]
WHEN MATCHED THEN UPDATE SET [Description] = SOURCE.[Description], [IsActive] = SOURCE.[IsActive]
WHEN NOT MATCHED THEN INSERT ([pkRelationshipId], [Description], [IsActive]) VALUES (SOURCE.[Id], SOURCE.[Description], SOURCE.[IsActive]);

SET IDENTITY_INSERT [dbo].[rtblRelationship] OFF

