CREATE TABLE [dbo].[tblConcessionComment] (
    [pkConcessionCommentId]   INT           IDENTITY (1, 1) NOT NULL,
    [fkConcessionId]          INT           NOT NULL,
    [fkUserId]                INT           NOT NULL,
    [fkConcessionSubStatusId] INT           NOT NULL,
    [Comment]                 VARCHAR (800) NOT NULL,
    [SystemDate]              DATETIME      NOT NULL,
    [IsActive]                BIT           NOT NULL,
    CONSTRAINT [PK_tblConcessionComment] PRIMARY KEY CLUSTERED ([pkConcessionCommentId] ASC),
    CONSTRAINT [FK_tblConcessionComment_rtblSubStatus] FOREIGN KEY ([fkConcessionSubStatusId]) REFERENCES [dbo].[rtblSubStatus] ([pkSubStatusId]),
    CONSTRAINT [FK_tblConcessionComment_tblConcession] FOREIGN KEY ([fkConcessionId]) REFERENCES [dbo].[tblConcession] ([pkConcessionId]),
    CONSTRAINT [FK_tblConcessionComment_tblUser] FOREIGN KEY ([fkUserId]) REFERENCES [dbo].[tblUser] ([pkUserId])
);



