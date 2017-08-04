SET IDENTITY_INSERT [dbo].[rtblRegion] ON

MERGE [dbo].[rtblRegion] AS TARGET
USING (VALUES  (1, 'Gauteng', 1), (2, 'KZN', 1)) AS SOURCE ([Id], [Description], [IsActive])
ON TARGET.[pkRegionId] = SOURCE.[Id]
WHEN MATCHED THEN UPDATE SET [Description] = SOURCE.[Description], [IsActive] = SOURCE.[IsActive]
WHEN NOT MATCHED THEN INSERT ([pkRegionId], [Description], [IsActive]) VALUES (SOURCE.[Id], SOURCE.[Description], SOURCE.[IsActive]);

SET IDENTITY_INSERT [dbo].[rtblRegion] OFF