
SET IDENTITY_INSERT [dbo].[rtblRelationship] ON

MERGE [dbo].[rtblRelationship] AS TARGET
USING (VALUES  (3, 'Resubmit', 1), (4, 'Update', 1)) AS SOURCE ([Id], [Description], [IsActive])
ON TARGET.[pkRelationshipId] = SOURCE.[Id]
WHEN MATCHED THEN UPDATE SET [Description] = SOURCE.[Description], [IsActive] = SOURCE.[IsActive]
WHEN NOT MATCHED THEN INSERT ([pkRelationshipId], [Description], [IsActive]) VALUES (SOURCE.[Id], SOURCE.[Description], SOURCE.[IsActive]);

SET IDENTITY_INSERT [dbo].[rtblRelationship] OFF