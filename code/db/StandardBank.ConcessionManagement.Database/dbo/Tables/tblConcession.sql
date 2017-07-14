﻿CREATE TABLE [dbo].[tblConcession] (
    [pkConcessionId]      INT            IDENTITY (1, 1) NOT NULL,
    [fkTypeId]            INT            NOT NULL,
    [ConcessionRef]       VARCHAR (30)   NULL,
    [fkLegalEntityId]     INT            NOT NULL,
    [fkConcessionTypeId]  INT            NOT NULL,
    [SMTDealNumber]       VARCHAR (20)   NOT NULL,
    [fkStatusId]          INT            NOT NULL,
    [fkSubStatusId]       INT            NULL,
    [ConcessionDate]      DATETIME       NOT NULL,
    [DatesentForApproval] DATETIME       NULL,
    [Motivation]          VARCHAR (1000) NOT NULL,
    [DateApproved]        DATETIME       NULL,
    [fkRequestorId]       INT            NOT NULL,
    [fkBCMUserId]         INT            NULL,
    [DateActionedByBCM]   DATETIME       NULL,
    [fkPCMUserId]         INT            NULL,
    [DateActionedByPCM]   DATETIME       NULL,
    [fkHOUserId]          INT            NULL,
    [DateActionedByHO]    DATETIME       NULL,
    [ExpiryDate]          DATETIME       NULL,
    [CentreId]            INT            NULL,
    [IsCurrent]           BIT            NOT NULL,
    [IsActive]            BIT            NOT NULL,
    CONSTRAINT [PK_tblConcession] PRIMARY KEY CLUSTERED ([pkConcessionId] ASC),
    CONSTRAINT [FK_tblConcession_rtblConcessionType] FOREIGN KEY ([fkConcessionTypeId]) REFERENCES [dbo].[rtblConcessionType] ([pkConcessionTypeId]),
    CONSTRAINT [FK_tblConcession_rtblStatus] FOREIGN KEY ([fkStatusId]) REFERENCES [dbo].[rtblStatus] ([pkStatusId]),
    CONSTRAINT [FK_tblConcession_rtblSubStatus] FOREIGN KEY ([fkSubStatusId]) REFERENCES [dbo].[rtblSubStatus] ([pkSubStatusId]),
    CONSTRAINT [FK_tblConcession_rtblType] FOREIGN KEY ([fkTypeId]) REFERENCES [dbo].[rtblType] ([pkTypeId]),
    CONSTRAINT [FK_tblConcession_tblCentre] FOREIGN KEY ([CentreId]) REFERENCES [dbo].[tblCentre] ([pkCentreId]),
    CONSTRAINT [FK_tblConcession_tblLegalEntity] FOREIGN KEY ([fkLegalEntityId]) REFERENCES [dbo].[tblLegalEntity] ([pkLegalEntityId]),
    CONSTRAINT [FK_tblConcession_tblUser] FOREIGN KEY ([fkRequestorId]) REFERENCES [dbo].[tblUser] ([pkUserId]),
    CONSTRAINT [FK_tblConcession_tblUserBCM] FOREIGN KEY ([fkBCMUserId]) REFERENCES [dbo].[tblUser] ([pkUserId]),
    CONSTRAINT [FK_tblConcession_tblUserHO] FOREIGN KEY ([fkHOUserId]) REFERENCES [dbo].[tblUser] ([pkUserId]),
    CONSTRAINT [FK_tblConcession_tblUserPCM] FOREIGN KEY ([fkPCMUserId]) REFERENCES [dbo].[tblUser] ([pkUserId])
);
