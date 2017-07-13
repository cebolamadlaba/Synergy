CREATE TABLE [dbo].[rtblRole] (
    [pkRoleId]        INT            IDENTITY (1, 1) NOT NULL,
    [RoleName]        NVARCHAR (100) NOT NULL,
    [RoleDescription] NVARCHAR (500) NULL,
    [IsActive]        BIT            CONSTRAINT [DF_rtblRole_IsActive] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_rtblRole] PRIMARY KEY CLUSTERED ([pkRoleId] ASC)
);

