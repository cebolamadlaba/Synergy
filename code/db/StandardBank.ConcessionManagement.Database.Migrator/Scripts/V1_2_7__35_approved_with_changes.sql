SET IDENTITY_INSERT [dbo].[rtblSubStatus] ON

MERGE [dbo].[rtblSubStatus] AS TARGET
USING (VALUES  (12, 'PCM Approved With Changes', 1), (13, 'HO Approved With Changes', 1)) AS SOURCE ([Id], [Description], [IsActive])
ON TARGET.[pkSubStatusId] = SOURCE.[Id]
WHEN MATCHED THEN UPDATE SET [Description] = SOURCE.[Description], [IsActive] = SOURCE.[IsActive]
WHEN NOT MATCHED THEN INSERT ([pkSubStatusId], [Description], [IsActive]) VALUES (SOURCE.[Id], SOURCE.[Description], SOURCE.[IsActive]);

SET IDENTITY_INSERT [dbo].[rtblSubStatus] OFF