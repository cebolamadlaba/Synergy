CREATE TABLE [dbo].[tblUserRole] (
    [pkUserRoleId] INT IDENTITY (1, 1) NOT NULL,
    [fkUserId]     INT NOT NULL,
    [fkRoleId]     INT NOT NULL,
    [IsActive]     BIT CONSTRAINT [DF_tblUserRole_IsActive] DEFAULT ((1)) NOT NULL,
    [fkSubRoleId]  INT NULL,
    CONSTRAINT [PK_tblUserRole] PRIMARY KEY CLUSTERED ([pkUserRoleId] ASC),
    FOREIGN KEY ([fkSubRoleId]) REFERENCES [dbo].[rtblSubRole] ([SubRoleId]),
    CONSTRAINT [FK_tblUserRole_tblRole] FOREIGN KEY ([fkRoleId]) REFERENCES [dbo].[rtblRole] ([pkRoleId]),
    CONSTRAINT [FK_tblUserRole_tblUser] FOREIGN KEY ([fkUserId]) REFERENCES [dbo].[tblUser] ([pkUserId])
);



