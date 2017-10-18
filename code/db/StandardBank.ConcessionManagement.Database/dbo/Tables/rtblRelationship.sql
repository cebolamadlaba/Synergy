CREATE TABLE [dbo].[rtblRelationship] (
    [pkRelationshipId] INT          IDENTITY (1, 1) NOT NULL,
    [Description]      VARCHAR (50) NOT NULL,
    [IsActive]         BIT          CONSTRAINT [DF_rtblRelationship_IsActive] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_rtblRelationship] PRIMARY KEY CLUSTERED ([pkRelationshipId] ASC)
);

