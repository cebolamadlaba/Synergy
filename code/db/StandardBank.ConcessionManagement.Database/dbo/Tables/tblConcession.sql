CREATE TABLE [dbo].[tblConcession] (
    [pkConcessionId]      INT             IDENTITY (1, 1) NOT NULL,
    [fkTypeId]            INT             NOT NULL,
    [fkConcessionTypeId]  INT             NOT NULL,
    [fkStatusId]          INT             NOT NULL,
    [fkSubStatusId]       INT             NOT NULL,
    [fkAAUserId]          INT             NULL,
    [fkRequestorId]       INT             NOT NULL,
    [fkBCMUserId]         INT             NULL,
    [fkPCMUserId]         INT             NULL,
    [fkHOUserId]          INT             NULL,
    [fkRiskGroupId]       INT             NOT NULL,
    [fkRegionId]          INT             NOT NULL,
    [fkCentreId]          INT             NOT NULL,
    [ConcessionRef]       VARCHAR (30)    NULL,
    [SMTDealNumber]       VARCHAR (20)    NULL,
    [ConcessionDate]      DATETIME        NOT NULL,
    [DatesentForApproval] DATETIME        NULL,
    [Motivation]          VARCHAR (1000)  NOT NULL,
    [DateActionedByBCM]   DATETIME        NULL,
    [DateActionedByPCM]   DATETIME        NULL,
    [DateActionedByHO]    DATETIME        NULL,
    [MRS_CRS]             DECIMAL (18, 2) NULL,
    [IsCurrent]           BIT             NOT NULL,
    [IsActive]            BIT             NOT NULL,
    CONSTRAINT [PK_tblConcession] PRIMARY KEY CLUSTERED ([pkConcessionId] ASC),
    CONSTRAINT [FK_tblConcession_rtblConcessionType] FOREIGN KEY ([fkConcessionTypeId]) REFERENCES [dbo].[rtblConcessionType] ([pkConcessionTypeId]),
    CONSTRAINT [FK_tblConcession_rtblRegion] FOREIGN KEY ([fkRegionId]) REFERENCES [dbo].[rtblRegion] ([pkRegionId]),
    CONSTRAINT [FK_tblConcession_rtblStatus] FOREIGN KEY ([fkStatusId]) REFERENCES [dbo].[rtblStatus] ([pkStatusId]),
    CONSTRAINT [FK_tblConcession_rtblSubStatus] FOREIGN KEY ([fkSubStatusId]) REFERENCES [dbo].[rtblSubStatus] ([pkSubStatusId]),
    CONSTRAINT [FK_tblConcession_rtblType] FOREIGN KEY ([fkTypeId]) REFERENCES [dbo].[rtblType] ([pkTypeId]),
    CONSTRAINT [FK_tblConcession_tblCentre] FOREIGN KEY ([fkCentreId]) REFERENCES [dbo].[tblCentre] ([pkCentreId]),
    CONSTRAINT [FK_tblConcession_tblRiskGroup] FOREIGN KEY ([fkRiskGroupId]) REFERENCES [dbo].[tblRiskGroup] ([pkRiskGroupId]),
    CONSTRAINT [FK_tblConcession_tblUser] FOREIGN KEY ([fkRequestorId]) REFERENCES [dbo].[tblUser] ([pkUserId]),
    CONSTRAINT [FK_tblConcession_tblUser_AA] FOREIGN KEY ([fkAAUserId]) REFERENCES [dbo].[tblUser] ([pkUserId]),
    CONSTRAINT [FK_tblConcession_tblUserBCM] FOREIGN KEY ([fkBCMUserId]) REFERENCES [dbo].[tblUser] ([pkUserId]),
    CONSTRAINT [FK_tblConcession_tblUserHO] FOREIGN KEY ([fkHOUserId]) REFERENCES [dbo].[tblUser] ([pkUserId]),
    CONSTRAINT [FK_tblConcession_tblUserPCM] FOREIGN KEY ([fkPCMUserId]) REFERENCES [dbo].[tblUser] ([pkUserId])
);











