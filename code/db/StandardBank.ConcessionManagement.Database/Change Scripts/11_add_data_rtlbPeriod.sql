SET IDENTITY_INSERT [dbo].[rtblPeriod] ON

MERGE [dbo].[rtblPeriod] AS TARGET
USING (VALUES  (1, '3 Months', 1), (2, '6 Months', 1), (3, '9 Months', 1), (4, '12 Months', 1)) AS SOURCE ([Id], [Description], [IsActive])
ON TARGET.[pkPeriodId] = SOURCE.[Id]
WHEN MATCHED THEN UPDATE SET [Description] = SOURCE.[Description], [IsActive] = SOURCE.[IsActive]
WHEN NOT MATCHED THEN INSERT ([pkPeriodId], [Description], [IsActive]) VALUES (SOURCE.[Id], SOURCE.[Description], SOURCE.[IsActive]);

SET IDENTITY_INSERT [dbo].[rtblPeriod] OFF