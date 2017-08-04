MERGE [dbo].[tblUserRegion] AS TARGET
USING (SELECT [pkUserId], 1 FROM [dbo].[tblUser]) AS SOURCE ([UserId], [RegionId])
ON TARGET.[fkUserId] = SOURCE.[UserId] AND TARGET.[fkRegionId] = SOURCE.[RegionId]
WHEN NOT MATCHED THEN INSERT ([fkUserId], [fkRegionId]) VALUES ([UserId], [RegionId]);