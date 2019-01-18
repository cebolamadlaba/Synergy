
CREATE VIEW [dbo].[ConcessionConditionView]
AS
SELECT        cc.pkConcessionConditionId AS ConcessionConditionId, cc.fkConcessionId AS ConcessionId, c.fkRequestorId AS RequestorId, c.ConcessionRef AS ReferenceNumber, rct.pkConcessionTypeId AS ConcessionTypeId, 
                         rct.Description AS ConcessionType, c.fkRiskGroupId AS RiskGroupId, rg.RiskGroupNumber, rg.RiskGroupName, cc.fkConditionTypeId AS ConditionTypeId, ct.Description AS ConditionType, 
                         cc.fkConditionProductId AS ConditionProductId, cp.Description AS ConditionProduct, cc.fkPeriodTypeId AS PeriodTypeId, pt.Description AS PeriodType, cc.fkPeriodId AS PeriodId, p.Description AS Period, cc.InterestRate, 
                         cc.Volume, cc.Value, cc.ConditionMet, cc.DateApproved, cc.ExpiryDate, cc.IsActive, cc.ActualVolume, cc.ActualValue, cc.ActualTurnover
FROM            dbo.tblConcessionCondition AS cc INNER JOIN
                         dbo.tblConcession AS c ON c.pkConcessionId = cc.fkConcessionId INNER JOIN
                         dbo.tblRiskGroup AS rg ON rg.pkRiskGroupId = c.fkRiskGroupId INNER JOIN
                         dbo.rtblConditionType AS ct ON ct.pkConditionTypeId = cc.fkConditionTypeId INNER JOIN
                         dbo.rtblConditionProduct AS cp ON cp.pkConditionProductId = cc.fkConditionProductId INNER JOIN
                         dbo.rtblPeriodType AS pt ON pt.pkPeriodTypeId = cc.fkPeriodTypeId INNER JOIN
                         dbo.rtblPeriod AS p ON p.pkPeriodId = cc.fkPeriodId INNER JOIN
                         dbo.rtblConcessionType AS rct ON rct.pkConcessionTypeId = c.fkConcessionTypeId
WHERE        (c.IsActive = 1) AND (c.fkStatusId IN (2, 3))
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 2, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ConcessionConditionView';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane2', @value = N'
         Begin Table = "rct"
            Begin Extent = 
               Top = 138
               Left = 456
               Bottom = 268
               Right = 655
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ConcessionConditionView';


GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPane1', @value = N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[39] 4[22] 2[20] 3) )"
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
         Begin Table = "cc"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 264
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "c"
            Begin Extent = 
               Top = 6
               Left = 302
               Bottom = 136
               Right = 502
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "rg"
            Begin Extent = 
               Top = 6
               Left = 540
               Bottom = 136
               Right = 727
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ct"
            Begin Extent = 
               Top = 6
               Left = 765
               Bottom = 119
               Right = 956
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "cp"
            Begin Extent = 
               Top = 120
               Left = 765
               Bottom = 233
               Right = 972
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "pt"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 251
               Right = 210
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "p"
            Begin Extent = 
               Top = 138
               Left = 248
               Bottom = 251
               Right = 418
            End
            DisplayFlags = 280
            TopColumn = 0
         End', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'ConcessionConditionView';



