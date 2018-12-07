CREATE TABLE [dbo].[tblProductBOL] (
    [pkProductBOLId]         INT          IDENTITY (1, 1) NOT NULL,
    [fkRiskGroupId]          INT          NULL,
    [fkLegalEntityId]        INT          NULL,
    [fkLegalEntityBOLUserId] INT          NULL,
    [fkChargeCodeId]         INT          NULL,
    [LoadedRate]             VARCHAR (50) NULL,
    CONSTRAINT [PK_tblProductBOL] PRIMARY KEY CLUSTERED ([pkProductBOLId] ASC),
    CONSTRAINT [FK_tblProductBOL_rtblBOLChargeCode] FOREIGN KEY ([fkChargeCodeId]) REFERENCES [dbo].[rtblBOLChargeCode] ([pkChargeCodeId]),
    CONSTRAINT [FK_tblProductBOL_tblLegalEntity] FOREIGN KEY ([fkLegalEntityId]) REFERENCES [dbo].[tblLegalEntity] ([pkLegalEntityId]),
    CONSTRAINT [FK_tblProductBOL_tblLegalEntityBOLUser] FOREIGN KEY ([fkLegalEntityBOLUserId]) REFERENCES [dbo].[tblLegalEntityBOLUser] ([pkLegalEntityBOLUserId]),
    CONSTRAINT [FK_tblProductBOL_tblRiskGroup] FOREIGN KEY ([fkRiskGroupId]) REFERENCES [dbo].[tblRiskGroup] ([pkRiskGroupId])
);

