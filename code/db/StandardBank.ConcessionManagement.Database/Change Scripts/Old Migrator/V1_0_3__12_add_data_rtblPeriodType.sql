SET IDENTITY_INSERT [dbo].[rtblPeriodType] ON

MERGE [dbo].[rtblPeriodType] AS TARGET
USING (VALUES  (1, 'Standard', 1), (2, 'Ongoing', 1)) AS SOURCE ([Id], [Description], [IsActive])
ON TARGET.[pkPeriodTypeId] = SOURCE.[Id]
WHEN MATCHED THEN UPDATE SET [Description] = SOURCE.[Description], [IsActive] = SOURCE.[IsActive]
WHEN NOT MATCHED THEN INSERT ([pkPeriodTypeId], [Description], [IsActive]) VALUES (SOURCE.[Id], SOURCE.[Description], SOURCE.[IsActive]);

SET IDENTITY_INSERT [dbo].[rtblPeriodType] OFF