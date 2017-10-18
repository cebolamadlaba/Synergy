CREATE TABLE [Audit].[rtblReviewFeeType] (
    [pkAuditReviewFeeTypeId] INT          IDENTITY (1, 1) NOT NULL,
    [pkReviewFeeTypeId]      INT          NOT NULL,
    [fkAuditTypeId]          INT          NOT NULL,
    [Entity]                 XML          NOT NULL,
    [Username]               VARCHAR (50) NOT NULL,
    [DateStamp]              DATETIME     CONSTRAINT [DF_Audit_rtblReviewFeeType_DateStamp] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Audit_rtblReviewFeeType] PRIMARY KEY CLUSTERED ([pkAuditReviewFeeTypeId] ASC),
    CONSTRAINT [FK_Audit_rtblReviewFeeType_AuditType] FOREIGN KEY ([fkAuditTypeId]) REFERENCES [Audit].[AuditType] ([Id])
);

