CREATE TABLE [dbo].[tblConcessionTypeMismatchEscalation] (
    [pkConcessionTypeMismatchEscalationId] INT      IDENTITY (1, 1) NOT NULL,
    [fkConcessionTypeId]                   INT      NOT NULL,
    [LastEscalationSentDateTime]           DATETIME DEFAULT (getdate()) NOT NULL,
    PRIMARY KEY CLUSTERED ([pkConcessionTypeMismatchEscalationId] ASC),
    FOREIGN KEY ([fkConcessionTypeId]) REFERENCES [dbo].[rtblConcessionType] ([pkConcessionTypeId]),
    CONSTRAINT [UC_ConcessionTypeMismatchEscalation] UNIQUE NONCLUSTERED ([fkConcessionTypeId] ASC)
);

