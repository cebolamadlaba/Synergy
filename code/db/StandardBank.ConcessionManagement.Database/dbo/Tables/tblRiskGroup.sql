CREATE TABLE [dbo].[tblRiskGroup] (
    [pkRiskGroupId]   INT           IDENTITY (0, 1) NOT NULL,
    [RiskGroupNumber] INT           NOT NULL,
    [RiskGroupName]   VARCHAR (200) NOT NULL,
    [IsActive]        BIT           NOT NULL,
    CONSTRAINT [PK_tblRiskGroup] PRIMARY KEY CLUSTERED ([pkRiskGroupId] ASC)
);

