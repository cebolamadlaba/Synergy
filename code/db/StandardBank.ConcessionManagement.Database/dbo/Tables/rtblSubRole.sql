CREATE TABLE [dbo].[rtblSubRole] (
    [SubRoleId] INT           IDENTITY (1, 1) NOT NULL,
    [Name]      VARCHAR (255) NOT NULL,
    [Active]    BIT           DEFAULT ((1)) NULL,
    [fkRoleId]  INT           NULL,
    PRIMARY KEY CLUSTERED ([SubRoleId] ASC),
    FOREIGN KEY ([fkRoleId]) REFERENCES [dbo].[rtblRole] ([pkRoleId])
);



