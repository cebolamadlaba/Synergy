


/*  
	2019-07-19 - Update: Add LEFT JOIN tblAENumberUser and LEFT JOIN tblCentreUser to get Concession AE Business Center
	2020-01-16 - UPdate: tblLegalEntity/tblLegalEntityAccount changes "Inner Join" to "Left Join";
*/


    CREATE VIEW [dbo].[ConcessionInboxView]
    AS
		SELECT		Distinct 
					c.pkConcessionId AS ConcessionId, rg.pkRiskGroupId AS RiskGroupId, rg.RiskGroupNumber, rg.RiskGroupName, le.pkLegalEntityId AS LegalEntityId, le.CustomerName, le.CustomerNumber, lea.pkLegalEntityAccountId AS LegalEntityAccountId, 
					lea.AccountNumber, ct.pkConcessionTypeId AS ConcessionTypeId, ct.Description AS ConcessionType, c.ConcessionDate, s.pkStatusId AS StatusId, s.Description AS Status, ss.pkSubStatusId AS SubStatusId, 
					ss.Description AS SubStatus, c.ConcessionRef, ms.pkMarketSegmentId AS MarketSegmentId, ms.Description AS Segment, c.DatesentForApproval, cd.pkConcessionDetailId AS ConcessionDetailId, cd.ExpiryDate, 
					cd.DateApproved, c.fkAAUserId AS AAUserId, c.fkRequestorId AS RequestorId, c.fkBCMUserId AS BCMUserId, c.fkPCMUserId AS PCMUserId, c.fkHOUserId AS HOUserId, ce.pkCentreId AS CentreId, ce.CentreName, 
					r.pkRegionId AS RegionId, r.Description AS Region, cd.IsMismatched, c.IsActive, c.IsCurrent, cd.PriceExported, cd.PriceExportedDate, c.Archived,
					--anu.pkAENumberUserId, aea.fkAccountExecutiveUserId AS CurrentAEUserId
					anu.fkUserId As CurrentAEUserId, aea.fkAccountAssistantUserId CurrentAAUserId
		FROM        dbo.tblConcession AS c 
		LEFT JOIN	dbo.tblRiskGroup AS rg ON rg.pkRiskGroupId = c.fkRiskGroupId 
		INNER JOIN	dbo.rtblConcessionType AS ct ON ct.pkConcessionTypeId = c.fkConcessionTypeId 
		INNER JOIN	dbo.tblConcessionDetail AS cd ON cd.fkConcessionId = c.pkConcessionId 
		Left JOIN	dbo.tblLegalEntity AS le ON le.pkLegalEntityId = cd.fkLegalEntityId Or le.pkLegalEntityId = c.fkLegalEntityId 
		Left JOIN	dbo.tblLegalEntityAccount AS lea ON lea.pkLegalEntityAccountId = cd.fkLegalEntityAccountId 
		INNER JOIN	dbo.rtblStatus AS s ON s.pkStatusId = c.fkStatusId
		INNER JOIN	dbo.rtblSubStatus AS ss ON ss.pkSubStatusId = c.fkSubStatusId 
		LEFT JOIN	dbo.rtblMarketSegment AS ms ON ms.pkMarketSegmentId = rg.fkMarketSegmentId 
		LEFT JOIN	tblAENumberUser aenu On	aenu.pkAENumberUserId = c.fkAENumberUserId
		LEFT JOIN	tblCentreUser cu On	cu.fkUserId = aenu.fkUserId
		LEFT JOIN	dbo.tblCentre AS ce ON ce.pkCentreId = cu.fkCentreId 
		LEFT JOIN	dbo.rtblRegion AS r ON r.pkRegionId = ce.fkRegionId  
		LEFT JOIN	dbo.tblAENumberUser AS anu ON anu.pkAENumberUserId = c.fkAENumberUserId 
		LEFT JOIN	dbo.tblAccountExecutiveAssistant AS aea ON aea.fkAccountExecutiveUserId = anu.fkUserId
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ConcessionInboxView';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'd
         Begin Table = "ss"
            Begin Extent = 
               Top = 138
               Left = 477
               Bottom = 251
               Right = 647
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ms"
            Begin Extent = 
               Top = 138
               Left = 685
               Bottom = 251
               Right = 881
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ce"
            Begin Extent = 
               Top = 252
               Left = 269
               Bottom = 382
               Right = 439
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "r"
            Begin Extent = 
               Top = 384
               Left = 291
               Bottom = 497
               Right = 461
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ConcessionInboxView';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "c"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 238
            End
            DisplayFlags = 280
            TopColumn = 21
         End
         Begin Table = "rg"
            Begin Extent = 
               Top = 6
               Left = 276
               Bottom = 136
               Right = 463
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ct"
            Begin Extent = 
               Top = 6
               Left = 501
               Bottom = 136
               Right = 700
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cd"
            Begin Extent = 
               Top = 6
               Left = 738
               Bottom = 136
               Right = 950
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "le"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 268
               Right = 231
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "lea"
            Begin Extent = 
               Top = 270
               Left = 38
               Bottom = 400
               Right = 253
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "s"
            Begin Extent = 
               Top = 138
               Left = 269
               Bottom = 251
               Right = 439
            End
            DisplayFlags = 280
            TopColumn = 0
         En', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ConcessionInboxView';

