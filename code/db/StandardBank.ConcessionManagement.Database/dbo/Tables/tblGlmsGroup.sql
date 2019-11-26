CREATE TABLE [dbo].[tblGlmsGroup] (
    [pkGlmsGroupId] INT           IDENTITY (1, 1) NOT NULL,
    [GroupNumber]   INT           NOT NULL,
    [GroupName]     VARCHAR (200) NOT NULL,
    [IsActive]      BIT           NOT NULL,
    PRIMARY KEY CLUSTERED ([pkGlmsGroupId] ASC)
);

