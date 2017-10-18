CREATE TABLE [Audit].[rtblMarketSegment] (
    [pkAuditMarketSegmentId] INT          IDENTITY (1, 1) NOT NULL,
    [pkMarketSegmentId]      INT          NOT NULL,
    [fkAuditTypeId]          INT          NOT NULL,
    [Entity]                 XML          NOT NULL,
    [Username]               VARCHAR (50) NOT NULL,
    [DateStamp]              DATETIME     CONSTRAINT [DF_Audit_rtblMarketSegment_DateStamp] DEFAULT (getdate()) NOT NULL,
    CONSTRAINT [PK_Audit_rtblMarketSegment] PRIMARY KEY CLUSTERED ([pkAuditMarketSegmentId] ASC),
    CONSTRAINT [FK_Audit_rtblMarketSegment_AuditType] FOREIGN KEY ([fkAuditTypeId]) REFERENCES [Audit].[AuditType] ([Id])
);

