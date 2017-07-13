CREATE TABLE [dbo].[tblConcessionRemovalRequest] (
    [pkConcessionRemovalRequestId] INT      IDENTITY (1, 1) NOT NULL,
    [fkConcessionId]               INT      NOT NULL,
    [RequestorId]                  INT      NOT NULL,
    [fkBCMUserId]                  INT      NULL,
    [fkPCMUserId]                  INT      NULL,
    [fkHOUserId]                   INT      NULL,
    [fkSubStatusId]                INT      NULL,
    [SystemDate]                   DATETIME NULL,
    [DateApproved]                 DATETIME NULL,
    CONSTRAINT [PK_tblConcessionRemovalRequest] PRIMARY KEY CLUSTERED ([pkConcessionRemovalRequestId] ASC),
    CONSTRAINT [FK_tblConcessionRemovalRequest_rtblSubStatus] FOREIGN KEY ([fkSubStatusId]) REFERENCES [dbo].[rtblSubStatus] ([pkSubStatusId]),
    CONSTRAINT [FK_tblConcessionRemovalRequest_tblConcession] FOREIGN KEY ([fkConcessionId]) REFERENCES [dbo].[tblConcession] ([pkConcessionId]),
    CONSTRAINT [FK_tblConcessionRemovalRequest_tblUser] FOREIGN KEY ([fkPCMUserId]) REFERENCES [dbo].[tblUser] ([pkUserId]),
    CONSTRAINT [FK_tblConcessionRemovalRequest_tblUserBcm] FOREIGN KEY ([fkBCMUserId]) REFERENCES [dbo].[tblUser] ([pkUserId])
);

