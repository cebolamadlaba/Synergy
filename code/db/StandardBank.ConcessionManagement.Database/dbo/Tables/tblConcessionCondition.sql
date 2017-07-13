CREATE TABLE [dbo].[tblConcessionCondition] (
    [pkConcessionConditionId] INT             IDENTITY (1, 1) NOT NULL,
    [fkConcessionId]          INT             NOT NULL,
    [fkConditionTypeId]       INT             NOT NULL,
    [fkConditionProductId]    INT             NOT NULL,
    [InterestRate]            DECIMAL (18, 2) NULL,
    [Volume]                  INT             NULL,
    [Value]                   DECIMAL (18, 2) NULL,
    [IsActive]                BIT             NOT NULL,
    CONSTRAINT [PK_tblConcessionCondition] PRIMARY KEY CLUSTERED ([pkConcessionConditionId] ASC),
    CONSTRAINT [FK_tblConcessionCondition_rtblConditionProduct] FOREIGN KEY ([fkConditionProductId]) REFERENCES [dbo].[rtblConditionProduct] ([pkConditionProductId]),
    CONSTRAINT [FK_tblConcessionCondition_rtblConditionType] FOREIGN KEY ([fkConditionTypeId]) REFERENCES [dbo].[rtblConditionType] ([pkConditionTypeId]),
    CONSTRAINT [FK_tblConcessionCondition_tblConcession] FOREIGN KEY ([fkConcessionId]) REFERENCES [dbo].[tblConcession] ([pkConcessionId])
);

