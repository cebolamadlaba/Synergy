CREATE TABLE [dbo].[tblConcessionRelationship] (
    [pkConcessionRelationshipId] INT      IDENTITY (1, 1) NOT NULL,
    [fkParentConcessionId]       INT      NOT NULL,
    [fkChildConcessionId]        INT      NOT NULL,
    [fkRelationshipId]           INT      NOT NULL,
    [fkUserId]                   INT      NOT NULL,
    [CreationDate]               DATETIME CONSTRAINT [DF_tblConcessionRelationship_CreationDate] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_tblConcessionRelationship] PRIMARY KEY CLUSTERED ([pkConcessionRelationshipId] ASC),
    CONSTRAINT [FK_tblConcessionRelationship_rtblRelationship] FOREIGN KEY ([fkRelationshipId]) REFERENCES [dbo].[rtblRelationship] ([pkRelationshipId]),
    CONSTRAINT [FK_tblConcessionRelationship_tblConcession_Child] FOREIGN KEY ([fkChildConcessionId]) REFERENCES [dbo].[tblConcession] ([pkConcessionId]),
    CONSTRAINT [FK_tblConcessionRelationship_tblConcession_Parent] FOREIGN KEY ([fkParentConcessionId]) REFERENCES [dbo].[tblConcession] ([pkConcessionId]),
    CONSTRAINT [FK_tblConcessionRelationship_tblUser] FOREIGN KEY ([fkUserId]) REFERENCES [dbo].[tblUser] ([pkUserId])
);

