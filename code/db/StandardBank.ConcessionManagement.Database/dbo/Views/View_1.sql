CREATE VIEW [dbo].[View_1]
AS
SELECT        c.pkCentreId AS CentreId, c.CentreName, c.IsActive, bcmtable.BCMId AS BusinessCentreManagerId, bcmtable.BCM AS BusinessCentreManager, r.pkRegionId AS RegionId, r.Description AS Region, 
                         CASE WHEN requestortable.[AECount] IS NULL THEN 0 ELSE requestortable.[AECount] END AS RequestorCount
FROM            dbo.tblCentre AS c LEFT OUTER JOIN
                             (SELECT        u.FirstName + ' ' + u.Surname AS BCM, u.pkUserId AS BCMId, cu.fkCentreId
                               FROM            dbo.tblCentreUser AS cu INNER JOIN
                                                         dbo.tblUser AS u ON u.pkUserId = cu.fkUserId INNER JOIN
                                                         dbo.tblUserRole AS ur ON ur.fkUserId = u.pkUserId INNER JOIN
                                                         dbo.rtblRole AS r ON r.pkRoleId = ur.fkRoleId AND r.RoleName = 'BCM') AS bcmtable ON bcmtable.fkCentreId = c.pkCentreId INNER JOIN
                         dbo.rtblRegion AS r ON r.pkRegionId = c.fkRegionId LEFT OUTER JOIN
                             (SELECT        COUNT(*) AS AECount, cu.fkCentreId
                               FROM            dbo.tblCentreUser AS cu INNER JOIN
                                                         dbo.tblUser AS u ON u.pkUserId = cu.fkUserId INNER JOIN
                                                         dbo.tblUserRole AS ur ON ur.fkUserId = u.pkUserId INNER JOIN
                                                         dbo.rtblRole AS r ON r.pkRoleId = ur.fkRoleId AND r.RoleName = 'Requestor'
                               GROUP BY cu.fkCentreId) AS requestortable ON requestortable.fkCentreId = c.pkCentreId
GO
EXECUTE sp_addextendedproperty @name = N'MS_DiagramPaneCount', @value = 1, @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'View_1';


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
               Right = 208
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "bcmtable"
            Begin Extent = 
               Top = 6
               Left = 246
               Bottom = 119
               Right = 416
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "r"
            Begin Extent = 
               Top = 6
               Left = 454
               Bottom = 119
               Right = 624
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "requestortable"
            Begin Extent = 
               Top = 6
               Left = 662
               Bottom = 102
               Right = 832
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
', @level0type = N'SCHEMA', @level0name = N'dbo', @level1type = N'VIEW', @level1name = N'View_1';

