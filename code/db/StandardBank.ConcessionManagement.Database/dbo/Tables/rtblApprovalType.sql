CREATE TABLE [dbo].[rtblApprovalType] (
    [pkApprovalTypeId] INT          IDENTITY (1, 1) NOT NULL,
    [Description]      VARCHAR (50) NOT NULL,
    [IsActive]         BIT          NOT NULL,
    CONSTRAINT [PK_rtblApprovalType] PRIMARY KEY CLUSTERED ([pkApprovalTypeId] ASC)
);

