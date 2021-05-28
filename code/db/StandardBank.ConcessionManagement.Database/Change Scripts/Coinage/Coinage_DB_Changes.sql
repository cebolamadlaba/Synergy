USE [ConcessionPricingTool]
GO

--Requirement 1 – Add Branch Deposits - Coins as a Channel Type under the Cash product--

INSERT INTO [dbo].[rtblChannelType]
           ([Description]
           ,[IsActive]
           ,[StandardPricingTable])
     VALUES
           ('Branch Deposits - Coins'
           ,1
           ,289)
GO

INSERT INTO [dbo].[rtblChannelTypeImport]
           ([fkChannelTypeId]
           ,[ImportFileChannel])
     VALUES
           (11
           ,106)
GO

---- Requirement 2 – Amend Branch Only to Branch Deposits - Notes as a Channel Type under the Cash product -----

 UPDATE [ConcessionPricingTool].[dbo].[rtblChannelType]
  SET [Description] = 'Branch Deposits - Notes'
  WHERE [Description] = 'Branch Only'


  ----- Requirement 3 – Add Coin Withdrawal as a Channel Type under the Transactional product -----

  INSERT INTO [dbo].[rtblTransactionType]
           ([fkConcessionTypeId]
           ,[Description]
           ,[IsActive]
           ,[StandardPricingTable])
     VALUES
           (5,
           'Coins Withdrawal'
           ,1
           ,3509)
GO

INSERT INTO [dbo].[rtblTransactionTypeImport]
           ([fkTransactionTypeId]
           ,[ImportFileChannel])
     VALUES
           (21
           ,304)
GO

----- Requirement 4 – Amend Cash Withdrawal to Note Withdrawal as a Channel Type under the Transactional product-----

  UPDATE [ConcessionPricingTool].[dbo].[rtblTransactionType]
  SET [Description] = 'Notes Withdrawal',
      [StandardPricingTable]= '294'
  WHERE [Description] = 'Cash Withdrawal - Branch'


  INSERT INTO [dbo].[rtblTransactionTypeImport]
           ([fkTransactionTypeId]
           ,[ImportFileChannel])
     VALUES
           (20
           ,305)
GO



