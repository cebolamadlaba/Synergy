
GO


GO




GO


GO


GO


GO


SET IDENTITY_INSERT [dbo].[rtblAccrualType] ON

MERGE [dbo].[rtblAccrualType] AS TARGET
USING (VALUES  (1, 'Immediately', 1), (2, 'Daily', 1), (3, 'Monthly', 1)) AS SOURCE ([Id], [Description], [IsActive])
ON TARGET.[pkAccrualTypeId] = SOURCE.[Id]
WHEN MATCHED THEN UPDATE SET [Description] = SOURCE.[Description], [IsActive] = SOURCE.[IsActive]
WHEN NOT MATCHED THEN INSERT ([pkAccrualTypeId], [Description], [IsActive]) VALUES (SOURCE.[Id], SOURCE.[Description], SOURCE.[IsActive]);

SET IDENTITY_INSERT [dbo].[rtblAccrualType] OFF

