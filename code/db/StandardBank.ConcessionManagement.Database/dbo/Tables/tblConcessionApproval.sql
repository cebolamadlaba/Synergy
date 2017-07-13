CREATE TABLE [dbo].[tblConcessionApproval] (
    [pkConcessionApprovalId] INT      IDENTITY (1, 1) NOT NULL,
    [fkConcessionId]         INT      NOT NULL,
    [fkOldSubStatusId]       INT      NULL,
    [fkNewSubStatusId]       INT      NOT NULL,
    [fkUserId]               INT      NOT NULL,
    [SystemDate]             DATETIME NOT NULL,
    [IsActive]               BIT      NOT NULL,
    CONSTRAINT [PK_tblConcessionApproval] PRIMARY KEY CLUSTERED ([pkConcessionApprovalId] ASC),
    CONSTRAINT [FK_tblConcessionApproval_rtblSubStatusNew] FOREIGN KEY ([fkNewSubStatusId]) REFERENCES [dbo].[rtblSubStatus] ([pkSubStatusId]),
    CONSTRAINT [FK_tblConcessionApproval_rtblSubStatusOld] FOREIGN KEY ([fkOldSubStatusId]) REFERENCES [dbo].[rtblSubStatus] ([pkSubStatusId]),
    CONSTRAINT [FK_tblConcessionApproval_tblConcession] FOREIGN KEY ([fkConcessionId]) REFERENCES [dbo].[tblConcession] ([pkConcessionId]),
    CONSTRAINT [FK_tblConcessionApproval_tblUser] FOREIGN KEY ([fkUserId]) REFERENCES [dbo].[tblUser] ([pkUserId])
);

