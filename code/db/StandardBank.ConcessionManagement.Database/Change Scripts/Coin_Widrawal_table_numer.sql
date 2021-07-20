USE [ConcessionPricingTool]
GO
---Add table number on coin widrawal-

INSERT  INTO 
     [dbo].[rtblTransactionTableNumber]
	 ([fkTransactionTypeId]
      ,[TariffTable]
       ,[Fee]
       ,[AdValorem]
       ,[IsActive]
       ,[ActiveUntil]
       ,[IsRingfenced])

SELECT 
       '21' as [fkTransactionTypeId]
      ,[TariffTable]
      ,[Fee]
      ,[AdValorem]
      ,[IsActive]
      ,[ActiveUntil]
      ,[IsRingfenced]
FROM 
    [dbo].[rtblTransactionTableNumber]

WHERE
    [fkTransactionTypeId] = 20

